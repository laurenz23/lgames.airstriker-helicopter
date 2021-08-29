using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to armament itself
/// handling the collision of the armament and explosion including the movemement of the armament
/// use this script to armament drop attacks only
/// </summary>

namespace game_ideas
{
    public class DropAttack : MonoBehaviour
    {
        //[SerializeField] private GameObject armament = null;
        public bool playerDescending; // if player is descending for nice armament drop effect
        
        private Rigidbody RIGIDBODY;
        private ArmamentAttackData armamentAttackData;
        private EffectPrefabManager effectPrefabManager;

        // we cannot destroy the bullet object instantly, wait for trail effect to finish to have a nice effect
        // so we assign the disabled bullet to hide and stop the movement of bullet once it collided
        private bool disabledBullet = false;

        private void Awake()
        {
            RIGIDBODY = GetComponent<Rigidbody>();
        }

        private void OnDisable()
        {
            RIGIDBODY.velocity = Vector3.zero;
            playerDescending = false;
        }

        private void Start()
        {
            effectPrefabManager = FindObjectOfType<EffectPrefabManager>();
            armamentAttackData = GetComponent<ArmamentAttackData>();
        }

        private void Update()
        {

            if (disabledBullet)
            {
                RIGIDBODY.isKinematic = true;
            }
            else
            {
                // when player is descending increase the down velocity of the armament
                if (playerDescending)
                {
                    RIGIDBODY.velocity += Vector3.down * 30f * Time.deltaTime;
                }
                
                transform.position += transform.forward * 2f * Time.deltaTime;
                
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

                // assign array inside area affect
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, armamentAttackData.GetAoE());

                foreach (Collider ca in colliderArray)
                {
                    // check the object inside area affect if an enemy
                    if (ca.GetComponent<EnemyHandler>())
                    {
                        ca.GetComponent<EnemyColliderHandler>().HitByArmament(armamentAttackData);
                    }
                    else if (ca.GetComponent<EnemyAttackStraight>())
                    {
                        ca.GetComponent<EnemyAttackStraight>().DestroyArmament();
                    }
                    else if (ca.GetComponent<EnemyAttackGuided>())
                    {
                        ca.GetComponent<EnemyAttackGuided>().DestroyArmament();
                    }
                    else if (ca.GetComponent<EnemyAttackDrop>())
                    {
                        ca.GetComponent<EnemyAttackDrop>().DestroyArmament();
                    }
                }

                // create explosion effect for the armament
                if (effectPrefabManager != null)
                {
                    effectPrefabManager.PoolExplosion(armamentAttackData.GetExplosionPoolName(), Quaternion.identity,
                    new Vector3(0f, transform.position.y, transform.position.z), new Vector3(2f, 2f, 2f));
                }

                // destroy the attack
                DestroyArmament();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // if armament exist the collider of the game boundary then destroy it
            if (other.transform.CompareTag(GameTag.GameBoundary.ToString()))
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
