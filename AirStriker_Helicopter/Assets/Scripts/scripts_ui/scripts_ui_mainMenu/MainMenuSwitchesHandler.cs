using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// usage:  attached to ui object as a parent
///         handles the event for navigating to other panel
///         and updating player deployment capsule value by performing the start game
/// </summary>

namespace game_ideas
{
    public class MainMenuSwitchesHandler : MonoBehaviour
    {
        public SoundManager soundManager;
        public GameObject mainMenuBackground;
        public Animator stageRotator;
        public PlatformRotator platformRotator;
        public GameObject playButton_particles;
        
        [Header("Script Reference")]
        [SerializeField] private ProfilePlayerDataManager profilePlayerDataManager;
        [SerializeField] private MainMenuUIHandler mainMenuUIHandler;
        [SerializeField] private ArmoryUIManager armoryUIManager;
        [SerializeField] private UnitArmoryManager unitArmoryManager;
        [SerializeField] private LoadSceneManager loadSceneManager;
        [SerializeField] private EnterNamePanel enterNamePanel;

        [HideInInspector]
        public InputControls inputControls; // ui input reference

        private Animator animator; // reference of animator when switching panels
        private string panel_id = "PANEL_ID"; // animation parameters [panel id list: 0 -> MainMenu, 1 -> Armory, 2 -> Map]
        private string have_profile = "HAVE_PROFILE";

        private void Start()
        {
            animator = GetComponent<Animator>();

            // check if have already player profile data
            // if not, player requires to create profile by inputing the name
            // and the game will generate player profile
            if (!profilePlayerDataManager.HavePlayerProfile()) // display the enter name panel
            {
                enterNamePanel.ShowPanel(true);
            }
            else // display the main menu, if player profile data is already exist
            {
                DisplayMainMenu();
            }
        }

        // use to check if have already player data existed or the game first open 
        public void DisplayMainMenu()
        {
            animator.SetBool(have_profile, true);
        }

        // trigger main menu to armory animations and functions
        public void MainMenuToArmory()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition1");

            animator.SetInteger(panel_id, 1); 
            stageRotator.SetInteger(panel_id, 1);
            platformRotator.ArmoryPlatform(5f); // change platform touch input area
            playButton_particles.SetActive(false); // hide play button particles in armory panel

            armoryUIManager.DisplayWeaponsUIList(unitArmoryManager.GetUnitIndex()); // create selected unit item weapon ui list
        }

        // trigger armory to main menu animations and functions
        public void ArmoryToMainMenu()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition1");

            animator.SetInteger(panel_id, 0);
            stageRotator.SetInteger(panel_id, 0);
            platformRotator.MainMenuPlatform(5f); // change platform touch input area
            playButton_particles.SetActive(true); // display play button particles in main menu

            // check if unit have atlease one non-special weapon and update mission ui group buttons
            if (unitArmoryManager.HaveWeapon())
                mainMenuUIHandler.SetMissionButton(true);
            else
                mainMenuUIHandler.SetMissionButton(false);

            unitArmoryManager.UnselectUnresearchWeapon(); // hide unresearch weapons
            armoryUIManager.DestroyWeaponUIList(); // destroy all item weapon ui list
        }

        // trigger main menu to map animations and functions
        public void MainMenuToMap()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition2");

            // check if unit have atleast one non-special weapon before can enter to world map or start a mission
            if (unitArmoryManager.HaveWeapon())
            {
                animator.SetInteger(panel_id, 2);
                StartCoroutine(HideObjects()); // hide main menu objects 
            }
            else
            {
                mainMenuUIHandler.ShowPopupMessage("PLEASE RESEARCH A WEAPON");
            }
        }

        IEnumerator HideObjects()
        {
            yield return new WaitForSeconds(.5f);
            stageRotator.gameObject.SetActive(false);
            mainMenuBackground.SetActive(false);
            playButton_particles.SetActive(false);
            StopCoroutine(HideObjects());
        }

        // trigger map to main menu animations and functions
        public void MapToMainMenu()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition2");

            animator.SetInteger(panel_id, 0);
            
            // display main menu objects
            stageRotator.gameObject.SetActive(true);
            mainMenuBackground.SetActive(true);
            playButton_particles.SetActive(true);
        }
        
        private bool gameInitiated = false; // reference if already triggered switch to game play scene, we apply delay before switching the scene

        // call this function to start the mission
        public void StartMission()
        {
            if (gameInitiated) // we apply delay before switching to game play scene
            {
                // if player will press again the start button it will disregard the start mission action
                // since the mission already initiated, wait until the delay of start is executed
                return;
            }

            int deploymentCapsule = profilePlayerDataManager.profileTokensData.playerDeploymentCapsule; // get how many deployment capsule player have

            if (deploymentCapsule > 0) // player have deployment capsule
            {
                soundManager.soundFXHandler.SFX_OTHER("mission_start1");

                gameInitiated = true; 
                profilePlayerDataManager.SetDeploymentCapsuleData(deploymentCapsule - 1); // deduct player deployment capsule and save data
                mainMenuUIHandler.SetPlayButton(); // update play button ui
                mainMenuUIHandler.SetPlayerDeploymentCapsule(); // update deployment capsule ui
                StartCoroutine(DelayOfStart()); // start the coroutine for switching to game play scene
            }
            else // player don't have deployment capsule
            {
                mainMenuUIHandler.ShowPopupMessage("Insuficient Deployment Capsule!"); // display an error informing the player that he don't have enough deployment capsule
            }
        }

        // delay to switch to game play scene
        IEnumerator DelayOfStart()
        {
            yield return new WaitForSeconds(1f);
            loadSceneManager.LoadScene("Level1");
            StopCoroutine(DelayOfStart());
        }

    }
}
