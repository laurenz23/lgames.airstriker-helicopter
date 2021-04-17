using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player armament object
/// handles the gatling gun attack of the player
/// </summary>

namespace game_ideas
{
    public class PlayerGatlingGun : MonoBehaviour
    {
        [SerializeField] private GameObject gatlingAttackPrefab = null;

        private float gatlingFireRate = .3f;
        private float gatlingAttackDelay = 0f;
        private bool alreadyFire = false;

        private void Update()
        {
            if (alreadyFire)
            {
                gatlingAttackDelay += Time.deltaTime;

                if (gatlingAttackDelay >= gatlingFireRate)
                {
                    ResetDelay();
                }
            }
        }

        public void GatlingAttack(Transform playerTransform)
        {
            if (gatlingAttackDelay == 0)
            {
                CreateGatlingAttack(playerTransform);
                alreadyFire = true;
            }
        }

        public void ResetDelay()
        {
            gatlingAttackDelay = 0f;
            alreadyFire = false;
        }

        private void CreateGatlingAttack(Transform playerTransform)
        {

            GameObject newObj = Instantiate(gatlingAttackPrefab) as GameObject;
            newObj.transform.rotation = playerTransform.rotation;
            newObj.transform.position = playerTransform.position;

        }

    }
}
