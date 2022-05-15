using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// usage:      attached this script to armoryUIManager object or create one if doesn't exist
/// function:   manages the ui of armory and events of upgrade weapons 
///             update ui armory panel values
/// </summary>

namespace game_ideas
{
    public class ArmoryUIManager : MonoBehaviour
    {

        private static ArmoryUIManager instance;

        public static ArmoryUIManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        [Header("Armory UI Reference")]
        [SerializeField] private Text weaponName_text;
        [SerializeField] private Image weaponIcon_image;
        [SerializeField] private Text weaponDescription_text;
        [SerializeField] private Button research_button;
        [SerializeField] private Button upgrade_button;
        [SerializeField] private Image maxLvL_img;
        [SerializeField] private Text cost_text;

        [Header("Damage Sliders")]
        [SerializeField] private Slider damage_slider;
        [SerializeField] private Slider nextDamage_slider;
        [SerializeField] private Slider subDamage_slider;
        [SerializeField] private Slider nextSubDamage_slider;

        [Header("Speed Sliders")]
        [SerializeField] private Slider speed_slider;
        [SerializeField] private Slider nextSpeed_slider;
        [SerializeField] private Slider subSpeed_slider;
        [SerializeField] private Slider nextSubSpeed_slider;

        [Header("Firerate Sliders")]
        [SerializeField] private Slider firerate_slider;
        [SerializeField] private Slider nextFirerate_slider;
        [SerializeField] private Slider subFirerate_slider;
        [SerializeField] private Slider nextSubFirerate_slider;

        [Header("AoE Sliders")]
        [SerializeField] private Slider aoe_slider;
        [SerializeField] private Slider nextAoe_slider;
        [SerializeField] private Slider subAoe_slider;
        [SerializeField] private Slider nextSubAoe_slider;

        [Header("Item Reference")]
        [SerializeField] private Transform itemParent;
        [SerializeField] private GameObject itemWeapon;

        [Header("Script Reference")]
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private MainMenuUIHandler mainMenuUIHandler;
        [SerializeField] private UnitArmoryManager unitArmoryManager;

        private List<ProfileWeaponData> profileWeaponData;
        private ProfilePlayerDataManager profilePlayerDataManager;

        // reference for upgrade action
        [HideInInspector] public ArmoryItem selectedArmoryItem;
        [HideInInspector] public GameWeaponData selectedGameWeaponData;
        [HideInInspector] public WeaponData selectedWeaponData;
        [HideInInspector] public int selectedCurrentWeaponLevel;

        private int unresearch = 0;

        private AttackType attackType;
        private int damage;
        private int firerate;
        private int speed;
        private int aoe;

        private void Start()
        {
            profilePlayerDataManager = ProfilePlayerDataManager.GetInstance();
        }

        private void OnDisable()
        {
            ResetData(); // reset when object is disabled or back to main menu
        }

        public void DisplayWeaponsUIList(int index)
        {
            ProfileUnitWrapper profileUnitWrapper = profilePlayerDataManager.profileUnitData;

            profileWeaponData = profileUnitWrapper.unitData[unitArmoryManager.GetUnitIndex()].weaponData;

            PopulateUnitWeaponListUI(unitArmoryManager.GetUnitData().id, profileUnitWrapper.unitData[unitArmoryManager.GetUnitIndex()].weaponData);
        }

        // call this function to destroy the populated weapon ui list, we use this when switching between from armory to another panel like back to main menu
        public void DestroyWeaponUIList()
        {
            for (int x = 0; x < itemParent.childCount; x++)
            {
                Destroy(itemParent.GetChild(x).gameObject);
            }
        }

        // call this function to instantiate the armory item ui object, if we enter armory panel 
        private void CreateItem(GameWeaponData gameWeaponData, string weaponID, int weaponLevel)
        {
            GameObject newItem = Instantiate(itemWeapon, itemParent) as GameObject;
            ArmoryItem ai = newItem.GetComponent<ArmoryItem>();
            ai.weaponID = weaponID; 
            ai.SetItem(gameWeaponData, weaponLevel); // set armory item ui values
        }

        // call this function to populate ui list weapon, pass unit id and profile weapon data
        public void PopulateUnitWeaponListUI(string unitID, List<ProfileWeaponData> profileWeaponData)
        {
            if (unitArmoryManager.GetUnitData().id == unitID) // unit weapons
            {
                // weapon1
                CreateItem(unitArmoryManager.GetUnitData().listGameWeaponData[0], profileWeaponData[0].weaponID, profileWeaponData[0].weaponLevel);

                // weapon2
                CreateItem(unitArmoryManager.GetUnitData().listGameWeaponData[1], profileWeaponData[1].weaponID, profileWeaponData[1].weaponLevel);

                // weapon3
                CreateItem(unitArmoryManager.GetUnitData().listGameWeaponData[2], profileWeaponData[2].weaponID, profileWeaponData[2].weaponLevel);

                // weapon4
                CreateItem(unitArmoryManager.GetUnitData().listGameWeaponData[3], profileWeaponData[3].weaponID, profileWeaponData[3].weaponLevel);

                // weapon5
                CreateItem(unitArmoryManager.GetUnitData().listGameWeaponData[4], profileWeaponData[4].weaponID, profileWeaponData[4].weaponLevel);

            }
        }

