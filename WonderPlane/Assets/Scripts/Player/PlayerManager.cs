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
        public bool automic; // special attack

        [HideInInspector]
        public int points; // score of the player that is showned as points in the game

        [HideInInspector]
        public int coins; // coins of the player, can be used to upgrade and purchase


        [Header("Player Properties")]

        public int health;

        public int energy;

        public float moveSpeed;

        public Transform player;


        [Header("Script Reference")]

        public GameManager gameManager;

        public InGameUIManager inGameUIManager;

        public PlayerUIManager playerUIManager;


        private PlayerMovement playerMovement; // handles player movement

        private PlayerColliderHandler playerColliderHandler;

        private PlayerGroundCollider playerGroundCollider; // check if on land

        private PlayerAttackHandler playerAttack;

        private Rigidbody RIGIDBODY;


        private void Awake()
        {

            playerMovement = GetComponent<PlayerMovement>();

            playerColliderHandler = GetComponent<PlayerColliderHandler>();

            playerGroundCollider = GetComponentInChildren<PlayerGroundCollider>();

            playerAttack = GetComponent<PlayerAttackHandler>();

            RIGIDBODY = GetComponent<Rigidbody>();

        }

        public Transform GetPlayerTransform()
        {

            return player;

        }

        private void Start()
        {

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

            // set the health value for ui so the player will know how much health have
            playerUIManager.SetPlayerHealth_ui(health);

            // set the energy value for ui so the player will know how much energy have
            playerUIManager.SetPlayerEnergy_ui(energy);
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

                playerAttack.Attack(GetPlayerTransform());

            }
            
            if (automic)
            {

                playerAttack.AutomicAttack(GetPlayerTransform());

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

        // set player points
        public void SetPlayerPoints(int value)
        {

            points += value;
            playerUIManager.SetPlayerPoints_ui(points);

        }
        
    }
}
