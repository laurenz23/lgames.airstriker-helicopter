using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class ScreenJoystick : MonoBehaviour
    {
        public Transform innerCircle = null;

        private int joystickTouch;

        private void Start()
        {
            joystickTouch = -1;
        }

        public void Update()
        {
            int i = 0;

            while (i < Input.touchCount)
            {
                Touch t = Input.GetTouch(i);

                if (t.phase == TouchPhase.Began)
                {
                    if (t.position.x < Screen.width / 2 && t.position.y < Screen.height / 2)
                    {
                        joystickTouch = t.fingerId; // assign finger id to joystick touch for reference when touch is already ended or canceled
                        innerCircle.position = t.position;
                    }
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    if (t.position.x < Screen.width / 2 && t.position.y < Screen.height / 2)
                    {
                        innerCircle.position = t.position;
                    }
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    if (t.fingerId == joystickTouch) // if the assign joystick touch already ended, set the inner circle position to default
                    {
                        innerCircle.localPosition = new Vector3(0f, 0f, 0f);
                    }
                }
                else if (t.phase == TouchPhase.Canceled)
                {
                    if (t.fingerId == joystickTouch) // if the assign joystick touch already canceled, set the inner circle position to default
                    {
                        innerCircle.localPosition = new Vector3(0f, 0f, 0f);
                    }
                }

                ++i;
            }
        }

    }
}
