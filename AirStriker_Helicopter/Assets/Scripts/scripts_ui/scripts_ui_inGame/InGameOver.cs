using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// usage:      
/// functions:  
/// handles in game ui for game over events
/// </summary>

namespace game_ideas
{
    public class InGameOver : MonoBehaviour
    {

        [Header("Gameover UI")]
        [SerializeField] private Text playerLevel_text;
        [SerializeField] private Text score_text;
        [SerializeField] private Text diamonds_text;
        [SerializeField] private Text coins_text;
        [SerializeField] private Image playerLevelFill_img;
        [SerializeField] private GameObject x2_btn;

        [Header("Script Reference")]
        public InGameUIManager inGameUIManager;

        private SoundManager soundManager;
        private LoadSceneManager loadSceneManager;
        private ProfilePlayerDataManager profilePlayerDataManager;
        private PlayerManager playerManager;
        
        private int diamonds = 0; // store value of diamonds before game level started
        private int coins = 0; // store value of coins before game level started

        private int growthRate = 1; // reference of how much number will increase(speed)
        private int collectedDiamonds; // store value of diamonds after the game
        private int collectedCoins; // store value of coins after the game

        private void Awake()
        {
            soundManager = SoundManager.GetInstance();
            loadSceneManager = LoadSceneManager.GetInstance();
            profilePlayerDataManager = ProfilePlayerDataManager.GetInstance();
            playerManager = PlayerManager.GetInstance();
        }

        private void Start()
        {
            collectedDiamonds = playerManager.diamonds;
            collectedCoins = playerManager.coins;
        }

        private void Update()
        {
            if (diamonds != collectedDiamonds && diamonds < collectedDiamonds) // keep increasing
            {
                diamonds += growthRate;

                diamonds_text.text = diamonds.ToString("##0");
            }

            if (coins != collectedCoins && coins < collectedCoins) // keep increasing
            {
                coins += growthRate;

                coins_text.text = coins.ToString("###0");
            }
        }

        public void GameOver()
        {
            playerLevel_text.text = "LEVEL " + profilePlayerDataManager.profilePlayerData.playerLevel;
            score_text.text = profilePlayerDataManager.profileScoreData.score.ToString("0000000000");
        }

        public void X2Rewards()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            ReceivedRewards();
        }
        
        public void ReceivedRewards()
        {
            soundManager.soundFXHandler.SFX_COLLECT_COIN("coin1");

            x2_btn.SetActive(false);

            collectedDiamonds *= 2;
            collectedCoins *= 2;

            profilePlayerDataManager.SetDiamondsData(profilePlayerDataManager.profileTokensData.playerDiamonds + collectedDiamonds);
            profilePlayerDataManager.SetCoinsData(profilePlayerDataManager.profileTokensData.playerCoins + collectedCoins);
        }
        
        public void RetryGame()
        {
            loadSceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void WorldMapGame()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            loadSceneManager.LoadScene("MainMenu");
        }

    }
}
