using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// usage:  reference of ui in main menu
///         handles the manipulation of ui in main menu
///         and display data or update ui values of main menu
///         NOTE: armory have a dedicated script to handle it's own ui
/// </summary>
namespace game_ideas
{
    public class MainMenuUIHandler : MonoBehaviour
    {
        [Header("Player UI References")]
        [SerializeField] private TextMeshProUGUI playerName_text;
        [SerializeField] private TextMeshProUGUI level_text;
        [SerializeField] private Slider level_slider;
        [SerializeField] private Text diamonds_text;
        [SerializeField] private Text coins_text;
        [SerializeField] private TextMeshProUGUI score_text;

        [Header("Unit UI Reference")]
        [SerializeField] private TextMeshProUGUI characterName_text;
        [SerializeField] private TextMeshProUGUI characterDescription_text;
        [SerializeField] private Slider characterHealth_slider;
        [SerializeField] private Slider characterSpeed_slider;
        [SerializeField] private Slider characterDamage_slider;
        [SerializeField] private Slider characterPassive_slider;
        [SerializeField] private Button characterPrev_button;
        [SerializeField] private Button characterNext_button;

        [Header("Mission Group Reference")]
        [SerializeField] private Image mission_panel;
        [SerializeField] private GameObject missionPointer_obj;
        [SerializeField] private GameObject invalidMission_btn;
        [SerializeField] private GameObject mission_btn;
        [SerializeField] private Sprite blue_btn_effect;
        [SerializeField] private Sprite red_btn_effect;

        [Header("Map UI Reference")]
        [SerializeField] private Button start_btn;
        [SerializeField] private Button invalid_start_btn;
        [SerializeField] private Image energyCapsule_fill;
        [SerializeField] private GameObject start_pointer_object;
        [SerializeField] private GameObject invalid_start_pointer_object;
        [SerializeField] private Image start_img;
        [SerializeField] private Sprite defaultStart_sprite;
        [SerializeField] private Sprite invalidStart_sprite;
        [SerializeField] private Image[] arrayDeploymentCapsule = new Image[10];

        [Header("Popup Text References")]
        [SerializeField] private GameObject popup_text_gObject;
        [SerializeField] private TextMeshProUGUI popup_message_tmp;

        [Header ("Script Reference")]
        public DataManager dataManager;
        public ProfilePlayerDataManager profilePlayerDataManager;
        [SerializeField] private UnitArmoryManager unitArmoryManager;
        
        private ProfilePlayerData profilePlayerData = new ProfilePlayerData();
        private ProfileScoreData profileScoreData = new ProfileScoreData();
        private ProfileTokensData profileTokensData = new ProfileTokensData();

        private void Start()
        {
            SetPlayerProfile(); // display player profile to main menu ui
            SetPlayerScore(); // display player score data to ui
            SetPlayerTokens(); // display player tokens data to ui
            SetPlayerDeploymentCapsule(); // display player deployment capsule data to ui
            SetUnitData(unitArmoryManager.GetUnitIndex()); // display information of the selected unit data to ui
            SetPlayButton(); // display the button? play button or error button
        }

        // call this function to update the value of ui player profile: name and level
        public void SetPlayerProfile()
        {
            profilePlayerData = dataManager.profilePlayerData; // load profile player data from data manager profile player data
            int level = profilePlayerData.playerLevel;
            int score = dataManager.profileScoreData.score;
            playerName_text.text = profilePlayerData.playerName.ToUpper(); // set player name text. upper text is the supported type of font style
            level_slider.minValue = profilePlayerDataManager.GetPlayerLevelValue(level - 1);
            level_slider.maxValue = profilePlayerDataManager.GetPlayerLevelValue(level);
            level_slider.value = score;
            level_text.text = "LEVEL " + level.ToString(); // set player level
        } 

        // call this function to update the value of ui player score
        public void SetPlayerScore()
        {
            profileScoreData = dataManager.profileScoreData; // load player score data from data manager profile score data
            score_text.text = "SCORE " + profileScoreData.score.ToString("###0000000000"); // set player score
        }

        // call this function to update the value of ui player tokens
        public void SetPlayerTokens()
        {
            profileTokensData = dataManager.profileTokensData; // load player tokens data from data manager profile tokens data
            diamonds_text.text = "X" + profileTokensData.playerDiamonds.ToString("######0"); // set player diamonds
            coins_text.text = "X" + profileTokensData.playerCoins.ToString("######0"); // set player coins
        }

