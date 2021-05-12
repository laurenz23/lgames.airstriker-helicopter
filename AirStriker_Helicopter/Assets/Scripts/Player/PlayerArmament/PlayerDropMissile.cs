using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 /// <summary>
 /// this script is attached to player armament object
 /// handles the drop missile attack of the player
 /// </summary>
 
namespace game_ideas
{
    public class PlayerDropMissile : MonoBehaviour
    {

        [SerializeField] private GameObject dropMissileAttackPrefab = null;
        [SerializeField] private PlayerManager playerManager = null;

        private PlayerUIManager playerUIManager;

        private float dropMissileFireRate = 1f; // fire rate of the missile
        private float dropMissileAttackDelay = 0f;
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

                dropMissileAttackDelay -= Time.deltaTime;

                // set the armament values for ui armament cooldown
                playerUIManager.DropBomb_uiCooldown(isCooldown, dropMissileFireRate, dropMissileAttackDelay);

                // check if the missile attack delay is equal to zero
                if (dropMissileAttackDelay <= 0f)
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
        public void DropMissileAttack(Transform playerTransform)
        {

            // if player trigger the attack, then set the cooldown value to true and create a missile attack prefab
            if (dropMissileAttackDelay.Equals(dropMissileFireRate))
            {
                CreateDropMissileAttack(playerTransform);
                isCooldown = true; // set the cooldown value to true so player cant trigger the attack while attack is in cooldown
            }

        }

        // reset the armament value to be able to attack again
        public void ResetDelay()
        {
            dropMissileAttackDelay = dropMissileFireRate;
            isCooldown = false;
        }

        // instantiate a new attack object for attack and set the position and rotation base on player transform
        private void CreateDropMissileAttack(Transform playerTransform)
        {

            GameObject newObj = Instantiate(dropMissileAttackPrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, 0f);
            newObj.transform.position = playerTransform.position;

            // check if player is descending to increase drop velocity
            if (playerManager.moveDescending)
            {
                newObj.GetComponent<DropAttack>().playerDescending = true; // set the player descending to increase drop velocity
            }
            
        }

    }
}
