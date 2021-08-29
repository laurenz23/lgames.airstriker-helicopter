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
        [SerializeField] private Image weaponDamage_image;
        [SerializeField] private Image weaponSpeed_image;
        [SerializeField] private Image weaponFirerate_image;
        [SerializeField] private Image weaponBlastArea_image;
        [SerializeField] private Button research_button;
        [SerializeField] private Button upgrade_button;
        [SerializeField] private Image maxLvL_img;
        [SerializeField] private Text cost_text;

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
        
        private float averageDamage;
        private float averageFirerate;
        private float averageSpeed;
        private float averageBlastArea;

        private int unresearch = 0;

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

        // call this function to set weapon details to ui in armory panel
        public void SetWeaponInfo(ArmoryItem armoryItem, GameWeaponData gameWeaponData, WeaponData weaponData, int weaponLevel)
        {
            ResetData(); // reset the data first before setting the details to avoid unwanted data's

            // check weapon type, to assign a computation formula
            // each weapon type have different computation for damage, firerate and speed
            if (gameWeaponData.type == AttackType.BULLET)
            {
                averageDamage = 0.1f;
                averageFirerate = 0.01f;
                averageSpeed = 0.1f;
            }
            else if (gameWeaponData.type == AttackType.MISSILE)
            {
                averageDamage = 0.01f;
                averageFirerate = 0.01f;
                averageSpeed = 0.1f;
            }
            else
            {
                averageDamage = 0f;
                averageFirerate = 0f;
                averageSpeed = 0f;
            }

            // set selected weapon name to ui armory panel
            weaponName_text.text = gameWeaponData._name.ToUpper();
            // set selected weapon icon to ui armory panel
            weaponIcon_image.sprite = weaponData.icon;
            // set selected weapon description to ui armory panel
            weaponDescription_text.text = gameWeaponData.description.ToUpper();
            // set selected weapon damage amount to ui armory panel
            weaponDamage_image.fillAmount = (float) weaponData.damage * averageDamage;
            // set selected weapon firerate amount to ui armory panel
            weaponFirerate_image.fillAmount = 1f - ((float) weaponData.firerate * averageFirerate);
            // set selected weapon speed amount to ui armory panel
            weaponSpeed_image.fillAmount = (float) weaponData.speed * averageSpeed;
            // set selected weapon aoe amount to ui armory panel
            weaponBlastArea_image.fillAmount = (float) weaponData.aoe / 20f;

            int cost = weaponData.cost; // weapon cost might differ from weapon level

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
            selectedWeaponData = weaponData;
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
            SetWeaponInfo(selectedArmoryItem, selectedGameWeaponData, selectedGameWeaponData.weaponData[newWeaponLevel], newWeaponLevel);

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
