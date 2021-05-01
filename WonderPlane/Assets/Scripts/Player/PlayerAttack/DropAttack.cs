using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to armament itself
/// handling the collision of the armament and explosion including the movemement of the armament
/// use this script to armament drop attacks only
/// </summary>

namespace game_ideas
{
    public class DropAttack : MonoBehaviour
    {
        [SerializeField] private GameObject armament = null;
        public bool playerDescending; // if player is descending for nice armament drop effect
        public CameraManager cameraManager;

        [SerializeField] private GameObject explosionEffect = null;
        
        private Rigidbody RIGIDBODY;
        private PlayerAttackInfo playerAttackInfo;
        private EffectHandler effectHandler;
        private PlayerManager playerManager;

        // we cannot destroy the bullet object instantly, wait for trail effect to finish to have a nice effect
        // so we assign the disabled bullet to hide and stop the movement of bullet once it collided
        private bool disabledBullet = false;

        private void Start()
        {
            cameraManager = FindObjectOfType<CameraManager>();
            effectHandler = FindObjectOfType<EffectHandler>();
            playerManager = FindObjectOfType<PlayerManager>();
            RIGIDBODY = GetComponent<Rigidbody>();
            playerAttackInfo = GetComponent<PlayerAttackInfo>();
        }

        private void Update()
        {

            if (disabledBullet)
            {
                RIGIDBODY.isKinematic = true;
            }
            else
            {
                // when player is descending increase the down velocity of the armament
                if (playerDescending)
                {
                    RIGIDBODY.velocity += Vector3.down * 30f * Time.deltaTime;
                }

                transform.position += transform.forward * 4f * Time.deltaTime;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (
                other.CompareTag(GameTag.Ground.ToString()) ||
                other.CompareTag(GameTag.Terrain.ToString()) ||
                other.CompareTag(GameTag.Enemy.ToString())
                )
            {

                /* 
                 * don't explode and affect other object if the attack is out of the camera view base on the Z axis
                 * if attack collide to enemy character, will take enemy health base on attack damage
                 * once enemy character health is equal or below to zero it will destroy and explode the enemy character
                 */
                if (!cameraManager.Equals(null))
                {
                    Vector3 screenBounds = cameraManager.screenBounds;

                    if (!(transform.position.z >= screenBounds.z))
                    {
                        // assign array inside area affect
                        Collider[] colliderArray = Physics.OverlapSphere(transform.position, playerAttackInfo.playerAttackData.aoe);

                        foreach (Collider ca in colliderArray)
                        {
                            // check the object inside area affect if an enemy
                            if (ca.GetComponent<EnemyHandler>())
                            {
                                EnemyHandler enemyHandler = ca.GetComponent<EnemyHandler>();

                                // take enemy health base on attack damage
                                enemyHandler.enemyData.health -= playerAttackInfo.playerAttackData.damage;

                                // check if enough health to explode the enemy character
                                if (enemyHandler.enemyData.health <= 0f)
                                {

                                    // explode and display points when enemy character health is equal to zero or below
                                    enemyHandler.DestroyCharacter();

                                    // set player points and update the ui points
                                    playerManager.SetPlayerPoints(enemyHandler.enemyData.points);
                                }
                            }
                            else if (ca.GetComponent<EnemyAttackStraight>())
                            {
                                Destroy(ca.GetComponent<EnemyAttackStraight>().gameObject);
                            }
                            else if (ca.GetComponent<EnemyAttackGuided>())
                            {
                                Destroy(ca.GetComponent<EnemyAttackGuided>().gameObject);
                            }
                        }

                        // create explosion effect for the armament
                        if (effectHandler != null)
                        {
                            effectHandler.CreatePrefabEffectAndDestroy(explosionEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                            new Vector3(0f, transform.position.y, transform.position.z), 3f);
                        }

                        // destroy the attack
                        DestroyArmament();
                    }
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            // if armament exist the collider of the game boundary then destroy it
            if (other.transform.CompareTag(GameTag.GameBoundary.ToString()))
            {
                DestroyArmament();
            }
        }

        private void DestroyArmament()
        {

            // disable the bullet object collider while waiting to destroy and hide the armament
            disabledBullet = true;
            GetComponent<Collider>().enabled = false;
            armament.SetActive(false);

            // wait to finish the trail effect before destroying the object
            // added a certain delay before destroying the bullet object for the trail effect transition
            Destroy(this.gameObject, 2f);

        }

    }
}
