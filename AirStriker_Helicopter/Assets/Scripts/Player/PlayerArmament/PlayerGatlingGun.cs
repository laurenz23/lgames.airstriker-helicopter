﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player armament object
/// handles the gatling gun attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerGatlingGun : MonoBehaviour
    {
        [SerializeField] private GameObject gatlingAttackPrefab = null;

        private float gatlingFireRate = .3f; // armament fire rate
        private float gatlingAttackDelay = 0f; // reference for attack delay
        private bool alreadyFire = false;

        private void Update()
        {
            // check if player already trigger the attack
            if (alreadyFire)
            {
                gatlingAttackDelay += Time.deltaTime; // incrase the value of gatling attack delay until it matches or greater than to gatling fire rate

                // if gatling attack delay is equal or greater than to gatling fire rate, player will be able to trigger the attack again
                if (gatlingAttackDelay >= gatlingFireRate)
                {
                    ResetDelay(); // reset the value for triggering the attack
                }
            }
        }

        // function is called at Player Attack Handler
        public void GatlingAttack(Transform playerTransform)
        {
            // if attack delay is equals to zero, player can trigger the attack
            if (gatlingAttackDelay == 0)
            {
                CreateGatlingAttack(playerTransform);
                alreadyFire = true; // player will not be able to trigger the attack for while if already fired
            }
        }

        // reset the attack to be able to attack again
        public void ResetDelay()
        {
            gatlingAttackDelay = 0f;
            alreadyFire = false;
        }

        // instantiate attack prefab and assign the transform base on player rotation and position
        private void CreateGatlingAttack(Transform playerTransform)
        {

            GameObject newObj = Instantiate(gatlingAttackPrefab) as GameObject;
            newObj.transform.rotation = playerTransform.rotation;
            newObj.transform.position = playerTransform.position;

        }

    }
}