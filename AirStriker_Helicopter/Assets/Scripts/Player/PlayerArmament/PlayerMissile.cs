using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player armament object
/// handles the missile attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerMissile : MonoBehaviour
    {

        [SerializeField] private GameObject missileAttackPrefab = null;

        private PlayerUIManager playerUIManager = null;
        
        private float missileFireRate = 1f; // fire rate of the missile
        private float missileAttackDelay = 0f;
        private bool leftAttackArmament = false; // start the attack of missile at right armamment
        private bool isCooldown = false; 

        private void Awake()
        {
            playerUIManager = FindObjectOfType<PlayerUIManager>();
        }

        private void Update()
        {

            // if armament is cooldown, then do not set value for triggering the attacks
            if (isCooldown)
            {
                missileAttackDelay -= Time.deltaTime;

                // set the armament values for ui armament cooldown
                playerUIManager.StraightMissile_uiCooldown(isCooldown, missileFireRate, missileAttackDelay);

                // check if the missile attack delay is equal to zero
                if (missileAttackDelay <= 0f)
                {
                    // then reset the armament value to be able to attack again
                    ResetDelay();
                }
            }
            else // if cooldown is false then reset the value
            {
                ResetDelay();
            }

        }

        // attack trigger called at player attack handler
        public void MissileAttack(Transform playerTransform)
        {

            // if player trigger the attack, then set the cooldown value to true and create a missile attack prefab
            if (missileAttackDelay.Equals(missileFireRate))
            {
                CreateMissileAttack(playerTransform);
                isCooldown = true; // set the cooldown value to true so player cant trigger the attack while attack is in cooldown
            }

        }

        // reset the armament value to be able to attack again
        public void ResetDelay()
        {
            missileAttackDelay = missileFireRate;
            isCooldown = false;
        }

        // instantiate a new attack object for attack and set the position and rotation base on player transform
        private void CreateMissileAttack(Transform playerTransform)
        {
            GameObject newObj = Instantiate(missileAttackPrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, 0f);
            newObj.transform.position = playerTransform.position;

            // apply alternate armament attacks from left armament or right armament
            if (leftAttackArmament)
            {
                newObj.GetComponent<StraightAttack>().armament[0].SetActive(true);
                newObj.GetComponent<StraightAttack>().armament[1].SetActive(false);

                leftAttackArmament = false;
            }
            else
            {
                newObj.GetComponent<StraightAttack>().armament[0].SetActive(false);
                newObj.GetComponent<StraightAttack>().armament[1].SetActive(true);
                
                leftAttackArmament = true;
            } 
        }

    }
}