        private void SetSlider(Slider slider, Slider subSlider, int value)
        {
            int sliderLevel = 0;

            if (value > 0 && value <= 20)
            {
                slider.minValue = 0;
                slider.maxValue = 20;
                sliderLevel = 1;
            }
            else if (value > 20 && value <= 100)
            {
                slider.minValue = 0;
                slider.maxValue = 100;
                sliderLevel = 2;
            }
            else if (value > 100 && value <= 300)
            {
                slider.minValue = 100;
                slider.maxValue = 300;
                sliderLevel = 3;
            }
            else if (value > 300 && value <= 1300)
            {
                slider.minValue = 300;
                slider.maxValue = 1300;
                sliderLevel = 4;
            }
            else
            {
                slider.minValue = 0;
                slider.maxValue = 0;
            }

            slider.value = value;
            subSlider.value = sliderLevel;
        }

        private void SetFirerateSlider(Slider slider, Slider subSlider, int value, AttackType type)
        {
            switch (type)
            {
                case AttackType.BULLET:

                    if (value > 0 && value <= 30)
                    {
                        value = 30 - value;
                        slider.minValue = 10;
                        slider.maxValue = 30;
                        subSlider.value = 4;
                    }
                    else
                    {
                        slider.minValue = 0;
                        slider.maxValue = 0;
                    }

                    break;
                case AttackType.MISSILE:
                case AttackType.BOMB:
                case AttackType.ATOMIC:

                    if (value > 0 && value <= 100)
                    {
                        value = 100 - value;
                        slider.minValue = 0;
                        slider.maxValue = 100;
                        subSlider.value = 3;
                    }
                    else if (value > 100 && value <= 600)
                    {
                        value = 600 - value;
                        slider.minValue = 100;
                        slider.maxValue = 600;
                        subSlider.value = 2;
                    }
                    else
                    {
                        slider.minValue = 0;
                        slider.maxValue = 0;
                    }

                    break;
            }

            if (value <= slider.minValue && subSlider.value > 1)
            {
                slider.value = slider.maxValue;
                subSlider.value -= 1;
            }
            else
            {
                slider.value = value;
            }
        }

        // call this function to set weapon details to ui in armory panel
        public void SetWeaponInfo(ArmoryItem armoryItem, GameWeaponData gameWeaponData, int weaponLevel)
        {
            ResetData(); // reset the data first before setting the details to avoid unwanted data's

            attackType = gameWeaponData.type;
            damage = gameWeaponData.weaponData[weaponLevel].damage;
            speed = gameWeaponData.weaponData[weaponLevel].speed;
            firerate = gameWeaponData.weaponData[weaponLevel].firerate;
            aoe = gameWeaponData.weaponData[weaponLevel].aoe;

            int nextDamage = 0;
            int nextSpeed = 0;
            int nextFirerate = 0;
            int nextAoE = 0;

            if (weaponLevel < 10)
            {
                nextDamage = gameWeaponData.weaponData[weaponLevel + 1].damage;
                nextSpeed = gameWeaponData.weaponData[weaponLevel + 1].speed;
                nextFirerate = gameWeaponData.weaponData[weaponLevel + 1].firerate;
                nextAoE = gameWeaponData.weaponData[weaponLevel + 1].aoe;
            }

            // check weapon type, to assign a computation formula 
            if (weaponLevel != unresearch)
            {
                // damage
                SetSlider(damage_slider, subDamage_slider, damage);
                // speed
                SetSlider(speed_slider, subSpeed_slider, speed);
                // firerate
                SetFirerateSlider(firerate_slider, subFirerate_slider, firerate, attackType);
                // aoe
                SetSlider(aoe_slider, subAoe_slider, aoe);
            }
            else
            {
                // damage
                SetSlider(damage_slider, subDamage_slider, 0);
                // speed
                SetSlider(speed_slider, subSpeed_slider, 0);
                // firerate
                SetSlider(firerate_slider, subFirerate_slider, 0);
                // aoe
                SetSlider(aoe_slider, subAoe_slider, 0);
            }

            // damage
            SetSlider(nextDamage_slider, nextSubDamage_slider, nextDamage);
            // speed
            SetSlider(nextSpeed_slider, nextSubSpeed_slider, nextSpeed);
            // firerate
            SetFirerateSlider(nextFirerate_slider, nextSubFirerate_slider, nextFirerate, attackType);
            // aoe
            SetSlider(nextAoe_slider, nextSubAoe_slider, nextAoE);

            // set selected weapon name to ui armory panel
            weaponName_text.text = gameWeaponData._name.ToUpper();
            // set selected weapon icon to ui armory panel
            weaponIcon_image.sprite = gameWeaponData.icon;
            // set selected weapon description to ui armory panel
            weaponDescription_text.text = gameWeaponData.description.ToUpper();
            // set selected weapon damage amount to ui armory panel

            int cost = gameWeaponData.weaponData[weaponLevel].cost; // weapon cost might differ from weapon level

            if (weaponLevel == unresearch) // weapon is unresearch then use diamonds as cost
            {
                research_button.gameObject.SetActive(true);
                upgrade_button.gameObject.SetActive(false);
                maxLvL_img.gameObject.SetActive(false);
                cost_text.gameObject.SetActive(true);
                cost_text.text = cost.ToString() + " RESEARCH COST"; // set the text as (COST VALUE) research
            }
            else if (weaponLevel >= 10) // weapon is maximum level then,
            {
                research_button.gameObject.SetActive(false); // hide research button
                upgrade_button.gameObject.SetActive(false); // hide upgrade button
                maxLvL_img.gameObject.SetActive(true); // display max level image
                cost_text.gameObject.SetActive(false); // hide cost text
            }
            else // weapon is between to 1 and 9 then,
            {
                research_button.gameObject.SetActive(false); // hide research button
                upgrade_button.gameObject.SetActive(true); // show upgrade button
                maxLvL_img.gameObject.SetActive(false); // hide max level image
                cost_text.gameObject.SetActive(true); // display cost text
                cost_text.text = cost.ToString() + " UPGRADE COST"; // set the text as (COST VALUE) upgrade
            }

            // assign selected weapons data and script for upgrade weapon action
            selectedArmoryItem = armoryItem;
            selectedGameWeaponData = gameWeaponData;
            selectedWeaponData = gameWeaponData.weaponData[weaponLevel];
            selectedCurrentWeaponLevel = weaponLevel;
        }