        // call this function to update the value of ui player deployment capsule
        public void SetPlayerDeploymentCapsule()
        {
            int deploymentCapsule = dataManager.profileTokensData.playerDeploymentCapsule; // load player deployment capsule data from data manager profile deployment capsule

            for (int x = 0; x < arrayDeploymentCapsule.Length; x++) // get the number of deployment capsule image referenced
            {
                if (x > (deploymentCapsule - 1)) // if deployment capsule is less than to the number of deployment array then
                {
                    // add a transparent color effect to deployment capsule image, as a visual representation of how many capsule don't have
                    arrayDeploymentCapsule[x].color = new Color32(255, 255, 255, 50); 
                }
                else // if deployment capsule is greater than to or equal to the number of deployment array then
                {
                    // the color of deployment capsule image is default, as a visual representation of how may does have 
                    arrayDeploymentCapsule[x].color = new Color32(255, 255, 255, 255); 
                }
            }
        }

        // set or update mission button group ui
        public void SetMissionButton(bool value)
        {
            missionPointer_obj.SetActive(value);

            if (value) // player can start a mission
            {
                mission_panel.sprite = blue_btn_effect;
                mission_btn.SetActive(true);
                invalidMission_btn.SetActive(false);
            }
            else 
            {
                mission_panel.sprite = red_btn_effect;
                mission_btn.SetActive(false);
                invalidMission_btn.SetActive(true);
            }
        }

        // call this function to update the ui of play button
        public void SetPlayButton()
        {
            int deploymentCapsule = dataManager.profileTokensData.playerDeploymentCapsule; // get how many deployment capsule player have

            if (deploymentCapsule > 0) // player have deployment capsule
            {
                start_img.sprite = defaultStart_sprite; // set start background sprite to default background start sprite
                start_btn.gameObject.SetActive(true); // show start play button 
                start_pointer_object.SetActive(true); // show pointer, pointing at start play button
                invalid_start_btn.gameObject.SetActive(false); // hide error start play button
                invalid_start_pointer_object.SetActive(false); // hide pointer, pointing at not enough deployment capsule
            }
            else // player don't have deployment capsule
            {
                start_img.sprite = invalidStart_sprite; // set start background sprite to error background start sprite
                start_btn.gameObject.SetActive(false); // hide start play button
                start_pointer_object.SetActive(false); // hide pointer, pointing at start play button
                invalid_start_btn.gameObject.SetActive(true); // show error start play button
                invalid_start_pointer_object.SetActive(true); // show pointer, pointing at not enought deployment capsule
            }
        }

        // hide and show of previous and next buttons
        // this method is called at UnityArmoryManager class
        // whenever the main menu scene is open it will display the button
        public void DisplayCharacterNavigationButton(bool isDisplayPrev, bool isDisplayNext)
        {
            characterPrev_button.gameObject.SetActive(isDisplayPrev);
            characterNext_button.gameObject.SetActive(isDisplayNext);
        }

        // NOTE: COMPUTATION IS CURRENTLY TEMPORARY
        // call this function to update the value of ui of unit data
        int playerLevel = 0;

        public void SetUnitData(int index)
        {
            playerLevel = profilePlayerData.playerLevel;

            characterName_text.text = dataManager.listGameUnitData[index]._name.ToUpper(); // set unit name 
            characterDescription_text.text = dataManager.listGameUnitData[index].description.ToUpper(); // set unit description

            characterHealth_slider.value = dataManager.listGameUnitData[index].health + AddStat(50); // set unit health amount
            characterSpeed_slider.value = dataManager.listGameUnitData[index].speed; // set unit speed amount 
            characterDamage_slider.value = dataManager.listGameUnitData[index].dmgS; // set unit damage amount
            characterPassive_slider.value = dataManager.listGameUnitData[index].passive + AddStat(1); // set unit passive amount
        }

        private int AddStat(int value)
        {
            if (playerLevel > 1)
            {
                return value * playerLevel;
            }

            return 0;
        }

        public void ShowPopupMessage(string message)
        {
            // we add "if statement" to avoid bug of reactivation even the message is still activated
            if (!popup_text_gObject.activeSelf) // if popup text object is deactivated, display the message
            {
                popup_text_gObject.SetActive(true); // display the message panel
                popup_message_tmp.text = message.ToUpper(); // set the message text
                StartCoroutine(HidePopupMessage()); // start the coroutine to hide message panel
            }
        }

        // error popup message delay
        IEnumerator HidePopupMessage()
        {
            yield return new WaitForSeconds(1f); // delay time
            popup_text_gObject.SetActive(false); // hide message panel
            StopCoroutine(HidePopupMessage()); // stop the started coroutine
        }

    }
}
