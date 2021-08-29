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
        [SerializeField] private float attackPassive1Firerate; // fire rate of the missile
        [SerializeField] private string objectPoolName;
        [SerializeField] private string soundFXName;
        [SerializeField] private PlayerManager playerManager = null;
        [SerializeField] private Transform[] attackPoint;

        private PlayerUIManager playerUIManager = null;
        private PoolingManager poolingManager = null;
        
        private float attackDelay = 0f;
        private bool isCooldown = false;
        private Vector3[] posAttackPoint;
        
        private void Start()
        {
            playerUIManager = FindObjectOfType<PlayerUIManager>();
            poolingManager = FindObjectOfType<PoolingManager>();

            posAttackPoint = new Vector3[attackPoint.Length];

            for (int x = 0; x < attackPoint.Length; x++)
            {
                posAttackPoint[x] = attackPoint[x].localPosition;
            }
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

            for (int x = 0; x < attackPoint.Length; x++)
            {
                // avoid armament crossing path when player moving ascending
                // by switching position of ap1 and ap2
                if (playerManager.moveAscending && !playerManager.moveBackward && !playerManager.moveForward)
                {
                    attackPoint[x].localPosition = posAttackPoint[1 - x]; // switch positin of attackPoints
                }
                else
                {
                    attackPoint[x].localPosition = posAttackPoint[x]; // reset position of attackPoints
                }

                // find and display object by pool name at pooling manager script
                GameObject poolObj = poolingManager.GetPooledObjectsPlayerArm(objectPoolName);
                poolObj.transform.eulerAngles = new Vector3(attackPoint[x].eulerAngles.x, poolObj.transform.eulerAngles.y, poolObj.transform.eulerAngles.z);
                poolObj.transform.position = attackPoint[x].position;
                poolObj.SetActive(true);
            }
        }

    }
}
