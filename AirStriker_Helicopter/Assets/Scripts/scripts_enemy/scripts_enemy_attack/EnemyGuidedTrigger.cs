using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to guided attack trigger object, a child of main guided attack armament
/// that object that attached with this script mush have a trigger
/// it will trigger that explosion effect once it is collided to object that have given tags
/// </summary>

namespace game_ideas
{
    public class EnemyGuidedTrigger : MonoBehaviour
    {
        
        [SerializeField] private EnemyAttackGuided guidedAttack = null;

        private EffectPrefabManager effectPrefabManager;
        private ArmamentAttackData armamentAttackData;

        private void Start()
        {
            effectPrefabManager = FindObjectOfType<EffectPrefabManager>();
            armamentAttackData = guidedAttack.transform.GetComponent<ArmamentAttackData>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (
                other.CompareTag(GameTag.Player.ToString()) ||
                other.CompareTag(GameTag.Terrain.ToString()) ||
                other.CompareTag(GameTag.BasicAttack.ToString())
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
            // if armament exist the collider of the game boundary then destroy it
            if (other.CompareTag(GameTag.GameBoundary.ToString()))
            {
                guidedAttack.DestroyArmament();
            }
        }
        
        public void DestroyArmament()
        {
            guidedAttack.gameObject.SetActive(false);

            effectPrefabManager.PoolExplosion(armamentAttackData.GetExplosionPoolName(), Quaternion.identity,
                new Vector3(0f, guidedAttack.transform.position.y, guidedAttack.transform.position.z), new Vector3(1f, 1f, 1f));

            guidedAttack.DestroyArmament();
        }

    }
}
