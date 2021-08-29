using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage:  main menu get units and weapons data player
///         and display the current selected unit and hide the unselected unit
///         handles the selection of units and weapons
///         change material weapons: default and transparent effects
/// </summary>
namespace game_ideas
{
    public class UnitArmoryManager : MonoBehaviour
    {
        [Header("UnitData")]
        [SerializeField] private List<GameObject> gameUnits; // reference for unit gameObjects. NOTE: gameUnitObjects must equal to gameUnitData
        [SerializeField] private List<GameUnitData> gameUnitData; // reference for unit gameUnitData. NOTE: gameUnitData must equal to gameUnitObjects

        [Header("Materials")]
        [SerializeField] private Material defaultMaterial; // reference for default material after the gameObject is set to transparent material
        [SerializeField] private Material transparentMaterial; // reference for transparent material at armory panel, if the selected weapon is not researched yet.

        #region unit1
        [Header("Unit1 Objects")]
        [SerializeField] private GameObject unit1;
        [SerializeField] private GameObject unit1Armament1;
        [SerializeField] private GameObject unit1Armament2;
        [SerializeField] private GameObject unit1Armament3;
        [SerializeField] private GameObject unit1Armament4;
        [SerializeField] private GameObject unit1Armament5;
        [SerializeField] private GameObject unit1MainWing;
        #endregion

        #region unit2
        [Header("Unit2 Objects")]
        [SerializeField] private GameObject unit2;
        [SerializeField] private GameObject unit2Armament1;
        [SerializeField] private GameObject unit2Armament2;
        [SerializeField] private GameObject unit2Armament3;
        [SerializeField] private GameObject unit2Armament4;
        [SerializeField] private GameObject unit2Armament5;
        [SerializeField] private GameObject unit2MainWing;
        #endregion

        [Header("Script Reference")]
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private MainMenuUIHandler mainMenuUIHandler;
        [SerializeField] private ProfilePlayerDataManager profilePlayerDataManager;

        private List<GameObject> listSelectedUnresearchArmament = new List<GameObject>();
        private ProfileUnitWrapper profileUnitData;

        private int unitIndex;
        private int unresearch = 0; // unresearch default value. unresearch is equal to zero base on weapon data

        private static UnitArmoryManager instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public static UnitArmoryManager GetInstance()
        {
            return instance;
        }

        private void Start()
        {
            profileUnitData = profilePlayerDataManager.profileUnitData;

            unitIndex = profilePlayerDataManager.profileSelectedData.selectedUnit; // get selected unit data

            // load all the weapons from profileUnitData 
            for (int x = 0; x < gameUnits.Count; x++)
            {
                DisplayUnitWeapons(x);
            }

            DisplayUnits(unitIndex); // display the last selected unit during last opened game app
        }

        // global access to get the current unit selected by player in main menu
        public int GetUnitIndex()
        {
            return unitIndex;
        }

        // globat access of selected game unit data 
        public GameUnitData GetUnitData()
        {
            return gameUnitData[unitIndex];
        }
        
        // show researched unit and hide the unresearch
        private void WeaponUse(int weaponLevel, GameObject armament)
        {
            if (weaponLevel == unresearch) // if weaponLevel equal to zero means weapon is unresearch
            {
                armament.SetActive(false); // hide the referenced object at parameters if unresearch
            }
            else // weaponLevel greater than to zero means weapon is research
            {
                armament.SetActive(true); // show the referenced object at parameters if research
            }
        }

        // display unit weapons according to unit index
        public void DisplayUnitWeapons(int unitIndex)
        {
            switch (unitIndex)
            {
                case 0:
                    LoadUnit1Weapons(profileUnitData.unitData[unitIndex].weaponData);
                    break;
                case 1:
                    LoadUnit2Weapons(profileUnitData.unitData[unitIndex].weaponData);
                    break;
                // TEMPORARY: before adding case here, please add a method for that specific case 
            }
        }

