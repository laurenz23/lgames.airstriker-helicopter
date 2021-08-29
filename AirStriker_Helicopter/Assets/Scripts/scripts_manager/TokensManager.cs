using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GOAL: this script handles the ads and rewards panel also the player rewards when ads is completed
/// USAGE: attached this script to TokensManager object or create a TokensManager if object is not exist
/// OPTIONAL: 
/// </summary>

namespace game_ideas
{
    public enum GameTokens
    {
        NONE,
        COINS,
        DIAMONDS,
        ENERGY_CAPSULE
    }

    public class TokensManager : MonoBehaviour
    {

        [SerializeField] private AdsPanel adsPanel;
        [SerializeField] private RewardPanel rewardPanel;

        [Header("Script Reference")]
        [SerializeField] private GameAssetsManager gameAssetsManager; // reference for our coins, diamonds and energy capsule sprites
        [SerializeField] private ProfilePlayerDataManager profilePlayerDataManager;
        [SerializeField] private MainMenuUIHandler mainMenuUIHandler;

        [HideInInspector] public GameTokens selectedTokens = GameTokens.NONE;

        private const int rewardCoins = 100;
        private const int rewardDiamonds = 10;
        private const int rewardDeploymentCapsule = 1;

        private static TokensManager instance;

        public static TokensManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }

        public void GetMoreCoins()
        {
            adsPanel.ShowAds("GET COINS", "WATCH VIDEO ADS TO GET MORE COINS");
            selectedTokens = GameTokens.COINS;
        }

        public void GetMoreDiamonds()
        {
            adsPanel.ShowAds("GET DIAMONDS", "WATCH VIDEO ADS TO GET MORE DIAMONDS");
            selectedTokens = GameTokens.DIAMONDS;
        }

        public void GetMoreEnergyCapsule()
        {
            adsPanel.ShowAds("GET ENERGY CAPSULE", "WATCH VIDEO ADS TO GET MORE ENERGY CAPSULE");
            selectedTokens = GameTokens.ENERGY_CAPSULE;
        }

        public void RewardedCoins()
        {
            profilePlayerDataManager.SetCoinsData(profilePlayerDataManager.profileTokensData.playerCoins + rewardCoins);
            mainMenuUIHandler.SetPlayerTokens();
            rewardPanel.ShowRewarded("COIN REWARD", "YOU RECIEVED 100 COINS", gameAssetsManager.more_coin_icon);
        }

        public void RewardedDiamonds()
        {
            profilePlayerDataManager.SetDiamondsData(profilePlayerDataManager.profileTokensData.playerDiamonds + rewardDiamonds);
            mainMenuUIHandler.SetPlayerTokens();
            rewardPanel.ShowRewarded("DIAMOND REWARD", "YOU RECIEVED 10 DIAMONDS", gameAssetsManager.more_supply_icon);
        }

        public void RewardedEnergyCapsule()
        {
            profilePlayerDataManager.SetDeploymentCapsuleData(profilePlayerDataManager.profileTokensData.playerDeploymentCapsule + rewardDeploymentCapsule);
            mainMenuUIHandler.SetPlayerDeploymentCapsule();
            mainMenuUIHandler.SetPlayButton();
            rewardPanel.ShowRewarded("ENERGY CAPSULE REWARD", "YOU RECIEVED 1 ENERGY CAPSULE", gameAssetsManager.energy_capsule_icon);
        }
    }
}
