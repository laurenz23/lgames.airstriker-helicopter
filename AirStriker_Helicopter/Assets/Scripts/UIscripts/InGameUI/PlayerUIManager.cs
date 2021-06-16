using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// this is script is attached to ui manager object
/// handles the ui players like attack button, special attack buttons and events
/// </summary>

namespace game_ideas
{
    public class PlayerUIManager : MonoBehaviour
    {
        // player healh ui
        [Header("Player Health UI")]
        [SerializeField] private Gradient healthBar_gradient = null;

        // player energy ui
        [Header("Player Energy UI")]
        [SerializeField] private Slider playerEnergyBar_slider = null;

        [Header("Player Points UI")]
        [SerializeField] private TextMeshProUGUI points_text = null;

        // script reference
        [Header("Script Reference")]
        [SerializeField] private InGameUIDesign inGameUIDesign;

        private GameSettingsManager gameSettingsManager = null;
        private PlayerManager playerManager = null;

        // player movement controls
        private Transform joystick_movement = null;
        private Transform buttons_movement = null;

        // player health ui
        private Image healthBar_fill;

        // player attacks
        private Button attack_btn;
        private Button activeSkill1_btn;
        private Transform passiveSkill1_icon;
        private Transform passiveSkill2_icon;
        private Transform passiveSkill3_icon;

        // player attack fills
        private Image activeSkill1_fill;
        private Image passiveSkill1_fill;
        private Image passiveSkill2_fill;
        private Image passiveSkill3_fill;

        private void Awake()
        {
            gameSettingsManager = GameSettingsManager.GetInstance();
        }

        private void Start()
        {
            playerManager = FindObjectOfType<PlayerManager>();
        
            GetUIStyleGameControls();

            if (
                !playerManager.playerAttack.attackBasic &&
                !playerManager.playerAttack.attackPassive1 &&
                !playerManager.playerAttack.attackPassive2 &&
                !playerManager.playerAttack.attackPassive3
                )
            {
                activeSkill1_btn.gameObject.SetActive(false);
            }
            
            if (!playerManager.playerAttack.attackActive1)
            {
                activeSkill1_btn.gameObject.SetActive(false);
            }

            if (!playerManager.playerAttack.attackPassive1)
            {
                passiveSkill1_icon.gameObject.SetActive(false);
            }

            if (!playerManager.playerAttack.attackPassive2)
            {
                passiveSkill2_icon.gameObject.SetActive(false);
            }

            if (!playerManager.playerAttack.attackPassive3)
            {
                passiveSkill3_icon.gameObject.SetActive(false);
            }
            
        }
        
        // call this function to display the player movement controls
        public void GetUIStyleGameControls()
        {
            inGameUIDesign.GetUIStyle();

            // get the player game ui design
            joystick_movement = inGameUIDesign.GetMovementJoystickUI();
            buttons_movement = inGameUIDesign.GetMovementButtonsUI();

            // get the ui design
            healthBar_fill = inGameUIDesign.GetHealthBar_fill();
            attack_btn = inGameUIDesign.GetAttack_button();
            activeSkill1_btn = inGameUIDesign.GetActiveSkill1_button();
            passiveSkill1_icon = inGameUIDesign.GetPassiveSkill1_icon();
            passiveSkill2_icon = inGameUIDesign.GetPassiveSkill2_icon();
            passiveSkill3_icon = inGameUIDesign.GetPassiveSkill3_icon();

            // player armaments ui
            activeSkill1_fill = activeSkill1_btn.transform.GetChild(0).GetComponent<Image>();
            passiveSkill1_fill = passiveSkill1_icon.GetChild(0).GetComponent<Image>();
            passiveSkill2_fill = passiveSkill2_icon.GetChild(0).GetComponent<Image>();
            passiveSkill3_fill = passiveSkill3_icon.GetChild(0).GetComponent<Image>();

            if (gameSettingsManager.GetData().gameControls.Equals(GameControls.BUTTON_CONTROLS))
            {
                buttons_movement.gameObject.SetActive(true);
                joystick_movement.gameObject.SetActive(false);
            }
            else if (gameSettingsManager.GetData().gameControls.Equals(GameControls.JOYSTICK_CONTROLS))
            {
                joystick_movement.gameObject.SetActive(true);
                buttons_movement.gameObject.SetActive(false);
            }

            SetPlayerHealth_ui(playerManager.health);
        }

        // these methods are called at the player collider 
        // set the player health
        public void SetPlayerHealth_ui(float playerHealth)
        {

            healthBar_fill.fillAmount = playerHealth / playerManager.characterHealth; // compute the health to return the value between 0 and 1
            healthBar_fill.color = healthBar_gradient.Evaluate(healthBar_fill.fillAmount); // change the fill color base on the gradient color

        }

        // set player energy
        public void SetPlayerEnergy_ui(int playerEnergy)
        {
            
            playerEnergyBar_slider.value = playerEnergy;

        }

        // set player points
        public void SetPlayerPoints_ui(int playerPoints)
        {

            points_text.text = playerPoints.ToString("0000000000");

        }

        // these methods are called at the armaments scripts
        public void ActiveSkill1_uiCooldown(bool isCooldown, float cooldown, float currentCooldown)
        {

            UICooldown(activeSkill1_fill, isCooldown, cooldown, currentCooldown);

        }

        public void PassiveSkill1_uiCooldown(bool isCooldown, float cooldown, float currentCooldown)
        {

            UICooldown(passiveSkill1_fill, isCooldown, cooldown, currentCooldown);

        }

        public void PassiveSkill2_uiCooldown(bool isCooldown, float cooldown, float currentCooldown)
        {

            UICooldown(passiveSkill2_fill, isCooldown, cooldown, currentCooldown);

        }

        public void PassiveSkill3_uiCooldown(bool isCooldown, float cooldown, float currentCooldown)
        {

            UICooldown(passiveSkill3_fill, isCooldown, cooldown, currentCooldown);

        }
        
        // function for ui armament cooldown
        private void UICooldown(Image fillImage, bool isCooldown, float cooldown, float currentCooldown)
        {

            // get the exact value between 0 to 1 by dividing current cooldown and cooldown
            currentCooldown = currentCooldown / cooldown;

            // check if current cooldown is already zero or less than zero
            if (currentCooldown <= 0f)
            {
                // set the default value of fill value 
                fillImage.fillAmount = 0f;
            }
            else
            {
                // set the current cooldown value to fill amount to have a ui animation cooldown
                fillImage.fillAmount = currentCooldown;
            }

        }

    }
}
