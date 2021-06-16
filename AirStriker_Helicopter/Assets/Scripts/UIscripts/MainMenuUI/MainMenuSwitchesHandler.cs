using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the event for navigating to other panel
/// </summary>

namespace game_ideas
{
    public class MainMenuSwitchesHandler : MonoBehaviour
    {
        public SoundManager soundManager;
        public GameObject mainMenuBackground;
        public Animator stageRotator;
        public GameObject playButton_particles;

        private Animator animator;

        private string panel_id = "PANEL_ID";

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        // panel id list: 0 -> MainMenu, 1 -> Armory, 2 -> Map

        public void MainMenuToArmory()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition1");

            animator.SetInteger(panel_id, 1);
            stageRotator.SetInteger(panel_id, 1);
            playButton_particles.SetActive(false);
        }

        public void ArmoryToMainMenu()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition1");

            animator.SetInteger(panel_id, 0);
            stageRotator.SetInteger(panel_id, 0);
            playButton_particles.SetActive(true);
        }

        public void MainMenuToMap()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition2");

            animator.SetInteger(panel_id, 2);
            StartCoroutine(HideObjects());
        }

        public void MapToMainMenu()
        {
            soundManager.soundFXHandler.SFX_UI_TRANSITION("transition2");

            animator.SetInteger(panel_id, 0);
            stageRotator.gameObject.SetActive(true);
            mainMenuBackground.SetActive(true);
            playButton_particles.SetActive(true);
        }

        public void StartMission()
        {
            soundManager.soundFXHandler.SFX_OTHER("mission_start1");
        }

        IEnumerator HideObjects()
        {
            yield return new WaitForSeconds(.5f);
            stageRotator.gameObject.SetActive(false);
            mainMenuBackground.SetActive(false);
            playButton_particles.SetActive(false);
            StopCoroutine(HideObjects());
        }

    }
}
