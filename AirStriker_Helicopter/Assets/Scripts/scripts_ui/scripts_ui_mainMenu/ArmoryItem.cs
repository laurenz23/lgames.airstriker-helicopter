using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// usage:  handles the weapons items, actions and item ui values
///         this function is intantiated when switching from main menu to armory panel
/// </summary>
namespace game_ideas
{
    public class ArmoryItem : MonoBehaviour
    {
        [SerializeField] private Button itemResearch_button;
        [SerializeField] private Button itemUpgrade_button;
        [SerializeField] private Button itemMaxLevel_button;
        [SerializeField] private Text itemName_text;
        [SerializeField] private Image itemIcon_image; 
        [SerializeField] private Image itemToken_image;
        [SerializeField] private Text itemCost_text; 

        [Header("Sprites (Research and Upgrade)")]
        [SerializeField] private Sprite tokenDiamond_sprite;
        [SerializeField] private Sprite tokenCoin_sprite;

        // armory item references
        public GameWeaponData gameWeaponData; // variable to get weapon details
        public string weaponID; // variable to identify what armory item is currently selected
        public int weaponLevel; // variable get weapon level

        private SoundManager soundManager;
        private ArmoryUIManager armoryUIManager;
        private UnitArmoryManager unitArmoryManager;

        private int unresearch = 0; // unresearch value

        private void Start()
        {
            unitArmoryManager = UnitArmoryManager.GetInstance();
            soundManager = SoundManager.GetInstance();
            armoryUIManager = ArmoryUIManager.GetInstance();
        }

        // call this function to update armory item ui and references
        public void SetItem(GameWeaponData gameWeaponData, int weaponLevel)
        {
            // update armory item references, we update the gameWeaponData and weaponLevel
            // if the player upgraded the selected armory item it will update gameWeaponData and weaponLevel
            this.gameWeaponData = gameWeaponData;
            this.weaponLevel = weaponLevel;

            itemName_text.text = gameWeaponData._name.ToUpper(); // set weapon name
            itemIcon_image.sprite = gameWeaponData.weaponData[weaponLevel].icon; // set weapon icon base on weapon level

            if (weaponLevel == unresearch) // weapon level is unresearch yet then,
            {
                itemToken_image.gameObject.SetActive(true); // diplay token image
                itemToken_image.sprite = tokenDiamond_sprite; // and change the token sprite to diamonds, diamonds is use to research weapons
                itemResearch_button.gameObject.SetActive(true); // show research button
                itemUpgrade_button.gameObject.SetActive(false); // hide upgrade button
                itemMaxLevel_button.gameObject.SetActive(false); // hide max level button
                itemCost_text.text = gameWeaponData.weaponData[weaponLevel].cost.ToString(); // set diamond cost value, base on weapon level
            }
            else if (weaponLevel >= 10) // weapon is already set to maximum level then,
            {
                itemToken_image.gameObject.SetActive(false); // hide the token image, we don't need anymore the tokens for selected weapon since it achieved the maximum level
                itemResearch_button.gameObject.SetActive(false);
                itemUpgrade_button.gameObject.SetActive(false);
                itemMaxLevel_button.gameObject.SetActive(true);
                itemCost_text.text = "MAX LEVEL"; // set the weapon cost to max level, token is already invisible
            }
            else
            {
                itemToken_image.gameObject.SetActive(true); // display token image
                itemToken_image.sprite = tokenCoin_sprite; // and change the token sprite to coins, coins is use to upgrade weapons
                itemResearch_button.gameObject.SetActive(false); 
                itemUpgrade_button.gameObject.SetActive(true); // display the upgrade button and hide the other two buttons
                itemMaxLevel_button.gameObject.SetActive(false);
                itemCost_text.text = gameWeaponData.weaponData[weaponLevel].cost.ToString(); // set the coin cost to upgrade the weapon
            }
            
        }

        // call this function to update the weapon item tokens
        public void WeaponItem()
        {
            if (weaponLevel == unresearch) // weapon is unresearched, then token use is diamond
            {
                soundManager.soundFXHandler.SFX_ARMORY("armory2");
                itemToken_image.sprite = tokenDiamond_sprite;
            }
            else // weapon is already research and will be upgrade, then token use is coins
            {
                soundManager.soundFXHandler.SFX_ARMORY("armory1");
                itemToken_image.sprite = tokenCoin_sprite;
            }

            armoryUIManager.SetWeaponInfo(this, gameWeaponData, gameWeaponData.weaponData[weaponLevel], weaponLevel); // update armory weapon ui

            unitArmoryManager.SelectCharacterWeapon(gameWeaponData.id); // update selected unit weapons object
        }

    }
}
