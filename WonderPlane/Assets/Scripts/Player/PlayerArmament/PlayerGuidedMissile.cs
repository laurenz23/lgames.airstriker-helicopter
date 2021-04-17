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
        private bool alreadyFire = false;

        private void Awake()
        {
            playerUIManager = FindObjectOfType<PlayerUIManager>();
        }

        public void Update()
        {
            if (alreadyFire)
            {
                guidedMissileAttackDelay += Time.deltaTime;

                if (guidedMissileAttackDelay >= guidedMissileFireRate)
                {
                    ResetDelay();
                }

                playerUIManager.GuidedMissile_uiCooldown(guidedMissileFireRate, guidedMissileAttackDelay);
            }
        }

        public void GuidedMissileAttack(Transform playerTransform)
        {
            if (guidedMissileAttackDelay == 0f)
            {
                CreateGuidedMissileAttack(playerTransform);
                alreadyFire = true;
            }

        }

        public void ResetDelay()
        {
            guidedMissileAttackDelay = 0f;
            alreadyFire = false;
        }

        private void CreateGuidedMissileAttack(Transform playerTransform)
        {

            GameObject newObj = Instantiate(guidedMissilePrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, 0f);
            newObj.transform.position = playerTransform.position;

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
