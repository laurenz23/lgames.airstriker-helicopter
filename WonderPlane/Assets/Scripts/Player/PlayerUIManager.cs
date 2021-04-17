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
        [SerializeField] private PlayerAttackHandler playerAttackHandler = null;

        private Image straightMissile_fill;
        private Image dropBomb_fill;
        private Image guidedMissile_fill;
        private Image automicBomb_fill;

        private void Start()
        {

            straightMissile_fill = straightMissile_icon.GetChild(0).GetComponent<Image>();
            dropBomb_fill = dropBomb_icon.GetChild(0).GetComponent<Image>();
            guidedMissile_fill = guidedMissile_icon.GetChild(0).GetComponent<Image>();
            automicBomb_fill = automicAttack_btn.transform.GetChild(0).GetComponent<Image>();

            if (
                !playerAttackHandler.gatling_armament &&
                !playerAttackHandler.missile_armament &&
                !playerAttackHandler.dropMissile_armament &&
                !playerAttackHandler.guidedMissile_armament
                )
            {
                basicAttack_btn.gameObject.SetActive(false);
            }

            if (!playerAttackHandler.missile_armament)
            {
                straightMissile_icon.gameObject.SetActive(false);
            }

            if (!playerAttackHandler.dropMissile_armament)
            {
                dropBomb_icon.gameObject.SetActive(false);
            }

            if (!playerAttackHandler.guidedMissile_armament)
            {
                guidedMissile_icon.gameObject.SetActive(false);
            }
            
            if (!playerAttackHandler.automic_armament)
            {
                automicAttack_btn.gameObject.SetActive(false);
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
        public void StraightMissile_uiCooldown(float cooldown, float remainingCooldown)
        {

            straightMissile_fill.fillAmount = Mathf.InverseLerp(0, cooldown, remainingCooldown);

        }

        public void DropBomb_uiCooldown(float cooldown, float remainingCooldown)
        {

            dropBomb_fill.fillAmount = Mathf.InverseLerp(0, cooldown, remainingCooldown);

        }

        public void GuidedMissile_uiCooldown(float cooldown, float remainingCooldown)
        {

            guidedMissile_fill.fillAmount = Mathf.InverseLerp(0, cooldown, remainingCooldown);

        }

        public void AutomicBomb_uiCooldown(float cooldown, float remainingCooldown)
        {

            automicBomb_fill.fillAmount = Mathf.InverseLerp(0, cooldown, remainingCooldown);

        }

    }
}
