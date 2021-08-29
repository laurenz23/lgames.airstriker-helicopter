using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to armament itself
/// Handling the collision of the armament and explosion including the movement of the armament
/// </summary>

namespace game_ideas
{
    public class StraightAttack : MonoBehaviour
    {
        public GameObject[] armament;

        private Rigidbody RIGIDBODY;
        private ArmamentAttackData armamentAttackData;
        private EffectPrefabManager effectHandler;

        // we cannot destroy the bullet object instantly, wait for trail effect to finish to have a nice effect
        // so we assign the disabled bullet to hide and stop the movement of bullet once it collided
        private bool disabledBullet = false;

        private void Start()
        {
            effectHandler = FindObjectOfType<EffectPrefabManager>();
            RIGIDBODY = GetComponent<Rigidbody>();
            armamentAttackData = GetComponent<ArmamentAttackData>();
        }

        private void Update()
        {

            // once the bullet is disable it will stop to move and hide the bullet while waiting to destroy the object
            if (disabledBullet)
            {
                RIGIDBODY.isKinematic = true;
            }
            else
            {
                transform.position += transform.forward * armamentAttackData.GetSpeed() * Time.deltaTime;
            }

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
                if (effectHandler != null)
                {
                    effectHandler.PoolExplosion(armamentAttackData.GetExplosionPoolName(), Quaternion.identity,
                    new Vector3(0f, transform.position.y, transform.position.z + 1.1f), new Vector3(1f, 1f, 1f));
                }

                // destroy the attack
                DestroyArmament();

            }
        }

        private void OnTriggerExit(Collider other)
        {
            // if armament exist the collider of the game boundary then destroy it
            if (other.CompareTag(GameTag.GameBoundary.ToString()))
            {
                DestroyArmament();
            }
        }

        private void DestroyArmament()
        {
            // we don't need to destory the object, since the object is added to pooling object list
            // instead set active to false to be use later at pooling manager script
            gameObject.SetActive(false);
        }

    }
}
