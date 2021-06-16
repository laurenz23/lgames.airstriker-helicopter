using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player armament object
/// handles the gatling gun attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerAttackBasic : MonoBehaviour
    {
        [SerializeField] private AttackType attackType;
        [SerializeField] private float attackBasicFirerate = .3f; // armament fire rate
        [SerializeField] private string soundFXName;
        [SerializeField] private AttackData attackData;
        [SerializeField] private PlayerManager playerManager = null;
        
        private float attackDelay = 0f; // reference for attack delay
        private bool alreadyFire = false;

        private void Update()
        {
            // check if player already trigger the attack
            if (alreadyFire)
            {
                attackDelay += Time.deltaTime; // incrase the value of gatling attack delay until it matches or greater than to gatling fire rate

                // if gatling attack delay is equal or greater than to gatling fire rate, player will be able to trigger the attack again
                if (attackDelay >= attackBasicFirerate)
                {
                    ResetDelay(); // reset the value for triggering the attack
                }
            }
        }

        // function is called at Player Attack Handler
        public void AttackAction(Transform playerTransform)
        {
            // if attack delay is equals to zero, player can trigger the attack
            if (attackDelay == 0)
            {
                CreateGatlingAttack(playerTransform);
                alreadyFire = true; // player will not be able to trigger the attack for while if already fired
            }
        }

        // reset the attack to be able to attack again
        public void ResetDelay()
        {
            attackDelay = 0f;
            alreadyFire = false;
        }

        // instantiate attack prefab and assign the transform base on player rotation and position
        private void CreateGatlingAttack(Transform playerTransform)
        {
            if (attackType == AttackType.BULLET)
            {
                playerManager.soundFXHandler.SFX_SHOOT(soundFXName);
            }

            GameObject newObj = Instantiate(attackData.attackPrefab) as GameObject;
            newObj.transform.rotation = playerTransform.rotation;
            newObj.transform.position = playerTransform.position;

        }

    }
}
