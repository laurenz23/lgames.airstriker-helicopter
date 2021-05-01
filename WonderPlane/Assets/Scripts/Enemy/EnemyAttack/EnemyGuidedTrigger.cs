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

        [SerializeField] private GameObject enemyHitEffectPrefab = null;
        [SerializeField] private EnemyAttackGuided guidedAttack = null;

        private EffectHandler effectHandler;
        private EnemyAttackData enemyAttackData;

        private void Start()
        {
            effectHandler = FindObjectOfType<EffectHandler>();
            enemyAttackData = guidedAttack.transform.GetComponent<EnemyAttackData>();
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
                    enemyAttackData.armamentAttackData.armamentType.Equals(ArmamentType.MISSILE) ||
                    enemyAttackData.armamentAttackData.armamentType.Equals(ArmamentType.BOMB) ||
                    enemyAttackData.armamentAttackData.armamentType.Equals(ArmamentType.BOMB)
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
                guidedAttack.StopArmament();
            }
        }
        
        public void DestroyArmament()
        {
            GetComponent<BoxCollider>().enabled = false;

            guidedAttack.gameObject.SetActive(false);

            effectHandler.CreatePrefabEffectAndDestroy(enemyHitEffectPrefab, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                new Vector3(0f, guidedAttack.transform.position.y, guidedAttack.transform.position.z), 3f);

            guidedAttack.StopArmament();
        }

    }
}
