using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is attached to debugger object
/// handles the target fps of game and displaying
/// </summary>

namespace game_ideas
{
    public class DisplayFPS : MonoBehaviour
    {

        public GameObject fpsPanel;
        public Text fpsText;
        public Toggle fps30;
        public Toggle fps60;
        public Toggle fps90;
        public Toggle fps120;
        public Toggle fpsUncapped;
        public string display;


        private void Start()
        {
            fpsPanel.SetActive(true);
            StartCoroutine(RecalculateFPS());
        }
        
        // setting up the target fps

        public void FPS30()
        {
            Application.targetFrameRate = 30;
            fps30.enabled = true;
            fps30.isOn = true;
            fpsPanel.SetActive(false);
        }

        public void FPS60()
        {
            Application.targetFrameRate = 60;
            fps60.enabled = true;
            fps60.isOn = true;
            fpsPanel.SetActive(false);
        }

        public void FPS90()
        {
            Application.targetFrameRate = 90;
            fps90.enabled = true;
            fps90.isOn = true;
            fpsPanel.SetActive(false);
        }

        public void FPS120()
        {
            Application.targetFrameRate = 120;
            fps120.enabled = true;
            fps120.isOn = true;
            fpsPanel.SetActive(false);
        }

        public void FPSUncapped()
        {
            Application.targetFrameRate = 1000;
            fpsUncapped.enabled = true;
            fpsUncapped.isOn = true;
            fpsPanel.SetActive(false);
        }

        // displaying to player and calculating fps
        IEnumerator RecalculateFPS()
        {
            while(true)
            {
                float fps = 1 / Time.deltaTime;
                fpsText.text = fps.ToString("FPS: 0");

                yield return new WaitForSeconds(.25f);
            }
        }

    }
}
