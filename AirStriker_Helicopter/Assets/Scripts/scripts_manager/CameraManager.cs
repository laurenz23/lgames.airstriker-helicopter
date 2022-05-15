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

        private static CameraManager instance;

        public static CameraManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }

            mainCamera = GetComponent<Camera>();

        }

        public float maxY_axis;

        public float minY_axis;

        public Transform targetMovementHandler; // target movement for camera

        public Transform gamePlayOptimizationManager;

        [HideInInspector]
        public Transform targetPlayer; // target player position

        [HideInInspector]
        public Vector3 screenBounds;

        [HideInInspector]
        public Camera mainCamera;

        private GameManager gameManager;

        private void Start()
        {
            targetPlayer = PlayerManager.GetInstance().playerTransform;
            gameManager = GameManager.GetInstance();
        }

        private void Update()
        {

            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.x));
            gamePlayOptimizationManager.position = new Vector3(0f, targetPlayer.localPosition.y, transform.position.z);

        }

        private void LateUpdate()
        {
            if (gameManager.gameState != GameState.LEVEL_COMPLETE)
            {
                CameraBound();
            }
            
        }

        public Vector3 ConvertToWorldPos(Vector3 tObject)
        {
            return mainCamera.ScreenToWorldPoint(tObject);
        }
        

        private float boundY = 5f; // assign the maximum of bound Y 
        private float boundTransition = 0f;
        private Vector3 desiredPosition;

        // this method handles the camera bound of player when player is moving vertically
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
            float dz = targetMovementHandler.position.z - transform.position.z; // assign the z position of camera

            delta.z = dz;

            // assign the camera position of camera x, y and z
            desiredPosition = transform.position + delta;

            // set the position of camera using lerp to have smooth transition
            transform.position = Vector3.Lerp(transform.position, desiredPosition, boundTransition);
            
        }

        public bool IfView(Transform objTransform)
        {
            if (
                !(objTransform.position.z >= screenBounds.z) &&
                !(objTransform.position.z <= ((transform.position.z * 2f) - screenBounds.z)) &&
                !(objTransform.position.y >= screenBounds.y) &&
                !(objTransform.position.y <= ((transform.position.y - 12f) - screenBounds.y))
                )
            {
                return true;
            }

            return false;
        }

    }

}
