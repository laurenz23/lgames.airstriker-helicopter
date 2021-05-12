using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this is script is attached to ui manager object
/// handles the ui for players like attack button, special attack buttons
/// </summary>

namespace game_ideas
{
    public class PlayerUIManager : MonoBehaviour
    {

        [Header("Player Movements")]
        [SerializeField] private Transform joystick_movement = null;
        [SerializeField] private Transform buttons_movement = null;

        // player healh ui
        [Header("Player Health UI")]
        [SerializeField] private Slider playerHealthBar_slider = null;
        [SerializeField] private Gradient healthBar_gradient = null;
        [SerializeField] private Image healthBar_fill = null;

        // player energy ui
        [Header("Player Energy UI")]
        [SerializeField] private Slider playerEnergyBar_slider = null;

        [Header("Player Points UI")]
        [SerializeField] private Text points_text = null;

        // player attacks
        [Header("Player Attack UI")]
        [SerializeField] private Button basicAttack_btn = null;
        [SerializeField] private Button automicAttack_btn = null;
        [SerializeField] private Transform straightMissile_icon = null;
        [SerializeField] private Transform dropBomb_icon = null;
        [SerializeField] private Transform guidedMissile_icon = null;

        // script reference
        [Header("Script Reference")]
        [SerializeField] private GameManager gameManager;
        [HideInInspector] private PlayerManager playerManager = null;

        private Image straightMissile_fill;
        private Image dropBomb_fill;
        private Image guidedMissile_fill;
        private Image automicBomb_fill;

        private void Start()
        {
            playerManager = FindObjectOfType<PlayerManager>();

            // player armaments ui
            straightMissile_fill = straightMissile_icon.GetChild(0).GetComponent<Image>();
            dropBomb_fill = dropBomb_icon.GetChild(0).GetComponent<Image>();
            guidedMissile_fill = guidedMissile_icon.GetChild(0).GetComponent<Image>();
            automicBomb_fill = automicAttack_btn.transform.GetChild(0).GetComponent<Image>();

            if (
                !playerManager.playerAttack.gatling_armament &&
                !playerManager.playerAttack.missile_armament &&
                !playerManager.playerAttack.dropMissile_armament &&
                !playerManager.playerAttack.guidedMissile_armament
                )
            {
                basicAttack_btn.gameObject.SetActive(false);
            }

            if (!playerManager.playerAttack.missile_armament)
            {
                straightMissile_icon.gameObject.SetActive(false);
            }

            if (!playerManager.playerAttack.dropMissile_armament)
            {
                dropBomb_icon.gameObject.SetActive(false);
            }

            if (!playerManager.playerAttack.guidedMissile_armament)
            {
                guidedMissile_icon.gameObject.SetActive(false);
            }
            
            if (!playerManager.playerAttack.automic_armament)
            {
                automicAttack_btn.gameObject.SetActive(false);
            }

        }

        public void SelectGameControls(GameControls selectControls)
        {
            if (selectControls.Equals(GameControls.BUTTON_CONTROLS))
            {
                buttons_movement.gameObject.SetActive(true);
                joystick_movement.gameObject.SetActive(false);
                return;
            }

            if (selectControls.Equals(GameControls.JOYSTICK_CONTROLS))
            {
                joystick_movement.gameObject.SetActive(true);
                buttons_movement.gameObject.SetActive(false);
                return;
            }
        }

        // these methods are called at the player collider 
        // set the player health
        public void SetPlayerHealth_ui(int playerHealth)
        {

            playerHealthBar_slider.value = playerHealth;

            healthBar_fill.color = healthBar_gradient.Evaluate(playerHealthBar_slider.normalizedValue); // change the fill color base on the gradient color

        }

        // set player energy
        public void SetPlayerEnergy_ui(int playerEnergy)
        {

            playerEnergyBar_slider.value = playerEnergy;

        }

        // set player points
        public void SetPlayerPoints_ui(int playerPoints)
        {

            points_text.text = playerPoints + " Points";

        }

        // these methods are called at the armaments scripts
        public void StraightMissile_uiCooldown(bool isCooldown, float cooldown, float currentCooldown)
        {

            UICooldown(straightMissile_fill, isCooldown, cooldown, currentCooldown);

        }

        public void DropBomb_uiCooldown(bool isCooldown, float cooldown, float currentCooldown)
        {

            UICooldown(dropBomb_fill, isCooldown, cooldown, currentCooldown);

        }

        public void GuidedMissile_uiCooldown(bool isCooldown, float cooldown, float currentCooldown)
        {

            UICooldown(guidedMissile_fill, isCooldown, cooldown, currentCooldown);

        }

        public void AutomicBomb_uiCooldown(bool isCooldown, float cooldown, float currentCooldown)
        {
            
            UICooldown(automicBomb_fill, isCooldown, cooldown, currentCooldown);

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
