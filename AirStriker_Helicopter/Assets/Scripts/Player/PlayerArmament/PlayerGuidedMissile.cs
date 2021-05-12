using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player armament object
/// handles the missile attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerGuidedMissile : MonoBehaviour
    {
        [SerializeField] private GameObject guidedMissilePrefab = null;

        private PlayerUIManager playerUIManager;

        private float guidedMissileFireRate = 2f;
        private float guidedMissileAttackDelay = 0f;
        private bool leftAttackArmament = false; // start the attack of missile at right armamment
        private bool isCooldown = false;

        private void Awake()
        {
            playerUIManager = FindObjectOfType<PlayerUIManager>();
        }

        public void Update()
        {
            // if armament is cooldown, then do not set value for triggering the attacks
            if (isCooldown)
            {
                guidedMissileAttackDelay -= Time.deltaTime;

                // set the armament values for ui armament cooldown
                playerUIManager.GuidedMissile_uiCooldown(isCooldown, guidedMissileFireRate, guidedMissileAttackDelay);

                // check if the missile attack delay is equal to zero
                if (guidedMissileAttackDelay <= 0f)
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
        public void GuidedMissileAttack(Transform playerTransform)
        {

            // if player trigger the attack, then set the cooldown value to true and create a missile attack prefab
            if (guidedMissileAttackDelay.Equals(guidedMissileFireRate))
            {
                CreateGuidedMissileAttack(playerTransform);
                isCooldown = true; // set the cooldown value to true so player cant trigger the attack while attack is in cooldown
            }

        }

        // reset the armament value to be able to attack again
        public void ResetDelay()
        {
            guidedMissileAttackDelay = guidedMissileFireRate;
            isCooldown = false;
        }

        // instantiate a new attack object for attack and set the position and rotation base on player transform
        private void CreateGuidedMissileAttack(Transform playerTransform)
        {

            GameObject newObj = Instantiate(guidedMissilePrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, 0f);
            newObj.transform.position = playerTransform.position;

            // apply alternate armament attacks from left armament or right armament
            if (leftAttackArmament)
            {
                newObj.GetComponentInChildren<GuidedAttack>().armament[0].SetActive(true);
                newObj.GetComponentInChildren<GuidedAttack>().armament[1].SetActive(false);
                leftAttackArmament = false;
            }
            else
            {
                newObj.GetComponentInChildren<GuidedAttack>().armament[0].SetActive(false);
                newObj.GetComponentInChildren<GuidedAttack>().armament[1].SetActive(true);
                leftAttackArmament = true;
            }
        }

    }
}
