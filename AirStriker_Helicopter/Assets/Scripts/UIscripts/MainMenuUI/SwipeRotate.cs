using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script attached to touch manager object
/// handles the rotation of character when touch phase 
/// is moved
/// </summary>

namespace game_ideas
{
    public class SwipeRotate : MonoBehaviour
    {
        public float rotationSpeed;

        public PlatformRotator platform;

        private Touch touch;

        private Vector2 touchPosition;

        // Update is called once per frame
        void Update()
        {

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    platform.transform.eulerAngles = new Vector3(platform.transform.eulerAngles.x, platform.transform.eulerAngles.y - (touch.deltaPosition.x * rotationSpeed), platform.transform.eulerAngles.z);
                    platform.enabled = false;
                }
            }
            else
            {
                platform.enabled = true;
            }

        }
    }
}
