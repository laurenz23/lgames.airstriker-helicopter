using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to guided attack explosion trigger object, a child of main guided attack object
/// the object that attached with this script must have a trigger
/// it will trigger the explosion effect once it is collided to object that have given tags
/// </summary>

namespace game_ideas
{
    public class GuidedAttackExplosionTrigger : MonoBehaviour
    {
        [SerializeField] private GuidedAttack guidedMissile = null;

        private EffectPrefabManager effectPrefabManager;
        private ArmamentAttackData armamentAttackData;

        private void Start()
        {
            effectPrefabManager = FindObjectOfType<EffectPrefabManager>();
            armamentAttackData = GetComponent<ArmamentAttackData>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (
                other.CompareTag(GameTag.Ground.ToString()) ||
                other.CompareTag(GameTag.Terrain.ToString()) ||
                other.CompareTag(GameTag.Enemy.ToString())
                )
            {

                // create explosion effect
                if (!effectPrefabManager.Equals(null))
                {
                    effectPrefabManager.PoolExplosion(armamentAttackData.GetExplosionPoolName(), Quaternion.identity,
                        new Vector3(0f, transform.position.y, transform.position.z), new Vector3(1f, 1f, 1f));
                }

                // disabled the collider of the attack while waiting to destroy it to avoid enemy character exploding
                guidedMissile.DestroyArmament();

            }
        }

        private void OnTriggerExit(Collider other)
        {
            // if armament exist the collider of the game boundary then destroy it
            if (other.CompareTag(GameTag.GameBoundary.ToString()))
            {
                guidedMissile.DestroyArmament();
            }
        }
    }
}
