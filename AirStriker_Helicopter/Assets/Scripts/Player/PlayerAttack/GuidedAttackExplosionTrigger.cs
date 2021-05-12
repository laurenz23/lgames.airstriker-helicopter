using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to guided attack explosion trigger object, a child of main guided attack object
/// the object that attached with this script must have a trigger
/// it will trigger the explosion effect once it is collided to object that have given tags
/// </summary>

namespace game_ideas
{
    public class GuidedAttackExplosionTrigger : MonoBehaviour
    {
        [SerializeField] private GuidedAttack guidedMissile = null;

        private EffectHandler effectHandler;
        private CameraManager cameraManager;
        private PlayerAttackInfo playerAttackInfo;
        private PlayerManager playerManager;

        private void Start()
        {
            effectHandler = FindObjectOfType<EffectHandler>();
            cameraManager = FindObjectOfType<CameraManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            playerAttackInfo = GetComponent<PlayerAttackInfo>();
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

                                // disable the collide and destroy the attack since we are going to return 
                                GetComponent<Collider>().enabled = false;
                                guidedMissile.DestroyArmament();

                                // return to avoid duplicate explosion effect
                                return;
                            }
                        }

                        // create explosion effect
                        if (!effectHandler.Equals(null))
                        {
                            effectHandler.CreatePrefabEffectAndDestroy(effectHandler.explosionEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                                new Vector3(0f, transform.position.y, transform.position.z), 3f);
                        }

                        // disabled the collider of the attack while waiting to destroy it to avoid enemy character exploding
                        GetComponent<Collider>().enabled = false;
                        guidedMissile.DestroyArmament();
                    }
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            // if armament exist the collider of the game boundary then destroy it
            if (other.CompareTag(GameTag.GameBoundary.ToString()))
            {
                guidedMissile.DestroyArmament();
            }
        }
    }
}