        // call this function to update weapon in armory panel
        public void Upgrade()
        {
            if (selectedCurrentWeaponLevel >= 10)
            {
                // the current selected weapon level is already in maximum level
                return;
            }
            
            if (selectedCurrentWeaponLevel == unresearch) // research weapon using diamond tokens
            {
                if (profilePlayerDataManager.profileTokensData.playerDiamonds >= selectedWeaponData.cost)
                {
                    soundManager.soundFXHandler.SFX_ARMORY("armory2");
                    // update player token data
                    profilePlayerDataManager.SetDiamondsData(profilePlayerDataManager.profileTokensData.playerDiamonds - selectedWeaponData.cost);
                }
                else
                {
                    soundManager.soundFXHandler.SFX_ALERT_WARNING("warning2");
                    // display error message for player
                    mainMenuUIHandler.ShowPopupMessage("you don't have enough diamonds to research");

                    return; // not enough diamonds, don't proceed to level up weapon
                }
            }
            else // upgrade weapon using coin tokens to upgrade
            {
                if (profilePlayerDataManager.profileTokensData.playerCoins >= selectedWeaponData.cost)
                {
                    soundManager.soundFXHandler.SFX_ARMORY("armory1");
                    // update player token data
                    profilePlayerDataManager.SetCoinsData(profilePlayerDataManager.profileTokensData.playerCoins - selectedWeaponData.cost);
                }
                else
                {
                    soundManager.soundFXHandler.SFX_ALERT_WARNING("warning2");
                    // display error message for player
                    mainMenuUIHandler.ShowPopupMessage("you don't have enough gold to upgrade");

                    return; // not enough coins, don't proceed to level up weapon
                }
            }

            // level up weapon
            int newWeaponLevel = selectedCurrentWeaponLevel + 1;
            
            // update ui coin text
            mainMenuUIHandler.SetPlayerTokens();

            // update profile player unit data
            profilePlayerDataManager.UpgradeUnitWeaponData(unitArmoryManager.GetUnitData().id, selectedGameWeaponData.id, newWeaponLevel);

            // update selected armory ui weapon information panel
            SetWeaponInfo(selectedArmoryItem, selectedGameWeaponData, newWeaponLevel);

            // update armory weapon list information
            selectedArmoryItem.SetItem(selectedGameWeaponData, newWeaponLevel);

            // display the weapon to character
            unitArmoryManager.UpgradeWeapon(selectedGameWeaponData.id);

            // update selected current weapon level reference
            selectedCurrentWeaponLevel = newWeaponLevel;
        }

        public void ResetData()
        {
            selectedArmoryItem = null;
            selectedGameWeaponData = null;
            selectedWeaponData = null;
            selectedCurrentWeaponLevel = 0;
        }

    }
}
