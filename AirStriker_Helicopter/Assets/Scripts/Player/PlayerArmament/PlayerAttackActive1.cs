using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this sccirpt is attached to player armament object
/// handles the automic attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerAttackActive1 : MonoBehaviour
    {
        [SerializeField] private AttackType attackType;
        [SerializeField] private float activeAttack1Firerate = 10f; // fire rate of the missile
        [SerializeField] private string soundFXName;
        [SerializeField] private AttackData attackData;
        [SerializeField] private PlayerManager playerManager = null;

        private PlayerUIManager playerUIManager = null;
        
        private float attackDelay = 0f;
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

                attackDelay -= Time.deltaTime;

                // set the armament values for ui armament cooldown
                playerUIManager.ActiveSkill1_uiCooldown(isCooldown, activeAttack1Firerate, attackDelay);

                // check if the missile attack delay is equal to zero
                if (attackDelay <= 0f)
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
        public void AttackAction(Transform playerTransform)
        {
            
            // if player trigger the attack, then set the cooldown value to true and create a missile attack prefab
            if (attackDelay.Equals(activeAttack1Firerate))
            {
                CreateAutomicAttack(playerTransform);
                isCooldown = true; // set the cooldown value to true so player cant trigger the attack while attack is in cooldown
            }

        }

        // reset the armament value to be able to attack again
        public void ResetDelay()
        {
            attackDelay = activeAttack1Firerate;
            isCooldown = false;
        }

        // instantiate a new attack object for attack and set the position and rotation base on player transform 
        private void CreateAutomicAttack(Transform playerTransform)
        {
            if (attackType == AttackType.ATOMIC)
            {
                playerManager.soundFXHandler.SFX_BOMB_DROP(soundFXName);
            }

            GameObject newObj = Instantiate(attackData.attackPrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, 0f);
            newObj.transform.position = playerTransform.position;

        }

    }
}
