using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace game_ideas
{
    public enum ActionType
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        MOVEASCENDING,
        MOVEDESCENDING,
        JOYSTICK,
        ATTACK,
        AUTOMIC
    }

    public class ScreenActionInput : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
    {
        public ActionType actionType;
        public bool buttonAnimation;

        private InputManager inputManager;
        private ScreenInput screenInput;
        private bool onClick;
        private string animName = "TAP";

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            screenInput = FindObjectOfType<ScreenInput>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onClick = true;

            if (buttonAnimation)
            {
                GetComponent<Animator>().SetBool(animName, true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //onClick = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onClick = false;

            if (buttonAnimation)
            {
                GetComponent<Animator>().SetBool(animName, false);
            }
        }

        private void Update()
        {
            if (inputManager.GetInputType() == InputType.ONSCREEN)
            { 
                if (actionType == ActionType.JOYSTICK)
                {

                    if (transform.localPosition.x > 20)
                    {
                        VirtualInput.Instance.moveForward = true;
                    }
                    else
                    {
                        VirtualInput.Instance.moveForward = false;
                    }

                    if (transform.localPosition.x < -20)
                    {
                        VirtualInput.Instance.moveBackward = true;
                    }
                    else
                    {
                        VirtualInput.Instance.moveBackward = false;
                    }

                    if (transform.localPosition.y > 20)
                    {
                        VirtualInput.Instance.moveAscending = true;
                    }
                    else
                    {
                        VirtualInput.Instance.moveAscending = false;
                    }

                    if (transform.localPosition.y < -20)
                    {
                        VirtualInput.Instance.moveDescending = true;
                    }
                    else
                    {
                        VirtualInput.Instance.moveDescending = false;
                    }

                    return;
                }

                if (onClick)
                {
                    if (actionType == ActionType.ATTACK)
                    {
                        VirtualInput.Instance.attack = true;
                    }

                    if (actionType == ActionType.AUTOMIC)
                    {
                        VirtualInput.Instance.automic = true;
                    }
                }
                else
                {
                    if (actionType == ActionType.ATTACK)
                    {
                        VirtualInput.Instance.attack = false;
                    }

                    if (actionType == ActionType.AUTOMIC)
                    {
                        VirtualInput.Instance.automic = false;
                    }

                    if (actionType == ActionType.JOYSTICK)
                    {
                        VirtualInput.Instance.moveForward = false;
                        VirtualInput.Instance.moveBackward = false;
                        VirtualInput.Instance.moveAscending = false;
                        VirtualInput.Instance.moveDescending = false;
                    }
                }
            }
        }
    }
}
