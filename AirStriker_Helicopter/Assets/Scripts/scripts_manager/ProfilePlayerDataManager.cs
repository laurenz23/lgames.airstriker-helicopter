using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage:      this script is attached to proflePlayerDataManager object prefab
/// function:   handles the save and load player profile data's by calling class functions
/// </summary>
namespace game_ideas
{
    public class ProfilePlayerDataManager : MonoBehaviour
    {

        private static ProfilePlayerDataManager instance;

        public static ProfilePlayerDataManager GetInstance()
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

        private DataManager dataManager;

        [HideInInspector] public ProfilePlayerData profilePlayerData = new ProfilePlayerData();
        [HideInInspector] public ProfileScoreData profileScoreData = new ProfileScoreData();
        [HideInInspector] public ProfileTokensData profileTokensData = new ProfileTokensData();
        [HideInInspector] public ProfileUnitWrapper profileUnitData = new ProfileUnitWrapper();
        [HideInInspector] public ProfileSelectedData profileSelectedData = new ProfileSelectedData();

        string playername = "AI 1.0";

        private void Start()
        {

            dataManager = DataManager.GetInstance();

            profilePlayerData = dataManager.profilePlayerData;
            profileScoreData = dataManager.profileScoreData;
            profileTokensData = dataManager.profileTokensData;
            profileUnitData = dataManager.profileUnitData;
            profileSelectedData = dataManager.profileSelectedData;

        }

        public void SetPlayerData(int playerLevel, int playerStage, int playerStageLevel)
        {
            profilePlayerData.playerName = playername;
            profilePlayerData.playerLevel = playerLevel;
            profilePlayerData.playerStage = playerStage;
            profilePlayerData.playerStageLevel = playerStageLevel;
            dataManager.SaveProfilePlayerData(profilePlayerData);
        }

        public void SetScoreData(int score)
        {
            profileScoreData.score = score;
            dataManager.SaveProfileScoreData(profileScoreData);
        }

        // change data value of player tokens
        public void SetDeploymentCapsuleData(int deploymentCapsule)
        {
            profileTokensData.playerDeploymentCapsule = deploymentCapsule;
            dataManager.SaveProfileTokensData(profileTokensData);
        }

        public void SetDiamondsData(int diamonds)
        {
            profileTokensData.playerDiamonds = diamonds;
            dataManager.SaveProfileTokensData(profileTokensData);
        }

        public void SetCoinsData(int coins)
        {
            profileTokensData.playerCoins = coins;
            dataManager.SaveProfileTokensData(profileTokensData);
        }

        // update all player unit data
        public void SetPlayerUnit(List<ProfileUnitData> playerUnitData)
        {
            profileUnitData.unitData = playerUnitData;
            dataManager.SaveProfileUnitData(profileUnitData);
        }

        // update specific player unit data
        public void UpgradeUnitWeaponData(string unitID, string weaponID, int weaponLevel)
        {
            List<ProfileUnitData> listUnitData = profileUnitData.unitData;
            foreach (ProfileUnitData pud in listUnitData)
            {
                if (pud.unitID == unitID)
                {
                    List<ProfileWeaponData> listWeaponData = pud.weaponData;
                    foreach (ProfileWeaponData pwd in listWeaponData)
                    {
                        if (pwd.weaponID == weaponID)
                        {
                            pwd.weaponLevel = weaponLevel;
                        }
                    }
                }
            }

            profileUnitData.unitData = listUnitData;
            dataManager.SaveProfileUnitData(profileUnitData);
        }

        public void UpdateSelectedUnitData(int index)
        {
            profileSelectedData.selectedUnit = index;
            dataManager.SaveProfileSelectedData(profileSelectedData);
        }

        public void UpdateSelectedStageLevelData(int stageLevel)
        {
            profileSelectedData.selectedStageLevel = stageLevel;
            dataManager.SaveProfileSelectedData(profileSelectedData);
        }

    }
}
