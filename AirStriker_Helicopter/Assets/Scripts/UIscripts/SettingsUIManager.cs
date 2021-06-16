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

        [Header("Script Reference")]
        [SerializeField] private GameSettingsManager gameSettingsManager;
        [SerializeField] private SoundManager soundManager;

        // get the game settings data and update the settings ui's
        public void GetSettings()
        {
            // game graphics 
            if (lowGraphics_toggle != null && highGraphics_toggle != null)
            {
                if (gameSettingsManager.GetData().gameGraphics == GameGraphics.HIGH_GRAPHICS)
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
            if (gameSettingsManager.GetData().gameControls == GameControls.JOYSTICK_CONTROLS)
            {
                ControlsSelection(GameControls.JOYSTICK_CONTROLS);
            }
            else if (gameSettingsManager.GetData().gameControls == GameControls.BUTTON_CONTROLS)
            {
                ControlsSelection(GameControls.BUTTON_CONTROLS);
            }

            // in game ui style
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                StyleSelection(GameInUIStyle.ROUND);
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                StyleSelection(GameInUIStyle.TRANSPARENT);
            }
            else
            {
                StyleSelection(GameInUIStyle.SQUARE);
            }

            // game sound
            if (gameSettingsManager.GetData().music)
            {
                music_toggle.isOn = true;
            }
            else
            {
                music_toggle.isOn = false;
            }

            if (gameSettingsManager.GetData().soundFX)
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

            gameSettingsManager.GetData().gameGraphics = GameGraphics.LOW_GRAPHICS; // set graphics data
            GraphicsSelection(GameGraphics.LOW_GRAPHICS); // update graphics ui
        }

        public void GraphicsHighAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsManager.GetData().gameGraphics = GameGraphics.HIGH_GRAPHICS; // set graphics data
            GraphicsSelection(GameGraphics.HIGH_GRAPHICS); // update graphics ui
        }

        public void GraphicsSelection(GameGraphics graphicsSelection) // handles on and off toggle's for graphics
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

            gameSettingsManager.GetData().gameControls = GameControls.JOYSTICK_CONTROLS; // set controls data
            ControlsSelection(GameControls.JOYSTICK_CONTROLS); // update graphics ui
        }

        public void ControlsButtonsAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsManager.GetData().gameControls = GameControls.BUTTON_CONTROLS; // set controls data
            ControlsSelection(GameControls.BUTTON_CONTROLS); // update graphics ui
        }

        public void ControlsSelection(GameControls controlsSelection) // handles on or off toggle's for controls
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

            gameSettingsManager.GetData().gameInUIStyle = GameInUIStyle.SQUARE; // set style data
            StyleSelection(GameInUIStyle.SQUARE); // update graphics ui
        }

        public void StyleRoundAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsManager.GetData().gameInUIStyle = GameInUIStyle.ROUND; // set style data
            StyleSelection(GameInUIStyle.ROUND); // update graphics ui
        }

        public void StyleTransparentAction()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            gameSettingsManager.GetData().gameInUIStyle = GameInUIStyle.TRANSPARENT; // set style data
            StyleSelection(GameInUIStyle.TRANSPARENT); // update graphics ui
        }

        public void StyleSelection(GameInUIStyle styleSelection) // handles on or off toggle's for style
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
                gameSettingsManager.GetData().music = true;
            }
            else
            {
                gameSettingsManager.GetData().music = false;
            }
        }

        public void SoundSFXAction() // set soundFX data from game settings
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            if (soundFX_toggle.isOn)
            {
                gameSettingsManager.GetData().soundFX = true;
            }
            else
            {
                gameSettingsManager.GetData().soundFX = false;
            }
        }
    }
}
