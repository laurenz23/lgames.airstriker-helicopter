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
        
        public AdsPanel adsPanel; 
        public RewardPanel rewardPanel;
        private GameAssetsManager gameAssetsManager; // reference for our coins, diamonds and energy capsule sprites

        [HideInInspector] public GameTokens selectedTokens = GameTokens.NONE;

        private static TokensManager instance;

        public static TokensManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;

            gameAssetsManager = GameAssetsManager.GetInstance();
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
            rewardPanel.ShowRewarded("COIN REWARD", "YOU RECIEVED 100 COINS", gameAssetsManager.more_coin_icon);
        }

        public void RewardedDiamonds()
        {
            rewardPanel.ShowRewarded("SUPPLY REWARD", "YOU RECIEVED 10 DIAMONDS", gameAssetsManager.more_supply_icon);
        }

        public void RewardedEnergyCapsule()
        {
            rewardPanel.ShowRewarded("ENERGY CAPSULE REWARD", "YOU RECIEVED 1 ENERGY CAPSULE", gameAssetsManager.energy_capsule_icon);
        }
    }
}
