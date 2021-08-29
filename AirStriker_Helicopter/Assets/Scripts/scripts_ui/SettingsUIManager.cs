using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GOAL:   all data sets for game settings will go through here
///         and handles the game settings ui from panel
/// USAGE:  attached the script to settings ui manager object or create a new one if object doesn't exist.
///         attached the ui that this class required.
/// OPTIONS:
/// </summary>

namespace game_ideas
{
    public class SettingsUIManager : MonoBehaviour
    {

        [Header("Graphics Reference")]
        [SerializeField] private Toggle lowGraphics_toggle = null;
        [SerializeField] private Toggle highGraphics_toggle = null;

        [Header("Controls Reference")]
        [SerializeField] private Toggle joystick_toggle = null;
        [SerializeField] private Toggle buttons_toggle = null;

        [Header("In Game Style Reference")]
        [SerializeField] private Toggle square_toggle = null;
        [SerializeField] private Toggle round_toggle = null;
        [SerializeField] private Toggle transparent_toggle = null;

        [Header("Sound Reference")]
        [SerializeField] private Toggle music_toggle = null;
        [SerializeField] private Toggle soundFX_toggle = null;

        private GameSettingsData gameSettingsData = new GameSettingsData();
        private DataManager dataManager;
        private SoundManager soundManager;

        private void Awake()
        {
            dataManager = DataManager.GetInstance();
            soundManager = SoundManager.GetInstance();
        }

        // get the game settings data and update the settings ui's
        public void GetSettings()
        {
            gameSettingsData = dataManager.gameSettingsData;

            // game graphics 
            if (lowGraphics_toggle != null && highGraphics_toggle != null)
            {
                if (gameSettingsData.gameGraphics == GameGraphics.HIGH_GRAPHICS.ToString())
                {
                    GraphicsSelection(GameGraphics.HIGH_GRAPHICS);
                }
                else
                {
                    GraphicsSelection(GameGraphics.LOW_GRAPHICS);
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("Graphics option cannot be use. Attached low graphics and high graphics toggle to enabled the option.");
#endif
            }

            // game controls
            if (gameSettingsData.gameControls == GameControls.JOYSTICK_CONTROLS.ToString())
            {
                ControlsSelection(GameControls.JOYSTICK_CONTROLS);
            }
            else if (gameSettingsData.gameControls == GameControls.BUTTON_CONTROLS.ToString())
            {
                ControlsSelection(GameControls.BUTTON_CONTROLS);
            }

            // in game ui style
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                StyleSelection(GameInUIStyle.ROUND);
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
            {
                StyleSelection(GameInUIStyle.TRANSPARENT);
            }
            else
            {
                StyleSelection(GameInUIStyle.SQUARE);
            }

            // game sound
            if (gameSettingsData.music)
            {
                music_toggle.isOn = true;
            }
            else
            {
                music_toggle.isOn = false;
            }

            if (gameSettingsData.soundFX)
            {
                soundFX_toggle.isOn = true;
            }
            else
            {
                soundFX_toggle.isOn = false;
            }
        }

        // settings graphics ----------------------
        // set graphics data and update graphics toggle
        public void GraphicsLowAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsData.gameGraphics = GameGraphics.LOW_GRAPHICS.ToString(); // set graphics data
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data graphics to high graphics
        }

        public void GraphicsHighAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsData.gameGraphics = GameGraphics.HIGH_GRAPHICS.ToString(); // set graphics data
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data graphics to low graphics
        }

        public void GraphicsSelection(GameGraphics graphicsSelection) // handles on and off toggle's for graphics when loaded data
        {
            if (graphicsSelection == GameGraphics.HIGH_GRAPHICS)
            {
                lowGraphics_toggle.isOn = false;
                highGraphics_toggle.isOn = true;
            }
            else if (graphicsSelection == GameGraphics.LOW_GRAPHICS)
            {
                lowGraphics_toggle.isOn = true;
                highGraphics_toggle.isOn = false;
            }
        }

        // settings controls -----------------------
        // set controls data and update controls toggle
        public void ControlsJoystickAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsData.gameControls = GameControls.JOYSTICK_CONTROLS.ToString(); // set controls data
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data control to joystick
        }

        public void ControlsButtonsAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsData.gameControls = GameControls.BUTTON_CONTROLS.ToString(); // set controls data
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data controls to button
        }

        public void ControlsSelection(GameControls controlsSelection) // handles on or off toggle's for controls when loaded data
        {
            if (controlsSelection == GameControls.JOYSTICK_CONTROLS)
            {
                joystick_toggle.isOn = true;
                buttons_toggle.isOn = false;
            }
            else if (controlsSelection == GameControls.BUTTON_CONTROLS)
            {
                joystick_toggle.isOn = false;
                buttons_toggle.isOn = true;
            }
        }

        // settings in game style -------------------
        public void StyleSquareAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsData.gameInUIStyle = GameInUIStyle.SQUARE.ToString(); // set style data
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data style to square
        }

        public void StyleRoundAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsData.gameInUIStyle = GameInUIStyle.ROUND.ToString(); // set style data
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data style to round
        }

        public void StyleTransparentAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsData.gameInUIStyle = GameInUIStyle.TRANSPARENT.ToString(); // set style data
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data style to transparent
        }

        public void StyleSelection(GameInUIStyle styleSelection) // handles on or off toggle's for style when loaded data
        {
            if (styleSelection == GameInUIStyle.ROUND)
            {
                square_toggle.isOn = false;
                round_toggle.isOn = true;
                transparent_toggle.isOn = false;
            }
            else if (styleSelection == GameInUIStyle.TRANSPARENT)
            {
                square_toggle.isOn = false;
                round_toggle.isOn = false;
                transparent_toggle.isOn = true;
            }
            else
            {
                square_toggle.isOn = true;
                round_toggle.isOn = false;
                transparent_toggle.isOn = false;
            }
        }

        // settings sound ---------------------------
        public void SoundMusicAction() // set sound music data from game settings
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            if (music_toggle.isOn)
            {
                gameSettingsData.music = true;
            }
            else
            {
                gameSettingsData.music = false;
            }

            soundManager.SetMusic();
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data music
        }

        public void SoundSFXAction() // set soundFX data from game settings
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            if (soundFX_toggle.isOn)
            {
                gameSettingsData.soundFX = true;
            }
            else
            {
                gameSettingsData.soundFX = false;
            }

            soundManager.SetSoundFX();
            dataManager.SaveGameSettingsData(gameSettingsData); // update game settings data sound fx
        }

    }
}
