using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script handles the movement in x axis direction
/// object moves in x axis direction while following the target  
/// NOTE: For some reason if the character will not able to move
///       change the value of Rigidbody Drag and Angular Drag
///       UNITY EDITOR BUG or SCRIPT LINES
/// </summary>

namespace game_ideas
{
    public class OnTargetMovementHorizontal : MonoBehaviour
    {
        public EnemyHandler enemyHandler;

        public float keepDistance; // this will make the object keeping it's distance from the target

        [SerializeField]
        private DetectionHandler zDetector; // reference for detection in his front or back, use for detecting objects on front or back

        [SerializeField]
        private DetectionHandler yDetector; // reference for detection in down or up direction, use for detecting grounds


        private Transform playerTransform;

        private float posZ;

        private Vector3 directionToMove; // reference for direction on where the object will move

        private CameraManager cameraManager;

        private Vector3 screenBounds;

        private GameManager gameManager;


        private void Awake()
        {

            gameManager = GameManager.GetInstance();

            cameraManager = FindObjectOfType<CameraManager>();

            if (FindObjectOfType<PlayerManager>())
            {
                playerTransform = FindObjectOfType<PlayerManager>().playerTransform;
            }

        }

        private void Start()
        {

            // check if the character is facing the z axis forward or backward
            if (enemyHandler.transform.eulerAngles.y.Equals(0f))
            {
                // once the character is facing the forward from z axis
                // character will move forward 
                // since the character is facing forward we keep the distance a little late
                directionToMove = Vector3.forward;
                keepDistance = -keepDistance;
            }
            else
            {
                // the character is facing back from z axis
                // character will move backward
                directionToMove = Vector3.back;
            }

        }

        private void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                if (zDetector != null && yDetector != null)
                {
                    // if theres an object to his front or back the character will not move
                    if (zDetector.OnDetected())
                    {
                        return;
                    }

                    // if not detected any grounds the character will not move
                    if (yDetector.OnDetected().Equals(false))
                    {
                        return;
                    }
                }
                
                screenBounds = cameraManager.screenBounds;

                // don't move the character if already in the right corner of camera view
                // disregard the distance between the target
                if (enemyHandler.transform.position.z >= screenBounds.z)
                {
                    return;
                }

                posZ = playerTransform.position.z + keepDistance;

                // if character is not on distance to the target it will move
                if (enemyHandler.transform.position.z < posZ)
                {
                    enemyHandler.transform.Translate(directionToMove * enemyHandler.enemyData.movementSpeed * Time.deltaTime);
                }

                // move the character if in the left corner of camera view in order the character will not left behind
                // disregard the distance between the target
                if (enemyHandler.transform.position.z <= ((cameraManager.transform.position.z * 2f) - screenBounds.z))
                {
                    enemyHandler.transform.Translate(directionToMove * enemyHandler.enemyData.movementSpeed * Time.deltaTime);
                }

            }

        }

    }
}
