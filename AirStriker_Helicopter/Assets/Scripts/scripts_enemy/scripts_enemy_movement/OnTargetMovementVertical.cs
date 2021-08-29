using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script handles the movement in y axis direction
/// object moves in y axis direction while following the target
/// </summary>

namespace game_ideas
{
    public class OnTargetMovementVertical : MonoBehaviour
    {
        public EnemyHandler enemyHandler;

        public float keepDistance_upWard; // this will make the object keeping it's distance from the target

        public float keepDistance_forward; // this will make the object keeping it's distance from the target

        [Header("Apply Delay when following Target")]
        public bool applyDelay;

        public float delayTime;

        private float posY; 

        private float posZ;

        private Transform playerTransform;

        private Vector3 screenBounds;

        private CameraManager cameraManager;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.GetInstance();

            cameraManager = FindObjectOfType<CameraManager>();

            playerTransform = FindObjectOfType<PlayerManager>().playerTransform; // set the player as the target
        }

        private void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {

                screenBounds = cameraManager.screenBounds;

                // calculate the distance of player and  character in y axis
                posY = playerTransform.position.y + keepDistance_upWard;

                // if character is facing positive z axis
                if (enemyHandler.transform.eulerAngles.y.Equals(0f))
                {
                    if (keepDistance_forward == 0f)
                    {
                        enemyHandler.transform.Translate(Vector3.forward * gameManager.horizontalForwardSpeed * Time.deltaTime);
                        posZ = enemyHandler.transform.position.z;
                    }
                    else
                    {
                        // calculate the distance of player and character in z axis 
                        // we subtract the player position and character position in z axis
                        // since the character is facing forward
                        posZ = playerTransform.position.z - keepDistance_forward;
                    }

                    // check if the character reach the left corner of camera field view
                    if (enemyHandler.transform.position.z <= ((cameraManager.transform.position.z * 2f) - screenBounds.z))
                    {
                        // if the distance of enemy position and player position is less than to declared keep distance then stop following the player
                        // but once the distance of enemy position and player position is greater than to declared keep distance
                        // move the character forward and start following the player 
                        if ((playerTransform.position.z - enemyHandler.transform.position.z) > keepDistance_forward)
                        {
                            enemyHandler.transform.Translate(Vector3.forward * (gameManager.horizontalForwardSpeed + 2f) * Time.deltaTime);
                        }
                        // stop following the player position and stay stationary
                        else
                        {
                            enemyHandler.transform.Translate(Vector3.forward * gameManager.horizontalForwardSpeed * Time.deltaTime);
                        }

                        // don't follow the player position and disregard distance
                        return;
                    }

                    // follow the player position
                    enemyHandler.transform.position = Vector3.MoveTowards(enemyHandler.transform.position, new Vector3(0f, posY, posZ), enemyHandler.enemyData.movementSpeed * Time.deltaTime);
                }
                // if characer is facing negative z axis
                else
                {
                    if (keepDistance_forward == 0f)
                    {
                        enemyHandler.transform.Translate(Vector3.forward * gameManager.horizontalForwardSpeed * Time.deltaTime);
                        posZ = enemyHandler.transform.position.z;
                    }
                    else
                    {
                        // calculate the player position
                        posZ = playerTransform.position.z + keepDistance_forward;
                    }

                    // if the character reach the right corner of camera field view
                    // stop the movement of character in z axis
                    // but if the character haven't reach
                    // just keep following the player
                    if (!(enemyHandler.transform.position.z >= screenBounds.z))
                    {
                        // follow the player position
                        enemyHandler.transform.position = Vector3.MoveTowards(enemyHandler.transform.position, new Vector3(0f, posY, posZ), enemyHandler.enemyData.movementSpeed * Time.deltaTime);
                    }
                }
            }

        }

    }
}
