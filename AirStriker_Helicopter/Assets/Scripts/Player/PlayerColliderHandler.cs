using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player collider object
/// handles the trigger of the object
/// it includes the trigger explosion of player when it collides to enemy and obstacles, grounds and enemies attacks 
/// </summary>

namespace game_ideas
{
    public class PlayerColliderHandler : MonoBehaviour
    {

        public PlayerManager playerManager;

        private void OnTriggerEnter(Collider collider)
        {

            // helicopter is collided to coins objects
            if (collider.transform.CompareTag(GameTag.Coins.ToString()))
            {

                int value = collider.GetComponent<ObjectiveHandler>().value;
                playerManager.SetPlayerCoins(value, collider.transform);
                return; // we need to return since it does not affect the health of the player

            }
            else if (collider.transform.CompareTag(GameTag.Energy.ToString()))
            {

                int value = collider.GetComponent<ObjectiveHandler>().value; // assign the value of the objective
                playerManager.SetPlayerEnergy(value, collider.transform);
                return; // we need to return since it does not affect the health of the player

            }
            else if (
                collider.transform.CompareTag(GameTag.Ground.ToString()) ||
                collider.transform.CompareTag(GameTag.Terrain.ToString()) ||
                collider.transform.CompareTag(GameTag.Enemy.ToString())
                )
            {

                // health reference
                int damage;

                // assign damages for players
                if (collider.transform.GetComponent<EnemyHandler>())
                {
                    // if the player collided to enemy character
                    // deduct the player health base on enemy health since it is a game character
                    EnemyHandler enemyHandler = collider.transform.GetComponent<EnemyHandler>();

                    int enemyHealth = collider.transform.GetComponent<EnemyHandler>().enemyData.health;

                    // assign damage base on enemy health
                    damage = enemyHealth;

                    // check if enemy health is greater than to player current health
                    if (enemyHealth > playerManager.health)
                    {
                        // if enemy health is greater than, deduct enemy health base on player current health
                        enemyHandler.enemyData.health -= playerManager.health;
                    }
                    else
                    {
                        // if enemy health is less than or equal to player health
                        // we need to explode the enemy character
                        enemyHandler.DestroyCharacter();
                        
                        // check if the current game object is still active to avoid errors
                        // where the coroutine is trying to start even the object is already destroyed
                        if (gameObject.activeSelf)
                        {
                            // change the material to hit material
                            playerManager.onHitCharacter.OnHit();
                        }
                    }
                }
                else
                {
                    damage = playerManager.health; // if the player collided to terrain and ground, deduct the player health base on it's current maximum health
                }

                // set player health, set to ui and display the damage
                playerManager.SetPlayerHealth(damage, true);

            }
            else if (collider.transform.CompareTag(GameTag.EnemyAttack.ToString()))
            {

                int damage = collider.GetComponent<EnemyAttackData>().attackData.damage;

                // set player health, set to ui and display the damage
                playerManager.SetPlayerHealth(damage, true);

                // check if the current game object is still active to avoid errors
                // where the coroutine is trying to start even the object is already destroyed
                if (gameObject.activeSelf)
                {
                    // change the material to hit material
                    playerManager.onHitCharacter.OnHit();
                }
            }

            // check player health
            // if player health is equal or less than to zero
            // if don't have health to continue the game, we need to set as game over and explode the player
            if (playerManager.health <= 0)
            {

                playerManager.gameManager.gameState = GameState.GAMEOVER; // set game over 
                playerManager.playerEffect.PlayerEffectExplosion(playerManager.transform); // create the explosion effect
                playerManager.gameObject.SetActive(false); // hide the player

            }

        }
    }
}
