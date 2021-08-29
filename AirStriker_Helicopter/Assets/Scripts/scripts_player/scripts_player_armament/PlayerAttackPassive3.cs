using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player armament object
/// handles the missile attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerAttackPassive3 : MonoBehaviour
    {
        [SerializeField] private AttackType attackType;
        [SerializeField] private float attackPassive3Firerate = 2f;
        [SerializeField] private string objectPoolName; 
        [SerializeField] private string soundFXName;
        [SerializeField] private PlayerManager playerManager = null;
        [SerializeField] private Transform[] attackPoint;

        private PlayerUIManager playerUIManager;
        private PoolingManager poolingManager;
        
        private float attackDelay = 0f;
        private bool leftAttackArmament = false; // start the attack of missile at right armamment
        private bool isCooldown = false;
        
        private void Start()
        {
            playerUIManager = FindObjectOfType<PlayerUIManager>();
            poolingManager = FindObjectOfType<PoolingManager>();
        }

        public void Update()
        {
            // if armament is cooldown, then do not set value for triggering the attacks
            if (isCooldown)
            {
                attackDelay -= Time.deltaTime;

                // set the armament values for ui armament cooldown
                playerUIManager.PassiveSkill3_uiCooldown(isCooldown, attackPassive3Firerate, attackDelay);

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
            if (attackDelay.Equals(attackPassive3Firerate))
            {
                CreateGuidedMissileAttack(playerTransform);
                isCooldown = true; // set the cooldown value to true so player cant trigger the attack while attack is in cooldown
            }

        }

        // reset the armament value to be able to attack again
        public void ResetDelay()
        {
            attackDelay = attackPassive3Firerate;
            isCooldown = false;
        }

        // instantiate a new attack object for attack and set the position and rotation base on player transform
        private void CreateGuidedMissileAttack(Transform playerTransform)
        {
            if (attackType == AttackType.MISSILE)
            {
                playerManager.soundFXHandler.SFX_SHOOT_MISSILE(soundFXName);
            }
            
            // get object pool by finding pool name and display the object
            GameObject poolObj = poolingManager.GetPooledObjectsPlayerArm(objectPoolName);
            poolObj.SetActive(true);
            
            // apply alternate armament attacks from left armament or right armament
            if (leftAttackArmament)
            {
                poolObj.transform.rotation = Quaternion.Euler(attackPoint[0].eulerAngles.x, poolObj.transform.eulerAngles.y, poolObj.transform.eulerAngles.z);
                poolObj.transform.position = attackPoint[0].position;
                leftAttackArmament = false;
            }
            else
            {
                poolObj.transform.rotation = Quaternion.Euler(attackPoint[1].eulerAngles.x, poolObj.transform.eulerAngles.y, poolObj.transform.eulerAngles.z);
                poolObj.transform.position = attackPoint[1].position;
                leftAttackArmament = true;
            }
        }

    }
}
