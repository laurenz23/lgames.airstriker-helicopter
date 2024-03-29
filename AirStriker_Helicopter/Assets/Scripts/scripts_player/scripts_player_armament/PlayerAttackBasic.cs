﻿using System.Collections;
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
        [SerializeField] private float attackBasicFirerate; // armament fire rate
        [SerializeField] private string objectPoolName;
        [SerializeField] private string soundFXName;
        [SerializeField] private PlayerManager playerManager = null;
        [SerializeField] private Transform[] attackPoint;

        private PoolingManager poolingManager;

        private float attackDelay = 0f; // reference for attack delay
        private bool alreadyFire = false;

        private void Start()
        {
            poolingManager = FindObjectOfType<PoolingManager>();
        }

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

            foreach (Transform ap in attackPoint)
            {
                // find and display the object by pool name
                GameObject poolObj = poolingManager.GetPooledObjectsPlayerArm(objectPoolName);
                poolObj.transform.eulerAngles = new Vector3(ap.eulerAngles.x, poolObj.transform.eulerAngles.y, poolObj.transform.eulerAngles.z);
                poolObj.transform.position = ap.position;
                poolObj.SetActive(true);

                ArmamentAttackData armamentAttackData = poolObj.GetComponent<ArmamentAttackData>();

                // if theres a muzzleFlash object attached
                // create a muzzleFlash effect
                if (armamentAttackData.HasMuzzleFlash())
                {
                    GameObject muzPoolObj = poolingManager.GetPooledObjectMuzzleFlash(armamentAttackData.GetMuzzleFlashPoolName());
                    muzPoolObj.GetComponent<MuzzleFlash>().attackPointTransform = ap;
                    muzPoolObj.transform.position = ap.position;
                    muzPoolObj.transform.eulerAngles = new Vector3(ap.eulerAngles.x, 0f, 0f);
                    muzPoolObj.SetActive(true);
                }
            }
        }

    }
}
