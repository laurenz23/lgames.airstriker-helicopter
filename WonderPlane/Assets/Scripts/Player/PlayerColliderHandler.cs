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

        public GameObject GameManager;

        public PlayerEffect playerEffect;

        public PlayerAnimator playerAnimator;


        private GameAssetsManager gameAssetsManager;


        private void Awake()
        {

            gameAssetsManager = GameManager.GetComponent<GameAssetsManager>();

        }

        private void OnTriggerEnter(Collider collider)
        {

            // helicopter is collided to coins objects
            if (collider.transform.CompareTag(GameTag.Coins.ToString()))
            {

                int value = collider.GetComponent<ObjectiveHandler>().value;
                playerManager.coins += value; // add coins for the player
                playerEffect.PlayerDisplayPopupText(collider.transform, playerEffect.popupText_coins, "+" + value.ToString()); // display popup text with the value
                playerEffect.PlayerEffectOnCoinsCollect(collider.transform); // call particle effect script for coins
                Destroy(collider.gameObject); // destroy the coin once plane make contact
                return; // we need to return since it does not affect the health of the player

            }
            else if (collider.transform.CompareTag(GameTag.Energy.ToString()))
            {

                int value = collider.GetComponent<ObjectiveHandler>().value; // assign the value of the objective
                playerManager.energy += value; // add energy to player
                playerManager.playerUIManager.SetPlayerEnergy_ui(playerManager.energy); // set the energy for ui
                playerEffect.PlayerDisplayPopupText(collider.transform, playerEffect.popupText_energy, "+" + value.ToString()); // display popup text with the value
                playerEffect.PlayerEffectOnEnergyCollect(collider.transform); // call particle script effect to create
                Destroy(collider.gameObject); // destroy the energy once plane make contact
                return; // we need to return since it does not affect the health of the player

            }
            else if (
                collider.transform.CompareTag(GameTag.Ground.ToString()) ||
                collider.transform.CompareTag(GameTag.Terrain.ToString()) ||
                collider.transform.CompareTag(GameTag.Enemy.ToString())
                )
            {

                int damage;

                if (collider.transform.GetComponent<EnemyHandler>())
                {
                    // if the player collided to enemy character
                    // deduct the player health base on enemy health since it is a game character
                    damage = collider.transform.GetComponent<EnemyHandler>().enemyData.health; 
                }
                else
                {
                    damage = playerManager.health; // if the player collided to terrain and ground, deduct the player health base on it's current maximum health
                }

                playerManager.health -= damage;
                playerEffect.PlayerDisplayPopupText(collider.transform, playerEffect.popupText_damage, "-" + damage.ToString());
                playerManager.playerUIManager.SetPlayerHealth_ui(playerManager.health);
                playerAnimator.OnPlayerHit(playerManager.health);

            }
            else if (collider.transform.CompareTag(GameTag.EnemyAttack.ToString()))
            {

                int damage = collider.GetComponent<EnemyAttackData>().armamentAttackData.damage;

                playerManager.health -= damage; // deduct the player health base on enemies attack that is collided
                playerEffect.PlayerDisplayPopupText(collider.transform, playerEffect.popupText_damage, "-" + damage.ToString());
                playerManager.playerUIManager.SetPlayerHealth_ui(playerManager.health);
                playerAnimator.OnPlayerHit(playerManager.health);

            }

            // don't have health to continue the game, we need to set as game over and explode the player
            if (playerManager.health <= 0)
            {

                playerManager.gameManager.gameState = GameState.GAMEOVER; // set game over 
                playerEffect.PlayerEffectExplosion(playerManager.transform); // create the explosion effect
                playerManager.gameObject.SetActive(false); // hide the player

            }

        }
    }
}
