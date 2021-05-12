using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to armament itself
/// Handling the collision of the armament and explosion including the movement of the armament
/// </summary>

namespace game_ideas
{
    public class StraightAttack : MonoBehaviour
    {
        
        public GameObject[] armament;
        [SerializeField] private GameObject explosionEffect = null;

        private Rigidbody RIGIDBODY;
        private PlayerAttackInfo playerAttackInfo;
        private EffectHandler effectHandler;
        private CameraManager cameraManager;
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

            // once the bullet is disable it will stop to move and hide the bullet while waiting to destroy the object
            if (disabledBullet)
            {
                RIGIDBODY.isKinematic = true;
            }
            else
            {
                transform.position += transform.forward * playerAttackInfo.attackData.speed * Time.deltaTime;
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

                        // check if the collide is an enemy object
                        if (other.GetComponent<EnemyHandler>())
                        {
                            EnemyHandler enemyHandler = other.GetComponent<EnemyHandler>();

                            // take enemy health base on attack damage
                            enemyHandler.enemyData.health -= playerAttackInfo.attackData.damage;

                            // check if enough health to explode the enemy character
                            if (enemyHandler.enemyData.health <= 0f)
                            {

                                // explode and display points when enemy character health is equal to zero or below
                                enemyHandler.DestroyCharacter();

                                // set player points and update the ui points
                                playerManager.SetPlayerPoints(enemyHandler.enemyData.points);

                                // destroy the attack since we are going to return 
                                DestroyArmament();

                                // return to avoid duplicate explosion effect
                                return;
                            }
                        }


                        // create explosion effect
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
            if (other.CompareTag(GameTag.GameBoundary.ToString()))
            {
                DestroyArmament();
            }
        }

        private void DestroyArmament()
        {

            // disable the bullet object and collider while waiting to destroy
            disabledBullet = true;
            GetComponent<Collider>().enabled = false;

            // hide the bullet so the player won't see while waiting to destroy the object
            foreach (GameObject arma in armament)
            {
                arma.SetActive(false);
            }

            // wait to finish the trail effect before destroying the object
            // added a certain delay before destroying the bullet object for the trail effect
            Destroy(this.gameObject, 2f);

        }

    }
}
