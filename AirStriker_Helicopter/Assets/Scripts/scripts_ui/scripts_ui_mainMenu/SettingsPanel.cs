using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this script is attached to settins panel ui
/// handles the settings panel functions such as display and hide of panel and etc.
/// </summary>

namespace game_ideas
{
    public class SettingsPanel : MonoBehaviour
    {

        [Header("Panels")]
        public GameObject popup_panel;
        public GameObject settings_panel;
        public GameObject enterName_panel;

        [Header("Script Reference")]
        [SerializeField] private SettingsUIManager settingsUIManager;
        [SerializeField] private SoundManager soundManager;

        public void ShowSettings()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");
            
            settingsUIManager.GetSettings();
            popup_panel.SetActive(true);
            settings_panel.SetActive(true);
        }

        public void HideSettigs()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            settings_panel.SetActive(false);
            popup_panel.SetActive(false);
        }

        // display enter name panel and hide this panel temporarily
        public void ShowEnterName()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            settings_panel.SetActive(false);
            enterName_panel.SetActive(true);
        }

    }
}
