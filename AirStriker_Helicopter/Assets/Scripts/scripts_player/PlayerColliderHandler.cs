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

            // helicopter is collided to coin object
            if (collider.transform.CompareTag(GameTag.Tokens.ToString()))
            {
                playerManager.soundFXHandler.SFX_COLLECT_COIN("coin1");

                ObjectiveHandler oh = collider.GetComponent<ObjectiveHandler>();
                oh.enabled = true;
                oh.DestroyObjective();
                int value = oh.value;
                GameObjective go = oh.objectiveType;

                if (go == GameObjective.DIAMONDS)
                {
                    playerManager.AddPlayerDiamonds(value, collider.transform);
                }
                else if (go == GameObjective.COINS)
                {
                    playerManager.AddPlayerCoins(value, collider.transform);
                }

                return; // we need to return since it does not affect the health of the player

            }
            else if (collider.transform.CompareTag(GameTag.EnemyAttack.ToString()))
            {
                playerManager.soundFXHandler.SFX_HIT_METAL("metal2");

                int damage = collider.GetComponent<ArmamentAttackData>().GetDamage();

                // set player health, set to ui and display the damage
                playerManager.ComputePlayerHealth(damage, true);

                // check if the current game object is still active to avoid errors
                // where the coroutine is trying to start even the object is already destroyed
                if (gameObject.activeSelf)
                {
                    // change the material to hit material
                    playerManager.onHitCharacter.OnHit();
                }
            }
            else if (collider.transform.CompareTag(GameTag.Enemy.ToString()))
            {
                playerManager.soundFXHandler.SFX_ALERT_WARNING("warning2");

                // if the player collided to enemy character
                // deduct the player health base on enemy health since it is a game character
                EnemyHandler enemyHandler = collider.transform.GetComponent<EnemyHandler>();

                int enemyHealth = collider.transform.GetComponent<EnemyHandler>().enemyData.health;

                // assign damage base on enemy health

                int damage = enemyHealth;

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

                // set player health, set to ui and display the damage
                playerManager.ComputePlayerHealth(damage, true);
            }
            // character is collided to diamond object
            else if (collider.transform.CompareTag(GameTag.Ground.ToString()) || collider.transform.CompareTag(GameTag.Terrain.ToString()))
            {
                playerManager.soundFXHandler.SFX_ALERT_WARNING("warning2");

                int damage = playerManager.health; // if the player collided to terrain and ground, deduct the player health base on it's current maximum health

                // set player health, set to ui and display the damage
                playerManager.ComputePlayerHealth(damage, true);
            }
            // when collided to finish trigger
            else if (collider.transform.CompareTag(GameTag.Finish.ToString()))
            {
                playerManager.PlayerLevelComplete();
                collider.gameObject.SetActive(false);
            }

            // check player health
            // if player health is equal or less than to zero
            // if don't have health to continue the game, we need to set as game over and explode the player
            if (playerManager.health <= 0)
            {
                playerManager.soundFXHandler.SFX_EXPLODE_BIG("big1");

                playerManager.PlayerGameover();
            }

        }

    }
}
