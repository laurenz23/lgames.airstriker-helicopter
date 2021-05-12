using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to enemy attack bullets
/// handles the movement forward attack direction
/// </summary>
namespace game_ideas
{
    public class EnemyAttackStraight : MonoBehaviour
    {

        public float armamentSpeed;

        [SerializeField] private GameObject enemyHitEffectPrefab = null;
        [SerializeField] private GameObject enemyAttackMesh = null;

        private EffectHandler effectHandler;
        private EnemyAttackData enemyAttackData;

        private bool stop = false;

        private void Start()
        {
            effectHandler = FindObjectOfType<EffectHandler>();
            enemyAttackData = GetComponent<EnemyAttackData>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!stop)
            {
                transform.Translate(Vector3.forward * armamentSpeed * Time.deltaTime);
            }
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
                stop = true;
            }
        }
        
        public void DestroyArmament()
        {
            stop = true;
            GetComponent<BoxCollider>().enabled = false;

            enemyAttackMesh.SetActive(false);
            effectHandler.CreatePrefabEffectAndDestroy(enemyHitEffectPrefab, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                new Vector3(0f, transform.position.y, transform.position.z), 3f);

            Destroy(transform.gameObject, 1f);
        }

    }
}
