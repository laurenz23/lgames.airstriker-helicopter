using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// handles the ui rewards
/// usage: call show reward method to display the reward after the ad and input the parameters required by method
/// </summary>

namespace game_ideas
{
    public class RewardPanel : MonoBehaviour
    {

        public SoundManager soundManager;
        public GameObject popupPanel;
        public GameObject rewardPanel;
        public GameObject adsPanel;
        public Text rewardTitle_text;
        public Text rewardDescription_text;
        public Image reward_icon;

        private TokensManager tokensManager;

        private void Start()
        {
            tokensManager = TokensManager.GetInstance();
        }

        public void ShowRewarded(string rewardTitle, string rewardDescription, Sprite rewardSprite)
        {
            soundManager.soundFXHandler.SFX_COLLECT_COIN("coin3");

            popupPanel.SetActive(true);
            rewardPanel.SetActive(true);
            adsPanel.SetActive(false);

            rewardTitle_text.text = rewardTitle;
            rewardDescription_text.text = rewardDescription;
            reward_icon.sprite = rewardSprite;
        }

        public void HideRewarded()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            popupPanel.SetActive(false);
            rewardPanel.SetActive(false);

            rewardTitle_text.text = "";
            rewardDescription_text.text = "";
            reward_icon.sprite = null;

            tokensManager.selectedTokens = GameTokens.NONE;
        }

    }
}
