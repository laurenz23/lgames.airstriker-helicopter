using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this sccirpt is attached to player armament object
/// handles the automic attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerAutomic : MonoBehaviour
    {
        
        [SerializeField] private GameObject automicAttackPrefab = null;

        private PlayerUIManager playerUIManager = null;
        
        private float automicFireRate = 10f; // fire rate of the missile
        private float automicAttackDelay = 0f;
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

                automicAttackDelay -= Time.deltaTime;

                // set the armament values for ui armament cooldown
                playerUIManager.AutomicBomb_uiCooldown(isCooldown, automicFireRate, automicAttackDelay);

                // check if the missile attack delay is equal to zero
                if (automicAttackDelay <= 0f)
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
        public void AutomicAttack(Transform playerTransform)
        {

            // if player trigger the attack, then set the cooldown value to true and create a missile attack prefab
            if (automicAttackDelay.Equals(automicFireRate))
            {
                CreateAutomicAttack(playerTransform);
                isCooldown = true; // set the cooldown value to true so player cant trigger the attack while attack is in cooldown
            }

        }

        // reset the armament value to be able to attack again
        public void ResetDelay()
        {
            automicAttackDelay = automicFireRate;
            isCooldown = false;
        }

        // instantiate a new attack object for attack and set the position and rotation base on player transform 
        private void CreateAutomicAttack(Transform playerTransform)
        {

            GameObject newObj = Instantiate(automicAttackPrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, 0f);
            newObj.transform.position = playerTransform.position;

        }

    }
}
