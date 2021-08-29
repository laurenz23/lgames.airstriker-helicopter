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

        private DataManager dataManager;

        [HideInInspector] public GameSettingsData gameSettingsData = new GameSettingsData();

        private void Start()
        {
            dataManager = DataManager.GetInstance();

            gameSettingsData = dataManager.gameSettingsData;
        }

        public void GetUIStyle()
        {
            // display the choosen ui and hide the other ui design
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                round_ui.SetActive(true);
                square_ui.SetActive(false);
                transparent_ui.SetActive(false);
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                return healthBar_fill_round;
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                return attack_btn_round;
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                return activeSkill1_btn_round;
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                return passiveSkill1_icon_round;
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                return passiveSkill2_icon_round;
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                return passiveSkill3_icon_round;
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                return movement_joystick_round;
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
            if (gameSettingsData.gameInUIStyle == GameInUIStyle.ROUND.ToString())
            {
                return movement_buttons_round;
            }
            else if (gameSettingsData.gameInUIStyle == GameInUIStyle.TRANSPARENT.ToString())
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