        // display the selected unit object only and hide the other
        private void DisplayUnits(int index)
        {
            for (int x = 0; x < gameUnits.Count; x++) // get how many game unit object is referenced
            {
                if (x == index)
                {
                    gameUnits[x].SetActive(true); // show the selected unit object
                }
                else
                {
                    gameUnits[x].SetActive(false); // hide the selected unit object
                    gameUnits[x].transform.rotation = Quaternion.identity; // reset character rotation after deselected
                }
            }

            DisplayUnitWeapons(index); // display the current weapons of unit
            mainMenuUIHandler.SetUnitData(index); // update the main menu ui when selecting units
        }
        
        // display the next unit of the game
        public void NextUnit()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click1");

            if (unitIndex < (gameUnits.Count - 1)) // if unit index is less than to and not equal to gameUnitObjects referenced
            {
                unitIndex++; // increment unitIndex, if equal to gameUnits, then the current displayed unit is the newest unit
                DisplayUnits(unitIndex); // display units and weapons
                profilePlayerDataManager.UpdateSelectedUnitData(unitIndex); // update and save selected unit data json
            }
            else // player already viewing the newest unit, since the unitIndex is equal to gameUnits referenced
            {
                mainMenuUIHandler.ShowPopupMessage("These is the last character in the list");
            }
        }

        public void PreviousUnit()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click1");

