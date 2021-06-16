using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// handles the ui events and displaying of ads
/// </summary>

namespace game_ideas
{
    public class AdsPanel : MonoBehaviour
    {
        public SoundManager soundManager;
        public GameObject popupPanel;
        public GameObject adsPanel;
        public Text adsTitle_text;
        public Text adsDescription_text;

        private TokensManager tokensManager;

        private void Awake()
        {
            tokensManager = TokensManager.GetInstance();
        }

        public void ShowAds(string adsTitle, string adsDescription)
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            popupPanel.SetActive(true);
            adsPanel.SetActive(true);

            adsTitle_text.text = adsTitle;
            adsDescription_text.text = adsDescription;
        }

        public void HideAds()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            popupPanel.SetActive(false);
            adsPanel.SetActive(false);

            adsTitle_text.text = "";
            adsDescription_text.text = "";

            tokensManager.selectedTokens = GameTokens.NONE;
        }

        public void PlayAds()
        {
            //soundManager.soundFXHandler.SFX_COLLECT_COIN("coin2");

            if (tokensManager.selectedTokens == GameTokens.COINS)
            {
                tokensManager.RewardedCoins();
            }
            else if (tokensManager.selectedTokens == GameTokens.DIAMONDS)
            {
                tokensManager.RewardedDiamonds();
            }
            else if (tokensManager.selectedTokens == GameTokens.ENERGY_CAPSULE)
            {
                tokensManager.RewardedEnergyCapsule();
            }
        }

    }
}
