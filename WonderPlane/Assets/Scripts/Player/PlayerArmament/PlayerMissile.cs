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

        //private int missileArmamentNumber = 100; // armament number of the missile
        private float missileFireRate = 1f; // fire rate of the missile
        private float missileAttackDelay = 0f;
        private bool leftAttackArmament = false; // start the attack of missile at right armamment
        private bool alreadyFire = false;

        private void Awake()
        {
            playerUIManager = FindObjectOfType<PlayerUIManager>();
        }

        private void Update()
        {
            if (alreadyFire)
            {
                missileAttackDelay += Time.deltaTime;

                if (missileAttackDelay >= missileFireRate)
                {
                    ResetDelay();
                }
                
                // cooldown ui armament 
                playerUIManager.StraightMissile_uiCooldown(missileFireRate, missileAttackDelay);
            }
        }

        public void MissileAttack(Transform playerTransform)
        {

            if (missileAttackDelay == 0f)
            {
                CreateMissileAttack(playerTransform);
                alreadyFire = true;
            }

        }

        public void ResetDelay()
        {
            missileAttackDelay = 0f;
            alreadyFire = false;
        }

        private void CreateMissileAttack(Transform playerTransform)
        {
            GameObject newObj = Instantiate(missileAttackPrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, 0f);
            newObj.transform.position = playerTransform.position;

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
