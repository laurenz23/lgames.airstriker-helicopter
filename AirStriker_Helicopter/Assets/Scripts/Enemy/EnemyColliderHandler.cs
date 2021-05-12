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
        [HideInInspector] public EnemyHandler enemyHandler;

        private void Awake()
        {
            enemyHandler = GetComponent<EnemyHandler>();
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
                if (enemyHandler.enemyData.health > collider.transform.GetComponent<EnemyHandler>().enemyData.health)
                {
                    // deduct this character health base on other enemy current health
                    enemyHandler.enemyData.health -= collider.transform.GetComponent<EnemyHandler>().enemyData.health;
                }
                else
                {
                    enemyHandler.DestroyCharacter();
                }

                return;

            }

            // if collided to player attack then apply on hit effect to character
            if (collider.CompareTag(GameTag.BasicAttack.ToString()))
            {
                // check if the current game object is still active to avoid errors
                // where the coroutine is trying to start even the object is already destroyed
                if (gameObject.activeSelf)
                {
                    // change the material to hit material
                    enemyHandler.onHitCharacter.OnHit();
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
    }
}
