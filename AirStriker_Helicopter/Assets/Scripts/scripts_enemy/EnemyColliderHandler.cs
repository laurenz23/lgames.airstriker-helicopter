using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached this script as a parent of the object or character
/// </summary>

namespace game_ideas
{
    public class EnemyColliderHandler : MonoBehaviour
    {
        [SerializeField] private EnemyHandler enemyHandler;

        private CameraManager cameraManager;
        private PlayerManager playerManager;

        private void Awake()
        {
            cameraManager = enemyHandler.cameraManager;
        }

        private void Start()
        {
            playerManager = PlayerManager.GetInstance();
        }

        private void OnTriggerEnter(Collider collider)
        {
            // if the enemy character collided to ground explode automatically
            if (
                collider.CompareTag(GameTag.Ground.ToString()) ||
                collider.CompareTag(GameTag.Terrain.ToString())
                )
            {
                enemyHandler.DestroyCharacter();
                return;
            }

            // if enemy collided to other enemy character
            if (collider.CompareTag(GameTag.Enemy.ToString()))
            {
                // check if other enemy health is greater than to these enemy character health
                // if these enemy health is equal or less than to other enemy health
                // explode these enemy character otherwise deduct this character health base on other enemy current health
                if (enemyHandler.enemyData.health <= collider.transform.GetComponent<EnemyHandler>().enemyData.health)
                {
                    enemyHandler.DestroyCharacter();
                }
                else
                {
                    // deduct this character health base on other enemy current health
                    enemyHandler.enemyData.health -= collider.transform.GetComponent<EnemyHandler>().enemyData.health;
                }

                return;

            }

            // if collided to player attack then apply on hit effect to character
            if (collider.CompareTag(GameTag.BasicAttack.ToString()))
            {   
                /* 
                 * don't explode and affect other object if the attack is out of the camera view base on the Z axis
                 * if attack collide to enemy character, will take enemy health base on attack damage
                 * once enemy character health is equal or below to zero it will destroy and explode the enemy character
                 */
                if (cameraManager != null)
                {
                    Vector3 screenBounds = cameraManager.screenBounds;

                    if (!(transform.position.z >= screenBounds.z))
                    {
                        // character is hit by armament
                        HitByArmament(collider.GetComponent<ArmamentAttackData>());
                    }
                }
            }

        }
        
        private void OnTriggerExit(Collider collider)
        {
            // once the object exit's on objectEnabler will destroy the character
            if (collider.GetComponent<ObjectEnabler>())
            {
                Destroy(enemyHandler.gameObject);
            }
        }

        // call this function when enemy character is hit by armament
        public void HitByArmament(ArmamentAttackData armamentAttackData)
        {
            // deduct enemy health by armament damage
            enemyHandler.enemyData.health -= armamentAttackData.GetDamage();

            // check if enemy health is equal or less than to zero
            if (enemyHandler.enemyData.health <= 0f)
            {
                // destroy enemy character
                enemyHandler.DestroyCharacter();

                // set points for player after destroying the character
                playerManager.AddPlayerPoints(enemyHandler.enemyData.points);

                // don't update material
                return;
            }

            // check if the current game object is still active to avoid errors
            // where the coroutine is trying to start even the object is already destroyed
            if (gameObject.activeSelf)
            {
                // change the material to hit material
                enemyHandler.onHitCharacter.OnHit();
            }
        }
    }
}
