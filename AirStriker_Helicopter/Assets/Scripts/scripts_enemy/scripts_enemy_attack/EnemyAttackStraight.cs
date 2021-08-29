using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached to enemy attack straight projectiles
/// goal: handles the movement attack, attack direction is always at positive z axis
/// </summary>
namespace game_ideas
{
    public class EnemyAttackStraight : MonoBehaviour
    {
        private EffectPrefabManager effectPrefabManager;
        private ArmamentAttackData armamentAttackData;

        private float armamentSpeed;

        private void Start()
        {
            effectPrefabManager = FindObjectOfType<EffectPrefabManager>();
            armamentAttackData = GetComponent<ArmamentAttackData>();
            armamentSpeed = armamentAttackData.GetSpeed();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * armamentSpeed * Time.deltaTime);
        }
       
        private void OnTriggerEnter(Collider other)
        {
            if (
                other.CompareTag(GameTag.Player.ToString()) ||
                other.CompareTag(GameTag.Ground.ToString())
                )
            {
                DestroyArmament();
                return;
            }

            if (other.CompareTag(GameTag.BasicAttack.ToString()))
            {
                if (
                    armamentAttackData.GetAttackType().Equals(AttackType.MISSILE) ||
                    armamentAttackData.GetAttackType().Equals(AttackType.BOMB)
                    )
                {
                    DestroyArmament();
                    return;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameTag.GameBoundary.ToString()))
            {
                // set the armament object active false, to be later use at pooling manager script
                gameObject.SetActive(false); 
            }
        }
        
        public void DestroyArmament()
        {
            effectPrefabManager.PoolExplosion(armamentAttackData.GetExplosionPoolName(), Quaternion.identity,
                new Vector3(0f, transform.position.y, transform.position.z), new Vector3(1f, 1f, 1f));
            
            // set the armament object active false, to be later use at pooling manager script
            gameObject.SetActive(false);
        }

    }
}
