using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// usage:      attached this script to ui level complete panel
/// functions:  handles in game level complete ui events
///             and count effect of rewards
/// </summary>

namespace game_ideas
{
    public class InGameLevelComplete : MonoBehaviour
    {

        [Header("Level Complete UI")]
        [SerializeField] private Text playerLevel_text;
        [SerializeField] private Text score_text;
        [SerializeField] private Text diamonds_text;
        [SerializeField] private Text coins_text;
        [SerializeField] private Image playerLevel_img;
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
            if (diamonds != collectedDiamonds && diamonds < collectedDiamonds) 
            {
                diamonds += growthRate; // keep increasing until diamonds is equal to collectedDiamonds

                diamonds_text.text = diamonds.ToString("##0"); // update text value of diamonds
            }

            if (coins != collectedCoins && coins < collectedCoins)
            {
                coins += growthRate; // keep increasing until coins is equal to collectedCoins

                coins_text.text = coins.ToString("###0"); // update text value of coins
            }
        }
        
        public void LevelComplete()
        {
            playerLevel_text.text = "LEVEL " + profilePlayerDataManager.profilePlayerData.playerLevel;
            score_text.text = profilePlayerDataManager.profileScoreData.score.ToString("0000000000");
        }

        // call this method to duplicate rewards
        public void X2Rewards()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            ReceiveRewards();
        }

        // call this method to receive rewards
        public void ReceiveRewards()
        {
            soundManager.soundFXHandler.SFX_COLLECT_COIN("coin1");
            
            // hide duplicate button so player can't duplicate rewards again and again
            x2_btn.SetActive(false);

            // multiply tokens
            collectedDiamonds *= 2; 
            collectedCoins *= 2;

            // update in game tokens ui
            diamonds_text.text = collectedDiamonds.ToString("##0");
            coins_text.text = collectedCoins.ToString("###0");

            // update tokens and save datas
            profilePlayerDataManager.SetDiamondsData(profilePlayerDataManager.profileTokensData.playerDiamonds + collectedDiamonds);
            profilePlayerDataManager.SetCoinsData(profilePlayerDataManager.profileTokensData.playerCoins + collectedCoins);
        }

        public void NextLevel()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            loadSceneManager.LoadScene("MainMenu");
        }
        
    }
}
