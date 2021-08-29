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
        [SerializeField] private Image level_image;
        [SerializeField] private Text diamonds_text;
        [SerializeField] private Text coins_text;
        [SerializeField] private TextMeshProUGUI score_text;

        [Header("Unit UI Reference")]
        [SerializeField] private TextMeshProUGUI characterName_text;
        [SerializeField] private TextMeshProUGUI characterDescription_text;
        [SerializeField] private Image characterHealth_image;
        [SerializeField] private Image characterSpeed_image;
        [SerializeField] private Image characterDamage_image;
        [SerializeField] private Image characterPassive_image;

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
        private void SetPlayerProfile()
        {
            profilePlayerData = dataManager.profilePlayerData; // load profile player data from data manager profile player data
            playerName_text.text = profilePlayerData.playerName.ToUpper(); // set player name text. upper text is the supported type of font style
            level_text.text = "LEVEL " + profilePlayerData.playerLevel.ToString(); // set player level
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

        // NOTE: COMPUTATION IS CURRENTLY TEMPORARY
        // call this function to update the value of ui of unit data
        public void SetUnitData(int index)
        {
            characterName_text.text = dataManager.listGameUnitData[index]._name.ToUpper(); // set unit name 
            characterDescription_text.text = dataManager.listGameUnitData[index].description.ToUpper(); // set unit description
            characterHealth_image.fillAmount = dataManager.listGameUnitData[index].health * 0.001f; // set unit health amount
            characterSpeed_image.fillAmount = dataManager.listGameUnitData[index].speed * 0.1f; // set unit speed amount 
            characterDamage_image.fillAmount = dataManager.listGameUnitData[index].dmgS * 0.001f; // set unit damage amount
            characterPassive_image.fillAmount = 0.9f; // set unit passive amount
        }

        public void ShowPopupMessage(string message)
        {
            // we add if statement to avoid bug of reactivation even the message is still activated
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
