using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// this script is attached to inGameUI manager object
/// manages navigation to access other panel in game
/// </summary>

namespace game_ideas
{
    public class InGameUIManager : MonoBehaviour
    {
        
        // navigation ui
        [Header("InGame Navigation UI")]
        public Button startGame_btn;
        public Button pause_btn;
        public Button play_btn;

        public GameObject player_ui;

        // script reference
        [Header("Script Reference")]
        public InGamePause inGamePause;
        public InGameOver inGameOver;
        public InGameLevelComplete inGameLevelComplete;
        [HideInInspector] public GameManager gameManager;
        [HideInInspector] public SoundManager soundManager;

        private void Awake()
        {
            gameManager = GameManager.GetInstance();
            soundManager = SoundManager.GetInstance();
        }

        private void Start()
        {

            startGame_btn.gameObject.SetActive(true);

        }

        // start the game
        public void StartGame()
        {
            startGame_btn.gameObject.SetActive(false);
            gameManager.gameState = GameState.GAME_START; // set game manager to game start
        }
        
        // call this function if game want to pause
        public void PauseGame()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            pause_btn.gameObject.SetActive(false);
            player_ui.SetActive(false); // hide player ui when game is pause
            inGamePause.gameObject.SetActive(true); // display pause panel when game is pause
            gameManager.gameState = GameState.GAME_PAUSE; // set game manager to game pause

        }

    }
}
