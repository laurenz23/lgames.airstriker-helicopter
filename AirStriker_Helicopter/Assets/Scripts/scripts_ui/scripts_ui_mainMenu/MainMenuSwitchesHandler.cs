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
        [SerializeField] private MainMenuUIHandler mainMenuHandler;
        [SerializeField] private ArmoryUIManager armoryUIManager;
        [SerializeField] private UnitArmoryManager unitArmoryManager;

        [HideInInspector]
        public InputControls inputControls; // ui input reference

        private Animator animator; // reference of animator when switching panels
        private string panel_id = "PANEL_ID"; // animation parameters [panel id list: 0 -> MainMenu, 1 -> Armory, 2 -> Map]

        private void Start()
        {
            animator = GetComponent<Animator>();
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

            unitArmoryManager.UnselectUnresearchWeapon(); // hide unresearch weapons
            armoryUIManager.DestroyWeaponUIList(); // destroy all item weapon ui list
        }

        // trigger main menu to map animations and functions
        public void MainMenuToMap()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition2");

            animator.SetInteger(panel_id, 2);
            StartCoroutine(HideObjects()); // hide main menu objects 
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
                mainMenuHandler.SetPlayButton(); // update play button ui
                mainMenuHandler.SetPlayerDeploymentCapsule(); // update deployment capsule ui
                StartCoroutine(DelayOfStart()); // start the coroutine for switching to game play scene
            }
            else // player don't have deployment capsule
            {
                mainMenuHandler.ShowPopupMessage("Insuficient Deployment Capsule!"); // display an error informing the player that he don't have enough deployment capsule
            }
        }

        // delay to switch to game play scene
        IEnumerator DelayOfStart()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Level1"); 
            StopCoroutine(DelayOfStart());
        }

    }
}
