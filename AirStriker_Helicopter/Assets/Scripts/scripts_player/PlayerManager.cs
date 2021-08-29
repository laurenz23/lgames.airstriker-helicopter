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
        public int diamonds; // current collected diamonds of player in the current stage level

        [HideInInspector]
        public int coins; // current collected coins of player in the current stage level

        private int dataDiamonds; // diamonds of the player, can be use to research weapons and buy new unit

        private int dataCoins; // coins of the player, can be use to upgrade and purchase

        private int selectedUnit; // reference of what unit is selected in the main menu scene by the player

        [Header("Player Properties")]

        public int characterHealth; // the health of character, this is an static health can be change base on character upgrades

        [HideInInspector]
        public int health; // the current health of character, this is a dynamic health can be change in game if hit by enemy

        public float moveSpeed;

        public Transform playerTransform;

        [HideInInspector]
        public float horizontalForwardSpeed; // in game forward movement speed

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

        [HideInInspector]
        public ProfilePlayerDataManager profilePlayerDataManager;

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

            profilePlayerDataManager = FindObjectOfType<ProfilePlayerDataManager>();
        }

        private void Start()
        {
            // get components
            onHitCharacter = GetComponent<OnHitCharacter>();

            // set horizontal forward speed
            horizontalForwardSpeed = gameManager.horizontalForwardSpeed;

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

            // set points of the player when the game is started
            points = profilePlayerDataManager.profileScoreData.score;

            // set diamonds of the player when the game is started
            diamonds = 0;

            // set coins of the player when the game is started
            coins = 0;

            // get player diamonds from saved data
            dataDiamonds = profilePlayerDataManager.profileTokensData.playerDiamonds;

            // get player coins from saved data
            dataCoins = profilePlayerDataManager.profileTokensData.playerCoins;

            // set the player health base on max health
            health = characterHealth;

            // set the health value for ui so the player will know how much health have
            playerUIManager.SetPlayerHealth_ui(health);

            // set player score value
            playerUIManager.SetPlayerPoints_ui(points);

            // set player diamonds value
            playerUIManager.SetPlayerDiamonds_ui(diamonds);

            // set player coinds value
            playerUIManager.SetPlayerCoins_ui(coins);

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

        public void ComputePlayerHealth(int value, bool asDamage = false)
        {
            if (asDamage) 
            {
                health -= value; // deduct the player health base on value

                playerEffect.PlayerDisplayPopupText(playerTransform, "popupTextDamage", "-" + value.ToString());

                playerAnimator.OnPlayerHit(health);
            }
            else
            {
                health += value; // increase the player health base on value

                playerEffect.PlayerDisplayPopupText(playerTransform, "popupTextDamage", "+" + value.ToString());
            }

            playerUIManager.SetPlayerHealth_ui(health);
        }

        // add player diamonds
        public void AddPlayerDiamonds(int value, Transform diamondTransform)
        {
            diamonds += value; // add diamonds for setting text ui

            dataDiamonds += value; // add diamonds for player data

            playerEffect.PlayerDisplayPopupText(diamondTransform, "popupTextDiamonds", "+" + value.ToString()); // display popup text with the value

            playerEffect.PlayerEffectOnCoinsCollect(diamondTransform); // call particle effect script for coins

            playerUIManager.SetPlayerDiamonds_ui(diamonds);  // update ui diamonds

            profilePlayerDataManager.SetDiamondsData(diamonds); // update and save profile diamonds data

            Destroy(diamondTransform.gameObject); // destroy diamond object once player make contact
        }

        // add player coins
        public void AddPlayerCoins(int value, Transform coinTransform)
        {

            coins += value; // add coins for setting text ui

            dataCoins += value; // add coins for player data

            playerEffect.PlayerDisplayPopupText(coinTransform, "popupTextCoins", "+" + value.ToString()); // display popup text with the value

            playerEffect.PlayerEffectOnCoinsCollect(coinTransform); // call particle effect script for coins

            playerUIManager.SetPlayerCoins_ui(coins); // update coins ui

            profilePlayerDataManager.SetCoinsData(dataCoins); // update and save profile coins data

            Destroy(coinTransform.gameObject); // destroy coin object once player make contact

        }

        // add player points
        public void AddPlayerPoints(int value)
        {

            points += value; // increment points value

            playerUIManager.SetPlayerPoints_ui(points); // update score ui

            profilePlayerDataManager.SetScoreData(points); // update player score data

        }

        // set the selected unit by CharacterSelectedInGame script
        public void SetSelectedUnit(int value)
        {
            selectedUnit = value;
        }

        // check if player can use the weapons from profile weapons data
        public bool HasArmament1()
        {
            if (profilePlayerDataManager.profileUnitData.unitData[selectedUnit].weaponData[0].weaponLevel == 0)
                return false;
            return true;
        }

        public bool HasArmament2()
        {
            if (profilePlayerDataManager.profileUnitData.unitData[selectedUnit].weaponData[1].weaponLevel == 0)
                return false;
            return true;
        }

        public bool HasArmament3()
        {
            if (profilePlayerDataManager.profileUnitData.unitData[selectedUnit].weaponData[2].weaponLevel == 0)
                return false;
            return true;
        }

        public bool HasArmament4()
        {
            if (profilePlayerDataManager.profileUnitData.unitData[selectedUnit].weaponData[3].weaponLevel == 0)
                return false;
            return true;
        }

        public bool HasArmament5()
        {
            if (profilePlayerDataManager.profileUnitData.unitData[selectedUnit].weaponData[4].weaponLevel == 0)
                return false;
            return true;
        }
    }
}
