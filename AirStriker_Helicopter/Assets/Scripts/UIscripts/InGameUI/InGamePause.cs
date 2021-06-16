using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// handle in game ui for pause events
/// </summary>

namespace game_ideas
{
    public class InGamePause : MonoBehaviour
    {

        [Header("Script Reference")]
        public GameManager gameManager;
        public InGameUIManager inGameUIManager;
        public SoundManager soundManager;

        // call this function if game want to continue after game is pause
        public void ContinueGame()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            inGameUIManager.pause_btn.gameObject.SetActive(true); // display the hide pause button in game
            inGameUIManager.player_ui.gameObject.SetActive(true); // display the hide player ui's in game
            inGameUIManager.inGamePause.gameObject.SetActive(false); // hide the pause panel when in game
            gameManager.gameState = GameState.GAME_CONTINUE; // set the game state to game continue

        }

        public void WorldMapGame()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameManager.gameState = GameState.GAME_CONTINUE;
            SceneManager.LoadScene("MainMenu");

        }

    }
}
