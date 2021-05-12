using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manages the camera of game
/// checks the boundary of the player field of view
/// </summary>

namespace game_ideas
{
    public class CameraManager : MonoBehaviour
    {

        public float maxY_axis;

        public float minY_axis;

        public Transform targetObject; // target movement for camera

        public Transform optimizationObject;

        [HideInInspector]
        public Transform targetPlayer; // target player position

        [HideInInspector]
        public Vector3 screenBounds;

        [HideInInspector]
        public Camera mainCamera;

        private void Awake()
        {

            mainCamera = GetComponent<Camera>();

        }

        private void Start()
        {
            targetPlayer = FindObjectOfType<PlayerManager>().playerTransform;
        }

        private void Update()
        {

            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.x));
            optimizationObject.position = new Vector3(0f, targetPlayer.localPosition.y, transform.position.z);

        }

        private void LateUpdate()
        {

            CameraBound();
            
        }
        

        private float boundY = 5f; // assign the maximum of bound Y 
        private float boundTransition = 0f;
        private Vector3 desiredPosition;

        // this method handles the camera bound of plane when plane is moving vertically
        private void CameraBound()
        {

            boundTransition += Time.deltaTime; // increase the bound transition

            Vector3 delta = Vector3.zero; 

            // y Axis
            float dy = (targetPlayer.position.y - transform.position.y) + 5f; // assign the y position of camera

            /*
             * check if plane position is greater than maximum bound of Y
             * positive bound Y if plane is going upward, negative bound Y if plane is going downward
             * then follow the plane position 
             */

            if (dy > boundY || dy < -boundY) 
            {

                if (transform.position.y < targetPlayer.position.y)
                {

                    if (transform.position.y < maxY_axis)
                    {
                        delta.y = dy - boundY; // assign negative value of y vector3 if plane is moving downward
                    }

                }
                else
                {
                    if (transform.position.y > minY_axis)
                    {
                        delta.y = dy + boundY; // assign positive value of y vector3 if plane is moving to upward
                    }

                }

            }

            // z Axis
            float dz = targetObject.position.z - transform.position.z; // assign the z position of camera

            delta.z = dz;

            // assign the camera position of camera x, y and z
            desiredPosition = transform.position + delta;

            // set the position of camera using lerp to have smooth transition
            transform.position = Vector3.Lerp(transform.position, desiredPosition, boundTransition);
            
        }

    }

}
