using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class KeyboardInput : MonoBehaviour
    {
        private InputManager inputManager;

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
        }

        private void Update()
        {
            if (inputManager.GetInputType() == InputType.KEYBOARD)
            {

                if (Input.GetKey(KeyCode.W))
                {
                    VirtualInput.Instance.moveAscending = true;
                }
                else
                {
                    VirtualInput.Instance.moveAscending = false;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    VirtualInput.Instance.moveDescending = true;
                }
                else
                {
                    VirtualInput.Instance.moveDescending = false;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    VirtualInput.Instance.moveForward = true;
                }
                else
                {
                    VirtualInput.Instance.moveForward = false;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    VirtualInput.Instance.moveBackward = true;
                }
                else
                {
                    VirtualInput.Instance.moveBackward = false;
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    VirtualInput.Instance.attack = true;
                }
                else
                {
                    VirtualInput.Instance.attack = false;
                }

                if (Input.GetKey(KeyCode.F))
                {
                    VirtualInput.Instance.activeSkill1 = true;
                }
                else
                {
                    VirtualInput.Instance.activeSkill1 = false;
                }
            }
        }
    }
}
