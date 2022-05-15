using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attach to player maanager object
/// handles the movement of the player
/// </summary>

namespace game_ideas
{
    public class PlayerMovement : MonoBehaviour
    {

        public PlayerManager playerManager;

        private float horizontalForwardSpeed; // the speed of game moving camera 

        private void Start()
        {
            horizontalForwardSpeed = playerManager.horizontalForwardSpeed;
        }

        public void PlayerMove(bool moveForward, bool moveBackward, bool moveAscending, bool moveDescending)
        {

            // move player forward
            if (moveForward && !moveBackward)
            {
                // move player forward if player z axis is inside of screen bounds right
                if (!(transform.position.z >= playerManager.cameraManager.screenBounds.z))
                {
                    transform.Translate(Vector3.forward * (playerManager.moveSpeed + 4f) * Time.deltaTime);
                }

            }
            // move player backward
            else if (!moveForward && moveBackward)
            {
                // move player backward if player z axis is inside of screen bounds left
                if (!(transform.position.z <= ((playerManager.cameraManager.transform.position.z * 2f) - playerManager.cameraManager.screenBounds.z)))
                {
                    transform.Translate(Vector3.back * (playerManager.moveSpeed + 4f) * Time.deltaTime);
                }

            }

            // move player upward
            if (moveAscending && !moveDescending)
            {
                // move player upward if player y axis is inside of screen bounds top
                if (!(transform.position.y >= playerManager.cameraManager.screenBounds.y))
                {
                    if (moveBackward || moveForward)
                    {
                        transform.Translate(Vector3.up * (playerManager.moveSpeed) * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(Vector3.up * (playerManager.moveSpeed + horizontalForwardSpeed) * Time.deltaTime);
                    }
                }

            }
            // move player downward 
            else if (!moveAscending && moveDescending)
            {
                // move player downward if player y axis is inside of screen bounds bottom
                if (!(transform.position.y <= ((playerManager.cameraManager.transform.position.y - 12f) - playerManager.cameraManager.screenBounds.y)))
                {
                    if (!playerManager.PlayerOnGround())
                    {
                        if (moveBackward || moveForward)
                        {
                            transform.Translate(Vector3.down * (playerManager.moveSpeed) * Time.deltaTime);
                        }
                        else
                        {
                            transform.Translate(Vector3.down * (playerManager.moveSpeed + horizontalForwardSpeed) * Time.deltaTime);
                        }
                    }
                }

            }

            transform.Translate(Vector3.forward * horizontalForwardSpeed * Time.deltaTime);

        }


        /*
        * ROLL and YAW of player 
        * Handles the smooth of player Rotation
        */

        private int playerLastFrameActionOfYaw = 0; // reference for next frame update last player action of ascending = -1, descending = 1 and neutral = 0

        private float playerMaxYaw = 20f; // maximum rotation of player in x axis

        private float playerYaw = 0f; // rotation of player in x axis

        private float playerRoll = 0f; // rotation of player in z axis

        public void PlayerManuever(bool moveForward, bool moveBackward, bool moveAscending, bool moveDescending)
        {

            // helicopter yaw
            // player reverse acceleration
            if (moveForward && !moveBackward)
            {
                PlayerAccelerationMovement();
            }
            // player reverse
            else if (!moveForward && moveBackward)
            {
                PlayerReverseMovement();
            }
            // reset player rotation smoothly
            else
            {
                PlayerResetRotationMovement();
            }

            // helicopter roll
            if (moveAscending && !moveDescending && !moveForward && !moveBackward && !playerManager.PlayerOnGround())
            {
                playerRoll = 60f;
            }
            else if (moveDescending && !moveAscending && !moveForward && !moveBackward && !playerManager.PlayerOnGround())
            {

                playerRoll = -40f;
            }
            else
            {
                playerRoll = 0f;
            }


            // set the player rotation 
            playerManager.playerTransform.rotation = Quaternion.Euler(new Vector3(playerYaw, 0f, playerRoll));

        }
        
        // Player Level Complete Movement
        public void LevelCompleteMovement()
        {
            transform.Translate(Vector3.forward * (playerManager.moveSpeed + 4f) * Time.deltaTime);

            if (playerYaw >= playerMaxYaw) // if increase yaw or overlap the maximum value 
            {

                playerYaw = playerMaxYaw; // set the yaw value to default

            }
            else
            {
                playerYaw += 100f * Time.deltaTime; // increase the yaw of helicopter clock wise

            }
            
            if (transform.position.z >= playerManager.cameraManager.screenBounds.z)
            {
                playerManager.gameObject.SetActive(false);
            }


            // set the player rotation 
            playerManager.playerTransform.rotation = Quaternion.Euler(new Vector3(playerYaw, 0f, 0f));
        }

        // smooth player acceleration helicopter yaw movement 
        private void PlayerAccelerationMovement()
        {
            if (playerManager.PlayerOnGround()) // don't yaw the player if on land and reset yaw(x axis rotaion) values
            {

                playerYaw = 0f;
                playerManager.playerTransform.rotation = Quaternion.identity;

                return;  // do nothing

            }
            else
            {
                if (playerYaw >= playerMaxYaw) // if increase yaw or overlap the maximum value 
                {

                    playerYaw = playerMaxYaw; // set the yaw value to default

                }
                else
                {
                    playerYaw += 100f * Time.deltaTime; // increase the yaw of helicopter clock wise

                }

            }

            // set the player last action for reference to convert the the helicopter yaw to negative or positive when releasing the action for smooth yaw back to default
            playerLastFrameActionOfYaw = 1; // set the player action
        }

        // smooth player reverse helicopter yaw movement
        private void PlayerReverseMovement()
        {
            if (!playerManager.PlayerOnGround())
            {
                // yaw player clock wise with a set maximum value rotation
                if (playerYaw <= -playerMaxYaw) // if increase yaw overlap the maximum value 
                {

                    playerYaw = -playerMaxYaw; // assign yaw value to default

                }
                else
                {

                    playerYaw -= 100f * Time.deltaTime; // decrease yaw value clock wise

                }

                // set the player last action for reference to convert yaw to negative or positive when releasing the action for smooth yaw back to default
                playerLastFrameActionOfYaw = -1;
            }
        }

        // reset smooth helicopter yaw movement
        private void PlayerResetRotationMovement()
        {
            // smooth yaw back to normal after player release the action
            if (playerLastFrameActionOfYaw == -1) // check last player action acceleration
            {

                if (playerYaw >= 0f) // if decreasing yaw overlap, assign to original values and assign default yaw value 
                {

                    playerYaw = 0f;
                    playerLastFrameActionOfYaw = 0;
                    playerManager.playerTransform.rotation = Quaternion.identity;
                    return; // do nothing

                }
                else
                {

                    playerYaw += 100f * Time.deltaTime; // increase the yaw

                }

            }
            else if (playerLastFrameActionOfYaw == 1) // check last player action descending
            {

                if (playerYaw <= 0f) // if decreasing yaw overlap, assign to original values and assign yaw values
                {

                    playerYaw = 0f;
                    playerLastFrameActionOfYaw = 0;
                    playerManager.playerTransform.rotation = Quaternion.identity;
                    return; // do nothing

                }
                else
                {

                    playerYaw -= 100f * Time.deltaTime; // decrease the yaw value

                }

            }
        }
        

    }
}
