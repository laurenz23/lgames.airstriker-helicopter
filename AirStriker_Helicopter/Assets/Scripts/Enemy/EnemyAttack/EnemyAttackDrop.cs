using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

namespace game_ideas
{
    public class EnemyAttackDrop : MonoBehaviour
    {
        
        [SerializeField] private GameObject explosionEffect = null;

        private Rigidbody RIGIDBODY;
        private EnemyAttackData enemyAttackData;
        private EffectHandler effectHandler;
        private bool facingForward = false;

        private void Start()
        {
            effectHandler = FindObjectOfType<EffectHandler>();
            enemyAttackData = GetComponent<EnemyAttackData>();
            RIGIDBODY = GetComponent<Rigidbody>();

            if (transform.eulerAngles.y.Equals(0f))
            {
                facingForward = true;
            }
        }

        private void Update()
        {
            if (facingForward)
            {
                transform.Translate(Vector3.forward * 5f * Time.deltaTime);
            }

            RIGIDBODY.velocity += Vector3.down * Time.deltaTime;
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
                    enemyAttackData.attackData.attackType.Equals(AttackType.MISSILE) ||
                    enemyAttackData.attackData.attackType.Equals(AttackType.BOMB)
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
                Destroy(transform.gameObject);
            }
        }

        public void DestroyArmament()
        {
            effectHandler.CreatePrefabEffectAndDestroy(explosionEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                new Vector3(0f, transform.position.y, transform.position.z), 3f);

            Destroy(transform.gameObject);
        }

    }
}
