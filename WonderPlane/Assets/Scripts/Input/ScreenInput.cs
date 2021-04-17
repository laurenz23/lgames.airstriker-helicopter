using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game_ideas
{
    public class ScreenInput : MonoBehaviour
    {
        private InputManager inputManager;
        private ScreenActionInput screenActionInput;

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            screenActionInput = FindObjectOfType<ScreenActionInput>();
        }

        private void Start()
        {
            if (inputManager.GetInputType() == InputType.ONSCREEN)
            {
                foreach (ScreenActionInput sai in FindObjectsOfType<ScreenActionInput>())
                {
                    sai.enabled = true;
                }
            }
        }
    }
}
