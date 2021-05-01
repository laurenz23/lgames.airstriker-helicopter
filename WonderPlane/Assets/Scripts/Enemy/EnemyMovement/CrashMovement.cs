﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached this script is a child of the main character
/// will move towards to target object and crash to target while in action
/// but it will move only to target if object can rotate to it's assign maximum pitch
/// NOTE:   
///         this script have still a bug going on
///         if the target object will move at the back of the character exiting the maximum pitch
///         then if the target object enters the maximum pitch while rotation of character
///         is at oppisite direction
///         then the bugs occure where the pitch rotation will snap to target
/// </summary>

namespace game_ideas
{
    public class CrashMovement : MonoBehaviour
    {
        public Transform character;

        public float speed;

        public float maximumPitch;

        [HideInInspector]
        public GameManager gameManager;

        [HideInInspector]
        public Transform targetPlayer;

        private void Awake()
        {

            gameManager = GameManager.GetInstance();

            if (FindObjectOfType<PlayerManager>())
            {
                targetPlayer = FindObjectOfType<PlayerManager>().GetPlayerTransform();
            }

        }

        private void Update()
        {
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                if (targetPlayer != null)
                {

                    Vector3 direction = new Vector3(0f, 0f, 0f);

                    // check if character is rotated by y axis
                    // and assign a formula base on it's rotation
                    if (character.rotation.y.Equals(0f))
                    {
                        direction = targetPlayer.position - character.position;
                    }
                    else
                    {
                        direction = character.position - targetPlayer.position;
                    }
                    
                    float anglePitch = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;

                    // apply rotation of character if inside the angle picth
                    if (anglePitch >= -maximumPitch && anglePitch <= maximumPitch)
                    {

                        // if character is rotated to 180 degress from y axis then angle pitch is absolute
                        if (character.rotation.y.Equals(0f))
                        {
                            character.rotation = Quaternion.Euler(-anglePitch, character.eulerAngles.y, character.eulerAngles.z);
                        }
                        else
                        {
                            character.rotation = Quaternion.Euler(anglePitch, character.eulerAngles.y, character.eulerAngles.z);
                        }
                    }

                    character.Translate(Vector3.forward * speed * Time.deltaTime);
                }

            }
        }

    }
}
