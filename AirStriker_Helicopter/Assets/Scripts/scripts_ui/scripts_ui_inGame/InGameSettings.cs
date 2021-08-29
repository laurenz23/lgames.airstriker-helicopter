using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles in game ui for settings events
/// </summary>

namespace game_ideas
{
    public class InGameSettings : MonoBehaviour
    {
        [Header("Panels")]
        public GameObject inGameSettings_panel;

        [Header("Script Reference")]
        [SerializeField] private InGameUIManager inGameUIManager;
        [SerializeField] private SettingsUIManager settingsUIManager;
        [SerializeField] private PlayerUIManager playerUIManager;
        private SoundManager soundManager;

        private void Awake()
        {
            soundManager = inGameUIManager.soundManager;
        }

        public void ShowSettings()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            settingsUIManager.GetSettings();
            inGameSettings_panel.SetActive(true);
        }

        public void CloseSettings()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            playerUIManager.GetUIStyleGameControls();
            inGameSettings_panel.SetActive(false);
        }
    }
}
