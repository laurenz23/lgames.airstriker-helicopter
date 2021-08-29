using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached this script to armament itself as a parent
/// goal: a script that handles a attack to drop for enemies
/// </summary>

namespace game_ideas
{
    public class EnemyAttackDrop : MonoBehaviour
    {

        private Rigidbody RIGIDBODY;
        private ArmamentAttackData armamentAttackData;
        private EffectPrefabManager effectPrefabManager;

        private float armamentSpeed;
        private bool facingForward = false;

        private void Start()
        {
            effectPrefabManager = FindObjectOfType<EffectPrefabManager>();
            armamentAttackData = GetComponent<ArmamentAttackData>();
            RIGIDBODY = GetComponent<Rigidbody>();

            if (transform.eulerAngles.y.Equals(0f))
            {
                facingForward = true;
            }

            armamentSpeed = armamentAttackData.GetSpeed();
        }

        private void Update()
        {
            if (facingForward)
            {
                transform.Translate(Vector3.forward * 5f * Time.deltaTime);
            }

            RIGIDBODY.velocity = Vector3.down * armamentSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {

            if (
                other.CompareTag(GameTag.Player.ToString()) ||
                other.CompareTag(GameTag.Ground.ToString()) ||
                other.CompareTag(GameTag.Terrain.ToString())
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
                new Vector3(0f, transform.position.y, transform.position.z), new Vector3(2f, 2f, 2f));

            // reset armament transform and velocity
            RIGIDBODY.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;

            // set the armament object active false, to be later use at pooling manager script
            gameObject.SetActive(false);
        }

    }
}
