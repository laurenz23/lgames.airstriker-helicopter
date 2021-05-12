using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace game_ideas
{
    public class ScreenActionInput : MonoBehaviour
    {
        public ActionType actionType;
        public bool buttonAnimation;

        private InputManager inputManager;
        private ScreenInput screenInput;
        [SerializeField] private ScreenJoystick screenJoystick;
        // private string animName = "TAP"; // if animation is applied use this variable

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            screenInput = FindObjectOfType<ScreenInput>();
        }

        private void Start()
        {
            if (GetComponent<ScreenJoystick>())
            {
                screenJoystick = GetComponent<ScreenJoystick>();
            }
        }

        private void Update()
        {
            if (inputManager.GetInputType() == InputType.ONSCREEN)
            {
                // if game control is joystick
                if (actionType == ActionType.JOYSTICK)
                {

                    if (screenJoystick.innerCircle.localPosition.x > 20)
                    {
                        VirtualInput.Instance.moveForward = true;
                    }
                    else
                    {
                        VirtualInput.Instance.moveForward = false;
                    }

                    if (screenJoystick.innerCircle.localPosition.x < -20)
                    {
                        VirtualInput.Instance.moveBackward = true;
                    }
                    else
                    {
                        VirtualInput.Instance.moveBackward = false;
                    }

                    if (screenJoystick.innerCircle.localPosition.y > 20)
                    {
                        VirtualInput.Instance.moveAscending = true;
                    }
                    else
                    {
                        VirtualInput.Instance.moveAscending = false;
                    }

                    if (screenJoystick.innerCircle.localPosition.y < -20)
                    {
                        VirtualInput.Instance.moveDescending = true;
                    }
                    else
                    {
                        VirtualInput.Instance.moveDescending = false;
                    }
                }
            }
        }
        
        // player movement functions if game controls is buttons

        // player move forward
        public void MoveForwardDown()
        {
            VirtualInput.Instance.moveForward = true;
        }

        public void MoveForwardUp()
        {
            VirtualInput.Instance.moveForward = false;
        }


        // player move backward
        public void MoveBackwardDown()
        {
            VirtualInput.Instance.moveBackward = true;
        }
        
        public void MoveBackwardUp()
        {
            VirtualInput.Instance.moveBackward = false;
        }


        // player move ascending
        public void MoveAscendingDown()
        {
            VirtualInput.Instance.moveAscending = true;
        }

        public void MoveAscendingUp()
        {
            VirtualInput.Instance.moveAscending = false;
        }


        // player move descending
        public void MoveDescendingDown()
        {
            VirtualInput.Instance.moveDescending = true;
        }

        public void MoveDescendingUp()
        {
            VirtualInput.Instance.moveDescending = false;
        }


        // player attack
        public void AttackDown()
        {
            VirtualInput.Instance.attack = true;
        }

        public void AttackUp()
        {
            VirtualInput.Instance.attack = false;
        }


        // player automic attack
        public void AutomicDown()
        {
            VirtualInput.Instance.automic = true;
        }

        public void AutomicUp()
        {
            VirtualInput.Instance.automic = false;
        }

    }
}
