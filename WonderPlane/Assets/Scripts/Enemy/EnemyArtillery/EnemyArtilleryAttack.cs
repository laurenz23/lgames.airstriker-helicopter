using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to enemy artillery object
/// handles the attack of the artillery that includes the firerate of the artillery
/// </summary>

namespace game_ideas
{
    public class EnemyArtilleryAttack : MonoBehaviour
    {

        public float fireRate;

        [SerializeField] private GameObject attackPrefab = null;
        [SerializeField] private EnemyArtilleryTarget enemyArtilleryTarget = null;
        [SerializeField] private Transform attackOut = null;

        private GameManager gameManager;

        private float attackDelay = 0f;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                if (enemyArtilleryTarget.ArtilleryReadyToFire())
                {
                    attackDelay += Time.deltaTime;

                    if (attackDelay >= fireRate)
                    {
                        ArtilleryAttack();
                        ResetDelay();
                    }
                }
            }
        }

        private void ArtilleryAttack()
        {
            CreateArtilleryAttack();
        }

        private void ResetDelay()
        {
            attackDelay = 0f;
        }

        private void CreateArtilleryAttack()
        {
            GameObject newObj = Instantiate(attackPrefab) as GameObject;
            newObj.transform.rotation = attackOut.rotation;
            newObj.transform.position = attackOut.position;
        }

    }
}
