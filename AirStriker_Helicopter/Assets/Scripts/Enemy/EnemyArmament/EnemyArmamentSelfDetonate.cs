using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// usage: attached this script to character as a child.
/// attached only to enemy that have self detonation ability
/// goal: find player as a target and move to targeted player at set distance the character
/// where this script is attached to going to explode
/// </summary>

namespace game_ideas
{
    public class EnemyArmamentSelfDetonate : MonoBehaviour
    {
        [Range(1, 7)]
        public float detonationTime;

        public float detonationRange;

        // public float damageRange;

        [Tooltip("Apply Z axis distance for explosion")]
        public bool zAxis;

        public TextMeshPro timer_text;

        public EnemyHandler enemyHandler;


        private Vector3 targetTransform;

        private Transform playerTransform;

        private Transform characterTransform;

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameManager.GetInstance();
        }

        private void Start()
        {
            playerTransform = FindObjectOfType<PlayerManager>().playerTransform;

            characterTransform = enemyHandler.transform;
        }

        private void Update()
        {
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                detonationTime -= 1f * Time.deltaTime;

                timer_text.text = Mathf.RoundToInt(detonationTime).ToString();

                if (Mathf.RoundToInt(detonationTime) <= 0f)
                {
                    // destoy the character and disabled the script
                    enemyHandler.DestroyCharacter();

                    enabled = false;
                }

                
                targetTransform = characterTransform.position - playerTransform.position;

                if (targetTransform.magnitude <= detonationRange)
                {

                    // destroy the character
                    enemyHandler.DestroyCharacter();

                    // disabled the script
                    enabled = false;

                }

                if (zAxis)
                {
                    if (playerTransform.position.z > characterTransform.position.z)
                    {
                        // destroy the character
                        enemyHandler.DestroyCharacter();

                        // disabled the script
                        enabled = false;
                    }
                }
                
            }
        }

    }
}
