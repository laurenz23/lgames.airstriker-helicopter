using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class EnemyColliderHandler : MonoBehaviour
    {

        public EnemyManager enemyManager;

        private void Awake()
        {

            enemyManager = GetComponent<EnemyManager>();

        }

        private void OnTriggerEnter(Collider collider)
        {

            if (collider.CompareTag(GameTag.Player.ToString()) ||
                collider.CompareTag(GameTag.Ground.ToString()) ||
                collider.CompareTag(GameTag.Terrain.ToString()))
            {
                
                    enemyManager.DestroyCharacter();

            }

        }

    }
}