            if (unitIndex > 0) // if unit index is not equal to and greater than zero, player will be able to display previous game unit objects
            {
                unitIndex--; // decrement unitIndex, if equal to zero, then the current displayed unit is the oldest unit
                DisplayUnits(unitIndex); // display units and weapons
                profilePlayerDataManager.UpdateSelectedUnitData(unitIndex); // update and save selected unit data json
            }
            else // player already viewing the oldest unit, since the unitIndex is equal to zero
            {
                mainMenuUIHandler.ShowPopupMessage("These is the first character in the list");
            }
        }
        
        // TEMPORARY ============================================================================================================
        private void LoadUnit1Weapons(List<ProfileWeaponData> weaponData) // display unit 1 weapons base on player researched data
        {
            unit1MainWing.SetActive(false);

            // weapon 1
            WeaponUse(weaponData[0].weaponLevel, unit1Armament1);
            // weapon 2
            WeaponUse(weaponData[1].weaponLevel, unit1Armament2);
            // weapon 3
            WeaponUse(weaponData[2].weaponLevel, unit1Armament3);
            // weapon 4
            WeaponUse(weaponData[3].weaponLevel, unit1Armament4);
            // weapon 5
            WeaponUse(weaponData[4].weaponLevel, unit1Armament5);

            if (weaponData[1].weaponLevel == unresearch &&
                weaponData[3].weaponLevel == unresearch)
            {
                unit1MainWing.SetActive(false);
            }
            else
            {
                unit1MainWing.SetActive(true);
            }
        }

        private void LoadUnit2Weapons(List<ProfileWeaponData> weaponData) // display unit 2 weapons base on player researched data
        {
            unit2MainWing.SetActive(false);

            // weapon 1
            WeaponUse(weaponData[0].weaponLevel, unit2Armament1);
            // weapon 2
            WeaponUse(weaponData[1].weaponLevel, unit2Armament2);
            // weapon 3
            WeaponUse(weaponData[2].weaponLevel, unit2Armament3);
            // weapon 4
            WeaponUse(weaponData[3].weaponLevel, unit2Armament4);
            // weapon 5
            WeaponUse(weaponData[4].weaponLevel, unit2Armament5);

            if (weaponData[1].weaponLevel == unresearch &&
                weaponData[3].weaponLevel == unresearch)
            {
                unit2MainWing.SetActive(false);
            }
            else
            {
                unit2MainWing.SetActive(true);
            }
        }
        // END ==================================================================================================================

        /*
         * ARMORY UI PANEL ======================================================================================================
         */
        
        // call this method when selected weapon is unresearch. display the weapon but change the material to transparent
        private void SelectedUnresearchWeapon(int weaponLevel, GameObject armament)
        {
            if (weaponLevel == unresearch)
            {
                Renderer[] renderer = armament.GetComponentsInChildren<Renderer>(); // get all material from parameter object as parent to children
                foreach (Renderer r in renderer)
                {
                    r.material = transparentMaterial; // change material to transparent, the object is not research yet.
                }
                armament.SetActive(true); // display the object 
                listSelectedUnresearchArmament.Add(armament); // add the object to list, the list is reference for unselection to hide it again and update material back to default
            }
        }
        
        // call this method if unselecting the weapon current selected weapon
        public void UnselectUnresearchWeapon()
        {
            foreach (GameObject go in listSelectedUnresearchArmament) 
            {
                go.SetActive(false); // all object added to list during the selection of unresearch weapon set the object to false, the object is not research yet
            }

            listSelectedUnresearchArmament.Clear(); // after setting the object to false. delete all the object from the list of unresearch weapons
        }

        // call this method when selected weapon is upgraded to change the material to default.
        private void SelectedUpgradeWeapon(GameObject armament)
        {
            Renderer[] renderer = armament.GetComponentsInChildren<Renderer>(); // get all material from parameter object as parent to children
            foreach (Renderer r in renderer)
            {
                r.material = defaultMaterial; // change material to default
            }
            listSelectedUnresearchArmament.Clear(); // clear the list added object, since the selected object is now researched
        }
        
        // call this method when selecting weapons, and also this method is being called at armory item script, as the action of selecting weapons
        public void SelectCharacterWeapon(string weaponID)
        {

            UnselectUnresearchWeapon(); // to reset the selected weapon and remove it from list

            // unit1=============================================================================================================
            if (GetUnitIndex() == 0) // get unit 1 data
            {
                // weapon 1
                if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[0].weaponID) // check if selected weapon is armament 1
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[0].weaponLevel, unit1Armament1);
                }
                // weapon 2
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponID) // check if selected weapon is armament 2
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponLevel, unit1Armament2);

                    // weapon 2 and weapon 4 is child of main wing, so if both are unresearch, main wing is also unresearch and will be displayed with material of unresearch effect
                    if (profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponLevel == unresearch &&
                        profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponLevel == unresearch)
                    {
                        unit1MainWing.SetActive(true); // display main wing
                        unit1MainWing.GetComponent<Renderer>().material = transparentMaterial; // and change the material to unresearch effect
                        listSelectedUnresearchArmament.Add(unit1MainWing); // add the main wing to list selected unresearch, the main wing is also unresearch
                    }
                }
                // weapon 3
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[2].weaponID) // check if selected weapon is armament 3
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[2].weaponLevel, unit1Armament3);
                }
                // weapon 4
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponID) // check if selected weapon is armament 4
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponLevel, unit1Armament4);

                    // weapon 2 and weapon 4 is child of main wing, so if both are unresearch, main wing is also unresearch and will be displayed with material of unresearch effect
                    if (profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponLevel == unresearch &&
                        profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponLevel == unresearch)
                    {
                        unit1MainWing.SetActive(true); // display main wing
                        unit1MainWing.GetComponent<Renderer>().material = transparentMaterial; // and change the material to unresearch effect
                        listSelectedUnresearchArmament.Add(unit1MainWing); // add the main wing to list selected unresearch, the main wing is also unresearch
                    }
                }
                // weapon 5
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[4].weaponID) // check if selected weapon is armament 5
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[4].weaponLevel, unit1Armament5);
                }

            }
            // unit2=============================================================================================================
            else if (GetUnitIndex() == 1) // get unit 2 data
            {
                // weapon 1
                if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[0].weaponID) // check if selected weapon is armament 1
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[0].weaponLevel, unit2Armament1);
                }
                // weapon 2
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponID) // check if selected weapon is armament 2
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponLevel, unit2Armament2);

                    // weapon 2 and weapon 4 is child of main wing, so if both are unresearch, main wing is also unresearch and will be displayed with material of unresearch effect
                    if (profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponLevel == unresearch &&
                        profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponLevel == unresearch)
                    {
                        unit2MainWing.SetActive(true); // display main wing
                        unit2MainWing.GetComponent<Renderer>().material = transparentMaterial; // and change the material to unresearch effect
                        listSelectedUnresearchArmament.Add(unit2MainWing); // add the main wing to list selected unresearch, the main wing is also unresearch
                    }
                }
                // weapon 3
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[2].weaponID) // check if selected weapon is armament 3
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[2].weaponLevel, unit2Armament3);
                }
                // weapon 4
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponID) // check if selected weapon is armament 4
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponLevel, unit2Armament4);

                    // weapon 2 and weapon 4 is child of main wing, so if both are unresearch, main wing is also unresearch and will be displayed with material of unresearch effect
                    if (profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponLevel == unresearch &&
                        profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponLevel == unresearch)
                    {
                        unit2MainWing.SetActive(true); // display main wing
                        unit2MainWing.GetComponent<Renderer>().material = transparentMaterial; // and change the material to unresearch effect
                        listSelectedUnresearchArmament.Add(unit2MainWing); // add the main wing to list selected unresearch, the main wing is also unresearch
                    }
                }
                // weapon 5
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[4].weaponID) // check if selected weapon is armament 5
                {
                    //check player weapon data if unresearch and pass the armament object to change the material to transparent, if true
                    SelectedUnresearchWeapon(profileUnitData.unitData[GetUnitIndex()].weaponData[4].weaponLevel, unit2Armament5);
                }
            }

            // add statement when adding units

        }

        // call this method when the selected weapon will be research, and also this method is being called at armoryUIManager, as part action of upgrade weapon
        public void UpgradeWeapon(string weaponID)
        {

            // unit1=============================================================================================================
            if (GetUnitIndex() == 0) // selected unit is unit1
            {
                // weapon 1
                if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[0].weaponID) // check if selected weapon is armament 1
                {
                    SelectedUpgradeWeapon(unit1Armament1); // update parameter object material to default or researched material
                }
                // weapon 2
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponID) // check if selected weapon is armament 2
                {
                    SelectedUpgradeWeapon(unit1Armament2); // update parameter object material to default or researched material
                    // weapon 2 and weapon 3 is child of main wing, if ether one of those weapons are searched the main wing will also be search
                    unit1MainWing.GetComponent<Renderer>().material = defaultMaterial; 
                }
                // weapon 3
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[2].weaponID) // check if selected weapon is armament 3
                {
                    SelectedUpgradeWeapon(unit1Armament3); // update parameter object material to default or researched material
                }
                // weapon 4
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponID) // check if selected weapon is armament 4
                {
                    SelectedUpgradeWeapon(unit1Armament4); // update parameter object material to default or researched material
                    // weapon 2 and weapon 3 is child of main wing, if ether one of those weapons are searched the main wing will also be search
                    unit1MainWing.GetComponent<Renderer>().material = defaultMaterial;
                }
                // weapon 5
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[4].weaponID) // check if selected weapon is armament 5
                {
                    SelectedUpgradeWeapon(unit1Armament5); // update parameter object material to default or researched material
                }
            }
            // unit2=============================================================================================================
            else if (GetUnitIndex() == 1) // selected unit is unit2
            {
                // weapon 1
                if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[0].weaponID) // check if selected weapon is armament 1
                {
                    SelectedUpgradeWeapon(unit2Armament1); // update parameter object material to default or researched material
                }
                // weapon 2
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[1].weaponID) // check if selected weapon is armament 2
                {
                    SelectedUpgradeWeapon(unit2Armament2); // update parameter object material to default or researched material
                    // weapon 2 and weapon 3 is child of main wing, if ether one of those weapons are searched the main wing will also be search
                    unit2MainWing.GetComponent<Renderer>().material = defaultMaterial;
                }
                // weapon 3
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[2].weaponID) // check if selected weapon is armament 3
                {
                    SelectedUpgradeWeapon(unit2Armament3); // update parameter object material to default or researched material
                }
                // weapon 4
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[3].weaponID) // check if selected weapon is armament 4
                {
                    SelectedUpgradeWeapon(unit2Armament4); // update parameter object material to default or researched material
                    // weapon 2 and weapon 3 is child of main wing, if ether one of those weapons are searched the main wing will also be search
                    unit2MainWing.GetComponent<Renderer>().material = defaultMaterial;
                }
                // weapon 5
                else if (weaponID == profileUnitData.unitData[GetUnitIndex()].weaponData[4].weaponID) // check if selected weapon is armament 5
                {
                    SelectedUpgradeWeapon(unit2Armament5); // update parameter object material to default or researched material
                }
            }

        }

        // END ==================================================================================================================
        
    }
}
