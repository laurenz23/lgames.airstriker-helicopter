using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// usage:      attached to data manager object prefab
/// functions:  handles the json of game data's: game settings and player profiles
///             creates a json data if given data is not yet created
///             save and load json's
/// </summary>
namespace game_ideas
{
    public class DataManager : MonoBehaviour
    {

        private static DataManager instance;

        public static DataManager GetInstance()
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

        public List<GameUnitData> listGameUnitData;

        [HideInInspector] public GameSettingsData gameSettingsData;
        [HideInInspector] public ProfilePlayerData profilePlayerData;
        [HideInInspector] public ProfileScoreData profileScoreData;
        [HideInInspector] public ProfileTokensData profileTokensData;
        [HideInInspector] public ProfileUnitWrapper profileUnitData;
        [HideInInspector] public ProfileSelectedData profileSelectedData;

        private string fileExtension = ".json";
        private string gameSettingsFilename = "GameSettingsData";
        private string profilePlayerFilename = "ProfilePlayerData";
        private string profileScoreFilename = "ProfileScoreData";
        private string profileTokensFilename = "ProfileTokensData";
        private string profileUnitFilename = "ProfileUnitData";
        private string profileSelectedFilename = "ProfileSelectedData";

        private void Start()
        {
            // LOAD DATA ----------------------------------------------------
            gameSettingsData = LoadGameSettingsData();
            profilePlayerData = LoadProfilePlayerData();
            profileScoreData = LoadProfileScoreData();
            profileTokensData = LoadProfileTokensData();
            profileUnitData = LoadProfileUnitData();
            profileSelectedData = LoadProfileSelectedData();
            // END LOAD DATA ------------------------------------------------
        }

        // START GAME SETTINGS DATA --------------------------------------------------------------------------
        public void SaveGameSettingsData(GameSettingsData gsd)
        {
            SaveData(gameSettingsFilename, gsd);
        }
        
        private GameSettingsData LoadGameSettingsData()
        {
            GameSettingsData gsd = new GameSettingsData();
            if (!LoadData(gameSettingsFilename, gsd))
            {
                gsd.gameGraphics = GameGraphics.HIGH_GRAPHICS.ToString();
                gsd.gameInUIStyle = GameInUIStyle.TRANSPARENT.ToString();
                gsd.gameControls = GameControls.JOYSTICK_CONTROLS.ToString();
                gsd.music = true;
                gsd.soundFX = true;
                SaveGameSettingsData(gsd);
            }
            return gsd;
        }
        // END GAME SETTINGS DATA ------------------------------------------------------------------------

        // CHECK PROFILE ----------------------------------------------------------------------------------
        public bool HavePlayerProfile()
        {
            if (profilePlayerData.playerName == "")
            {
                return false;
            }

            return true;
        }
        // END CHECK PROFILE ------------------------------------------------------------------------------

        // START PLAYER PROFILE --------------------------------------------------------------------------
        public void SaveProfilePlayerData(ProfilePlayerData ppd)
        {
            SaveData(profilePlayerFilename, ppd);
        }

        private ProfilePlayerData LoadProfilePlayerData()
        {
            ProfilePlayerData ppd = new ProfilePlayerData();
            if (!LoadData(profilePlayerFilename, ppd))
            {
                ppd.playerName = "";
                ppd.playerLevel = 1;
                ppd.playerStage = 1;
                ppd.playerStageLevel = 1;
                SaveProfilePlayerData(ppd);
            }
            return ppd;

        }
        // END PLAYER PROFILE ----------------------------------------------------------------------------

        // START TOKENS PROFILE --------------------------------------------------------------------------
        public void SaveProfileTokensData(ProfileTokensData ptd)
        {
            SaveData(profileTokensFilename, ptd);
        }

        private ProfileTokensData LoadProfileTokensData()
        {
            ProfileTokensData ptd = new ProfileTokensData();
            if (!LoadData(profileTokensFilename, ptd))
            {
                ptd.playerDeploymentCapsule = 5;
                ptd.playerDiamonds = 100;
                ptd.playerCoins = 1000;
                SaveProfileTokensData(ptd);
            }
            return ptd;

        }
        // END TOKENS PROFILE ----------------------------------------------------------------------------

        // START SCORE PROFILE --------------------------------------------------------------------------
        public void SaveProfileScoreData(ProfileScoreData psd)
        {
            SaveData(profileScoreFilename, psd);
        }

        private ProfileScoreData LoadProfileScoreData()
        {
            ProfileScoreData psd = new ProfileScoreData();
            if (!LoadData(profileScoreFilename, psd))
            {
                psd.score = 0;
                SaveProfileScoreData(psd);
            }
            return psd;

        }
        // END SCORE PROFILE ----------------------------------------------------------------------------

        // START UNIT PROFILE --------------------------------------------------------------------------
        public void SaveProfileUnitData(ProfileUnitWrapper pud)
        {
            SaveData(profileUnitFilename, pud);
        }

        private ProfileUnitWrapper LoadProfileUnitData()
        {
            ProfileUnitWrapper pud = new ProfileUnitWrapper();
            if (!LoadData(profileUnitFilename, pud))
            {
                List<ProfileUnitData> playerUnitData = new List<ProfileUnitData>();
                for (int x = 1; x <= 2; x++)
                {
                    ProfileUnitData unitData = new ProfileUnitData();
                    unitData.unitID = "unit" + x.ToString();
                    for (int y = 1; y <= 5; y++)
                    {
                        ProfileWeaponData weaponData = new ProfileWeaponData();
                        weaponData.weaponID = "unit" + x.ToString() + "weapon" + y.ToString();
                        weaponData.weaponLevel = 0;
                        unitData.weaponData.Add(weaponData);
                    }
                    playerUnitData.Add(unitData);
                }
                pud.unitData = playerUnitData;
                SaveProfileUnitData(pud);
            }
            return pud;

        }
        // END UNIT PROFILE -----------------------------------------------------------------------------

        // START SELECTED PROFILE -----------------------------------------------------------------------
        public void SaveProfileSelectedData(ProfileSelectedData psd)
        {
            SaveData(profileSelectedFilename, psd);
        }

        private ProfileSelectedData LoadProfileSelectedData()
        {
            ProfileSelectedData psd = new ProfileSelectedData();
            if (!LoadData(profileSelectedFilename, psd))
            {
                psd.selectedUnit = 0;
                psd.selectedStageLevel = 0;
                SaveProfileSelectedData(psd);
            }

            return psd;
        }
        // END SELECTED PROFILE -------------------------------------------------------------------------

        // SAVE AND LOAD DATA ---------------------------------------------------------------------------
        private void SaveData(string filename, object objectData)
        {
            string destination = Application.persistentDataPath + "/" + filename + fileExtension;

            string json = JsonUtility.ToJson(objectData);

            File.WriteAllText(destination, json);

#if UNITY_EDITOR
            Debug.Log(filename + " Data File Save: " + destination);
#endif

        }

        private bool LoadData(string filename, object objectData)
        {
            string destination = Application.persistentDataPath + "/" + filename + fileExtension;

            if (File.Exists(destination))
            {
                string json = File.ReadAllText(destination);

                JsonUtility.FromJsonOverwrite(json, objectData);

                return true;
            }

#if UNITY_EDITOR
            Debug.Log(filename + " Data File does not exist: " + destination);
#endif

            return false;

        }
        // END SAVE AND LOAD DATA --------------------------------------------------------------------------

    }
}
