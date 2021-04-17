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
        
        private float automicFireRate = 10f;
        private float automicAttackDelay = 0f;
        private bool alreadyFire = false;

        private void Awake()
        {
            playerUIManager = FindObjectOfType<PlayerUIManager>();
        }

        private void Update()
        {
            if (alreadyFire)
            {
                automicAttackDelay += Time.deltaTime;

                if (automicAttackDelay >= automicFireRate)
                {
                    ResetDelay();
                }

                // ui cooldown armament
                playerUIManager.AutomicBomb_uiCooldown(automicFireRate, automicAttackDelay);
            }
        }

        public void AutomicAttack(Transform playerTransform)
        {
            if (automicAttackDelay == 0f)
            {
                CreateAutomicAttack(playerTransform);
                alreadyFire = true;
            }

        }

        public void ResetDelay()
        {
            automicAttackDelay = 0;
            alreadyFire = false;
        }

        private void CreateAutomicAttack(Transform playerTransform)
        {

            GameObject newObj = Instantiate(automicAttackPrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, 0f);
            newObj.transform.position = playerTransform.position;

        }

    }
}
