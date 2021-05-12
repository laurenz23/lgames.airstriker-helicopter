using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// this script is attached to inGameUI manager object
/// that manages the ui of the game
/// </summary>

namespace game_ideas
{
    public class InGameUIManager : MonoBehaviour
    {
        
        // navigation ui
        [Header("InGame Navigation UI")]
        [SerializeField] private Button startGame_btn = null;
        [SerializeField] private Button pause_btn = null;
        [SerializeField] private Button play_btn = null;

        // script reference
        [Header("Script Reference")]
        public GameManager gameManager;

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
            SceneManager.LoadScene("Level1");
            //pause_btn.gameObject.SetActive(false);
            //play_btn.gameObject.SetActive(true);
            //gameManager.gameState = GameState.GAME_PAUSE; // set game manager to game pause

        }

        // call this function if the game is pause to continue the game
        public void ContinueGame()
        {

            pause_btn.gameObject.SetActive(true);
            play_btn.gameObject.SetActive(false);
            gameManager.gameState = GameState.GAME_CONTINUE; // set game manager to continue

        }

    }
}
