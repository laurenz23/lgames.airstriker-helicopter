using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// usage:      attached this script to enterName_panel object
/// functions:  handles player name, by creating player name the game will generate
///             player profile data
/// </summary>
namespace game_ideas
{
    public class EnterNamePanel : MonoBehaviour
    {

        [SerializeField] private TMP_InputField enterName_inputField;
        [SerializeField] private TMP_Text invalidInput_text;
        [SerializeField] private Button close_btn;
        [SerializeField] private GameObject popup_panel;
        [SerializeField] private GameObject enterName_panel;
        [SerializeField] private GameObject settings_panel;

        [Header("Script Refernce")]
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private ProfilePlayerDataManager profilePlayerDataManager;
        [SerializeField] private MainMenuUIHandler mainMenuUIHandler;
        [SerializeField] private MainMenuSwitchesHandler mainMenuSwitchesHandler;

        private void Start()
        {
            enterName_inputField.characterLimit = 16; // limit character value
            // each input
            enterName_inputField.onValidateInput += delegate (string input, int charIndex, char addedChar) {
                invalidInput_text.text = "";
                return nameValidation(addedChar); // validate first the inputed character
            };
        }

        // validate the inputed character and return only letters value
        private char nameValidation(char c)
        {
            c = char.ToUpper(c);
            return char.IsLetter(c) ? c : '\0';
        }

        // call this function to save the inputed name
        public void Submit()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            if (enterName_inputField.text != "") // player have inputed something
            {
                profilePlayerDataManager.SetPlayerName(enterName_inputField.text); // save player name data
                mainMenuUIHandler.SetPlayerProfile(); // set player name to ui
                mainMenuSwitchesHandler.DisplayMainMenu(); // display the main menu ui
                ShowPanel(false); // hide this panel
                // reset values
                enterName_inputField.text = ""; 
                invalidInput_text.text = "";
            }
            else // player don't have inputs
            {
                invalidInput_text.text = "PLEASE INPUT YOUR NAME";
            }
        }

        public void ShowPanel(bool show)
        {
            // hide player button if don't have player name
            // it is also required for our data
            if (!profilePlayerDataManager.HavePlayerProfile()) 
            {
                close_btn.gameObject.SetActive(false);
            }
            else // display the close button since already have player name in data
            {
                close_btn.gameObject.SetActive(true);
            }
            
            popup_panel.SetActive(show); // display the popup panel effect 
            enterName_panel.SetActive(show); // display this panel
        }

        // call this function if the panel is open at settings panel
        public void ClosePanel()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click3");

            // reset data when closing this panel
            enterName_inputField.text = ""; 
            invalidInput_text.text = "";

            // close this panel and display the settings panel after being close by opening this
            enterName_panel.SetActive(false);
            settings_panel.SetActive(true);
        }

    }
}
