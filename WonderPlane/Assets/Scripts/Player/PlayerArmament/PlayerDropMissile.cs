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

        private float dropMissileFireRate = 1f;
        private float dropMissileAttackDelay = 0f;
        private bool alreadyFire = false;

        private void Awake()
        {
            playerUIManager = FindObjectOfType<PlayerUIManager>();
        }

        private void Update()
        {
            if (alreadyFire)
            {
                dropMissileAttackDelay += Time.deltaTime;

                if (dropMissileAttackDelay >= dropMissileFireRate)
                {
                    ResetDelay();
                }

                playerUIManager.DropBomb_uiCooldown(dropMissileFireRate, dropMissileAttackDelay);
            }
        }

        public void DropMissileAttack(Transform playerTransform)
        {

            if (dropMissileAttackDelay == 0f)
            {
                CreateDropMissileAttack(playerTransform);
                alreadyFire = true;
            }

        }

        public void ResetDelay()
        {
            dropMissileAttackDelay = 0f;
            alreadyFire = false;
        }

        private void CreateDropMissileAttack(Transform playerTransform)
        {

            GameObject newObj = Instantiate(dropMissileAttackPrefab) as GameObject;
            newObj.transform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, 0f);
            newObj.transform.position = playerTransform.position;

            if (playerManager.moveDescending)
            {
                newObj.GetComponent<DropAttack>().playerDescending = true;
            }
            
        }

    }
}
