﻿using System.Collections;
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

        [SerializeField] private CameraManager cameraManager = null; // player reference for boundaries

        private PlayerManager playerManager;
        private Vector3 screenBounds; // player reference for boundaries

        private void Awake()
        {
            cameraManager = FindObjectOfType<CameraManager>();
            playerManager = GetComponent<PlayerManager>();
        }

        public void PlayerMove(bool moveForward, bool moveBackward, bool moveAscending, bool moveDescending)
        {

            screenBounds = cameraManager.screenBounds;

            if (moveForward && !moveBackward)
            {

                if (!(transform.position.z >= screenBounds.z))
                {
                    transform.Translate(Vector3.forward * (playerManager.moveSpeed + 10f) * Time.deltaTime);
                }

            }
            else if (!moveForward && moveBackward)
            {

                if (!(transform.position.z <= ((cameraManager.transform.position.z * 2f) - screenBounds.z)))
                {
                    transform.Translate(Vector3.back * (playerManager.moveSpeed + 10f) * Time.deltaTime);
                }

            }

            if (moveAscending && !moveDescending)
            {

                if (!(transform.position.y >= screenBounds.y))
                {
                    transform.Translate(Vector3.up * (playerManager.moveSpeed + 5f) * Time.deltaTime);
                }

            }
            else if (!moveAscending && moveDescending)
            {

                if (!(transform.position.y <= ((cameraManager.transform.position.y - 12f) - screenBounds.y)))
                {
                    if (!playerManager.PlayerOnGround())
                    {
                        transform.Translate(Vector3.down * (playerManager.moveSpeed + 5f) * Time.deltaTime);
                    }
                }

            }

            transform.Translate(Vector3.forward * playerManager.moveSpeed * Time.deltaTime);

        }


        /*
        * ROLL and ROTATION of player 
        * Handles the smooth of player Rotation
        */

        private int playerLastFrameActionOfPitch = 0; // reference for next frame update last player action of ascending = -1, descending = 1 and neutral = 0

        private float playerMaxPitch = 20f; // maximum rotation of player in x axis

        private float playerPitch = 0f; // rotation of player in x axis

        private float playerRoll = 0f; // rotation of player in z axis

        public void PlayerManuever(bool moveForward, bool moveBackward, bool moveAscending, bool moveDescending)
        {

            // move player Ascending
            if (!moveForward && moveBackward)
            {

                if (!playerManager.PlayerOnGround())
                {
                    // rotate player clock wise with a set maximum value rotation
                    if (playerPitch <= -playerMaxPitch) // if increase rotation overlap the maximum value 
                    {

                        playerPitch = -playerMaxPitch; // assign the player rotation value to default

                    }
                    else
                    {

                        playerPitch -= 100f * Time.deltaTime; // increase the rotation of player clock wise by Time.deltaTime

                    }

                    // set the player last action for reference to convert the playerRotation to negative or positive when releasing the action for smooth rotation back to default
                    playerLastFrameActionOfPitch = -1;
                }

            }
            // move player Descending
            else if (moveForward && !moveBackward)
            {

                if (playerManager.PlayerOnGround()) // don't rotate and descend the player if on land and reset the rotations
                {

                    playerPitch = 0f;
                    playerManager.player.rotation = Quaternion.identity;

                    return;  // do nothing

                }
                else
                {

                    if (playerPitch >= playerMaxPitch) // if increase rotation overlap the maximum value 
                    {

                        playerPitch = playerMaxPitch; // set the player rotation value to default

                    }
                    else
                    {
                        playerPitch += 100f * Time.deltaTime; // increase the rotation of player clock wise by Time.deltaTime

                    }

                }

                // set the player last action for reference to convert the playerRotation to negative or positive when releasing the action for smooth rotation back to default
                playerLastFrameActionOfPitch = 1; // set the player action

            }
            else
            {

                // smooth player rotation back to normal after player release the action
                if (playerLastFrameActionOfPitch == -1) // check last player action ascending
                {

                    if (playerPitch >= 0f) // if decreasing rotation overlap, assign to original values and assign default rotation 
                    {

                        playerPitch = 0f;
                        playerLastFrameActionOfPitch = 0;
                        playerManager.player.rotation = Quaternion.identity;
                        return; // do nothing

                    }
                    else
                    {

                        playerPitch += 100f * Time.deltaTime; // decrease the player rotation

                    }

                }
                else if (playerLastFrameActionOfPitch == 1) // check last player action descending
                {

                    if (playerPitch <= 0f) // if decreasing rotation overlap, assign to original values and assign default rotation 
                    {

                        playerPitch = 0f;
                        playerLastFrameActionOfPitch = 0;
                        playerManager.player.rotation = Quaternion.identity;
                        return; // do nothing

                    }
                    else
                    {

                        playerPitch -= 100f * Time.deltaTime; // decrease the player rotation

                    }

                }

            }


            if (moveAscending && !moveDescending && !moveForward && !moveBackward)
            {

                playerRoll = 60f;
            }
            else if (moveDescending && !moveAscending && !moveForward && !moveBackward)
            {

                playerRoll = -40f;
            }
            else
            {
                playerRoll = 0f;
            }


            // set the player rotation 
            playerManager.player.rotation = Quaternion.Euler(new Vector3(playerPitch, 0f, playerRoll));

        }


    }
}