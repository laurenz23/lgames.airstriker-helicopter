using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class EnemyColliderHandler : MonoBehaviour
    {
        [SerializeField] private Material default_material = null; // reference for default material here
        [SerializeField] private Material hit_material = null; // reference for hit material here
        [HideInInspector] public EnemyHandler enemyHandler;

        [SerializeField] private Renderer[] objectRenderer = null;

        private void Awake()
        {

            enemyHandler = GetComponent<EnemyHandler>();

            objectRenderer = GetComponentsInChildren<Renderer>(); // get all objects that have mesh rendered

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

            // if the enemy collided to player subtract enemy current health from player current health
            if (collider.CompareTag(GameTag.Player.ToString()))
            {
                // enemy current health subtract from player current health
                enemyHandler.enemyData.health -= collider.transform.GetComponent<PlayerColliderHandler>().playerManager.health;

                // if enemy current health is equal or less than 0 then explode the enemy character
                if (enemyHandler.enemyData.health <= 0)
                {
                    enemyHandler.DestroyCharacter();
                    return;
                }
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
                    return;
                }
                else
                {
                    enemyHandler.DestroyCharacter();
                    return;
                }

            }

            // if collided to player attack then apply on hit effect to character
            if (collider.CompareTag(GameTag.BasicAttack.ToString()))
            {
                // check if the current game object is still active to avoid errors
                // where the coroutine is trying to start even the object is already destroyed
                if (gameObject.activeSelf)
                {
                    // change the material to hit material
                    foreach (Renderer r in objectRenderer)
                    {
                        r.material = hit_material;
                    }

                    // start the coroutine to change to default material
                    StartCoroutine(RestoreDefaultMaterial());
                }
            }

        }

        IEnumerator RestoreDefaultMaterial()
        {
            yield return new WaitForSeconds(0.2f);
            
            // change the material to default
            foreach (Renderer r in objectRenderer)
            {
                r.material = default_material;
            }

            // stop the started coroutine
            StopCoroutine(RestoreDefaultMaterial());
        }

    }
}
