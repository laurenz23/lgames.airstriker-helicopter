using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// attached this script to game ui manager object
/// manages the design in ui for player presentation
/// </summary>

namespace game_ideas
{
    public class InGameUIDesign : MonoBehaviour
    {

        [Header("Designs")]
        [SerializeField] private GameObject square_ui = null; // default ui
        [SerializeField] private GameObject round_ui = null;
        [SerializeField] private GameObject transparent_ui = null;

        [Header("Health Design")]
        [SerializeField] private Image healthBar_fill_square = null;
        [SerializeField] private Image healthBar_fill_round = null;
        [SerializeField] private Image healthBar_fill_transparent = null;

        [Header("Attack Design")]
        [SerializeField] private Button attack_btn_square = null;
        [SerializeField] private Button attack_btn_round = null;
        [SerializeField] private Button attack_btn_transparent = null;

        [Header("Active Skill 1")]
        [SerializeField] private Button activeSkill1_btn_square = null;
        [SerializeField] private Button activeSkill1_btn_round = null;
        [SerializeField] private Button activeSkill1_btn_transparent = null;

        [Header("Passive Skill 1")]
        [SerializeField] private Transform passiveSkill1_icon_square = null;
        [SerializeField] private Transform passiveSkill1_icon_round = null;
        [SerializeField] private Transform passiveSkill1_icon_transparent = null;

        [Header("Passive Skill 2")]
        [SerializeField] private Transform passiveSkill2_icon_square = null;
        [SerializeField] private Transform passiveSkill2_icon_round = null;
        [SerializeField] private Transform passiveSkill2_icon_transparent = null;

        [Header("Passive Skill 3")]
        [SerializeField] private Transform passiveSkill3_icon_square = null;
        [SerializeField] private Transform passiveSkill3_icon_round = null;
        [SerializeField] private Transform passiveSkill3_icon_transparent = null;

        [Header("Square Movement")]
        [SerializeField] private Transform movement_joystick_square = null;
        [SerializeField] private Transform movement_buttons_square = null;

        [Header("Round Movement")]
        [SerializeField] private Transform movement_joystick_round = null;
        [SerializeField] private Transform movement_buttons_round = null;

        [Header("Transparent Movement")]
        [SerializeField] private Transform movement_joystick_transparent = null;
        [SerializeField] private Transform movement_buttons_transparent = null;

        private GameSettingsManager gameSettingsManager = null;

        private void Awake()
        {
            gameSettingsManager = GameSettingsManager.GetInstance();
        }

        public void GetUIStyle()
        {
            // display the choosen ui and hide the other ui design
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                round_ui.SetActive(true);
                square_ui.SetActive(false);
                transparent_ui.SetActive(false);
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                round_ui.SetActive(false);
                square_ui.SetActive(false);
                transparent_ui.SetActive(true);
            }
            else
            {
                round_ui.SetActive(false);
                square_ui.SetActive(true);
                transparent_ui.SetActive(false);
            }
        }

        // get health fill design
        public Image GetHealthBar_fill()
        {
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                return healthBar_fill_round;
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                return healthBar_fill_transparent;
            }
            else
            {
                return healthBar_fill_square;
            }
        }

        // get attack button design
        public Button GetAttack_button()
        {
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                return attack_btn_round;
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                return attack_btn_transparent;
            }
            else
            {
                return attack_btn_square;
            }
        }

        // get active skill 1 design
        public Button GetActiveSkill1_button()
        {
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                return activeSkill1_btn_round;
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                return activeSkill1_btn_transparent;
            }
            else
            {
                return activeSkill1_btn_square;
            }
        }

        // get passive skill 1 design
        public Transform GetPassiveSkill1_icon()
        {
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                return passiveSkill1_icon_round;
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                return passiveSkill1_icon_transparent;
            }
            else
            {
                return passiveSkill1_icon_square;
            }
        }

        // get passive skill 2 design
        public Transform GetPassiveSkill2_icon()
        {
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                return passiveSkill2_icon_round;
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                return passiveSkill2_icon_transparent;
            }
            else
            {
                return passiveSkill2_icon_square;
            }
        }

        // get passive skill 3 design
        public Transform GetPassiveSkill3_icon()
        {
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                return passiveSkill3_icon_round;
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                return passiveSkill3_icon_transparent;
            }
            else
            {
                return passiveSkill3_icon_square;
            }
        }

        // get joystick design
        public Transform GetMovementJoystickUI()
        {
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                return movement_joystick_round;
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                return movement_joystick_transparent;
            }
            else
            {
                return movement_joystick_square;
            }
        }

        // get buttons design
        public Transform GetMovementButtonsUI()
        {
            if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.ROUND)
            {
                return movement_buttons_round;
            }
            else if (gameSettingsManager.GetData().gameInUIStyle == GameInUIStyle.TRANSPARENT)
            {
                return movement_buttons_transparent;
            }
            else
            {
                return movement_buttons_square;
            }
        }

    }
}
