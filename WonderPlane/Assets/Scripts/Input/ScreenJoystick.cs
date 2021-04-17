using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class ScreenJoystick : MonoBehaviour
    {
        
        [SerializeField] private Transform innerCircle = null;

        private void Update()
        {
            int i = 0;
            while (i < Input.touchCount)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began)
                {
                    //if (t.position.x < Screen.width / 2 && t.position.y < Screen.height / 2)
                    //{
                    //    outerCircle.position = t.position;
                    //}
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
                    innerCircle.localPosition = new Vector3(0f, 0f, 0f);
                }
                ++i;
            }
        }

    }
}
