using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player manager object or main player object itself
/// manages the player speed, ascending, descending and etc.
/// </summary>

namespace game_ideas
{
    public class PlayerManager : MonoBehaviour
    {

        [HideInInspector]
        public bool moveForward;

        [HideInInspector]
        public bool moveBackward;

        [HideInInspector]
        public bool moveAscending;

        [HideInInspector]
        public bool moveDescending;

        [HideInInspector]
        public bool attack;

        [HideInInspector]
        public bool activeSkill1; // special attack

        [HideInInspector]
        public int points; // score of the player that is showned as points in the game

        [HideInInspector]
        public int coins; // coins of the player, can be used to upgrade and purchase


        [Header("Player Properties")]

        public int characterHealth; // the health of character, this is an static health can be change base on character upgrades

        [HideInInspector]
        public int health; // the current health of character, this is a dynamic health can be change in game if hit by enemy

        public int energy;

        public float moveSpeed;

        public Transform playerTransform;


        [Header("Script Reference")]

        public Rigidbody playerRigidbody;

        public PlayerAnimator playerAnimator;

        public PlayerMovement playerMovement;

        public PlayerColliderHandler playerColliderHandler;

        public PlayerGroundCollider playerGroundCollider;

        public PlayerEffect playerEffect;

        public PlayerAttackHandler playerAttack;

        [HideInInspector]
        public GameManager gameManager;

        [HideInInspector]
        public CameraManager cameraManager;

        [HideInInspector]
        public InGameUIManager inGameUIManager;

        [HideInInspector]
        public PlayerUIManager playerUIManager;

        [HideInInspector]
        public OnHitCharacter onHitCharacter;

        [HideInInspector]
        public SoundFXHandler soundFXHandler;


        public static PlayerManager instance;

        public static PlayerManager GetInstance()
        {
            return instance;
        }
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            soundFXHandler = FindObjectOfType<SoundFXHandler>();

            gameManager = FindObjectOfType<GameManager>();

            cameraManager = FindObjectOfType<CameraManager>();

            inGameUIManager = FindObjectOfType<InGameUIManager>();

            playerUIManager = FindObjectOfType<PlayerUIManager>();
        }

        private void Start()
        {
            // get components
            onHitCharacter = GetComponent<OnHitCharacter>();

#if UNITY_EDITOR
            // game manager is not attached
            if (gameManager.Equals(null))
            {
                Debug.LogError("Player Manager is missing a reference of Game Manager, please attached Game Manager");
            }

            // in game ui manager is not attached
            if (inGameUIManager.Equals(null))
            {
                Debug.LogError("Player Manager is missing a reference of In Game UI Manager, please attached In Game UI Manager, can be found at Game Manager as children");
            }

            // player ui manager is not attached
            if (playerUIManager.Equals(null))
            {
                Debug.LogError("Player Manager is missing a reference of Player UI Manager, please attach Player UI Manager, can be found at In Game UI Manager object");
            }
#endif

            // set the points of the player when the game is started
            points = 0;

            // set the coins of the player when the game is started
            coins = 0;

            // set the player health base on max health
            health = characterHealth;

            // set the health value for ui so the player will know how much health have
            playerUIManager.SetPlayerHealth_ui(health);

            // set the energy value for ui so the player will know how much energy have
            playerUIManager.SetPlayerEnergy_ui(energy);

            // preparation for on hit material
            onHitCharacter.SetMaterial();
        }

        private void Update()
        {

            switch (gameManager.gameState)
            {

                case GameState.WAITING_TO_START:

                    // waiting the game to press the start button
                    break;

                case GameState.GAME_START:
                case GameState.GAME_CONTINUE:

                    playerMovement.PlayerMove(moveForward, moveBackward, moveAscending, moveDescending); // Handles the player movement forward

                    playerMovement.PlayerManuever(moveForward, moveBackward, moveAscending, moveDescending); // Handles the player going up or down

                    PlayerAttack(); // Handles the player attacks                    

                    break;

                case GameState.GAMEOVER:

                    // do something here...

                    break;

            }

        }


        // handles player attack action
        private void PlayerAttack()
        {
            if (attack)
            {

                playerAttack.Attack(playerTransform);

            }
            
            if (activeSkill1)
            {
                playerAttack.AtomicAttack(playerTransform);

            }
        }

        // for easy global access
        public bool PlayerOnGround()
        {

            return playerGroundCollider.OnGround();

        }


        // for easy player energy status
        public bool PlayerHaveEnergy()
        {

            if (energy > 0)
            {
                return true;
            }

            return false;

        }

        public void SetPlayerHealth(int value, bool asDamage = false)
        {
            if (asDamage) 
            {
                health -= value; // deduct the player health base on value

                playerEffect.PlayerDisplayPopupText(playerTransform, playerEffect.popupText_damage, "-" + value.ToString());

                playerAnimator.OnPlayerHit(health);
            }
            else
            {
                health += value; // increase the player health base on value

                playerEffect.PlayerDisplayPopupText(playerTransform, playerEffect.popupText_damage, "+" + value.ToString());
            }

            playerUIManager.SetPlayerHealth_ui(health);
        }

        public void SetPlayerEnergy(int value, Transform energyTransform)
        {

            energy += value; // add energy to player

            playerUIManager.SetPlayerEnergy_ui(energy); // set the energy for ui

            playerEffect.PlayerDisplayPopupText(energyTransform, playerEffect.popupText_energy, "+" + value.ToString()); // display popup text with the value

            playerEffect.PlayerEffectOnEnergyCollect(energyTransform); // call particle script effect to create

            Destroy(energyTransform.gameObject); // destroy the energy once plane make contact

        }
        
        // set player coins
        public void SetPlayerCoins(int value, Transform coinTransform)
        {

            coins += value; // add coins for the player

            playerEffect.PlayerDisplayPopupText(coinTransform, playerEffect.popupText_coins, "+" + value.ToString()); // display popup text with the value

            playerEffect.PlayerEffectOnCoinsCollect(coinTransform); // call particle effect script for coins

            Destroy(coinTransform.gameObject); // destroy the coin once plane make contact

        }

        // set player points
        public void SetPlayerPoints(int value)
        {

            points += value;

            playerUIManager.SetPlayerPoints_ui(points);

        }

    }
}
