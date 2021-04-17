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
        
        [SerializeField] private GameObject[] enemyAttackMesh = null;

        private EffectHandler effectHandler;

        private bool stop = false;

        private void Awake()
        {
            effectHandler = FindObjectOfType<EffectHandler>();
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

                foreach (GameObject g in enemyAttackMesh)
                {
                    g.SetActive(false);
                    effectHandler.CreatePrefabEffectAndDestroy(effectHandler.basicBulletHitEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                        new Vector3(0f, g.transform.position.y, g.transform.position.z), 3f);
                }
                
                Destroy(transform.gameObject, 1f);
                stop = true;

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

    }
}
