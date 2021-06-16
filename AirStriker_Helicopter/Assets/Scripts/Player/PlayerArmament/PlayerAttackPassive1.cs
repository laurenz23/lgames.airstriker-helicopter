using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player armament object
/// handles the missile attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerAttackPassive1 : MonoBehaviour
    {
        [SerializeField] private AttackType attackType;
        [SerializeField] private float attackPassive1Firerate = 1f; // fire rate of the missile
        [SerializeField] private string soundFXName;
        [SerializeField] private AttackData attackData;
        [SerializeField] private PlayerManager playerManager = null;

        private PlayerUIManager playerUIManager = null;
        
        private float attackDelay = 0f;
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
                attackDelay -= Time.deltaTime;

                // set the armament values for ui armament cooldown
                playerUIManager.PassiveSkill1_uiCooldown(isCooldown, attackPassive1Firerate, attackDelay);

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
            if (attackDelay.Equals(attackPassive1Firerate))
            {
                CreateMissileAttack(playerTransform);
                isCooldown = true; // set the cooldown value to true so player cant trigger the attack while attack is in cooldown
            }

        }

        // reset the armament value to be able to attack again
        public void ResetDelay()
        {
            attackDelay = attackPassive1Firerate;
            isCooldown = false;
        }

        // instantiate a new attack object for attack and set the position and rotation base on player transform
        private void CreateMissileAttack(Transform playerTransform)
        {
            if (attackType == AttackType.MISSILE)
            {
                playerManager.soundFXHandler.SFX_SHOOT_MISSILE(soundFXName);
            }

            GameObject newObj = Instantiate(attackData.attackPrefab) as GameObject;
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
