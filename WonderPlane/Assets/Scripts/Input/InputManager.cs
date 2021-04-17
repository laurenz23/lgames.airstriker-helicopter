using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public enum InputType
    {
        KEYBOARD,
        ONSCREEN
    }

    public class InputManager : MonoBehaviour
    {
        public InputType inputType;

        public InputType GetInputType()
        {
            if (inputType == InputType.KEYBOARD)
            {
                return InputType.KEYBOARD;
            }
            else
            {
                return InputType.ONSCREEN;
            }
        }

        private void Start()
        {
            if (GetInputType() == InputType.KEYBOARD)
            {
                transform.GetComponentInChildren<KeyboardInput>().gameObject.SetActive(true);
            }
            else
            {
                transform.GetComponentInChildren<ScreenInput>().gameObject.SetActive(true);
            }
        }
    }
}
