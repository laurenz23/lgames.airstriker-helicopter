using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to character object as a child
/// it will attack automatically even no target is detected
/// unlike to enemyArmamentAttackTarget that will attack if target is in range
/// </summary>

namespace game_ideas
{
    public class EnemyArmamentAutoAttack : MonoBehaviour
    {
        public EnemyHandler enemyHandler;

        [SerializeField] private bool implementEachAttackDelay;
        [SerializeField] private float eachAttackDelayRate;

        private GameManager gameManager;

        private float getFireRate;
        private float attackDelay = 0f;
        private int eachAttackDelayNum = -1;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            getFireRate = enemyHandler.enemyData.firerate;
        }

        private void Update()
        {
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                attackDelay += Time.deltaTime;

                if (attackDelay >= enemyHandler.enemyData.firerate)
                {
                    if (implementEachAttackDelay)
                    {
                        eachAttackDelayNum = eachAttackDelayNum + 1;
                        enemyHandler.enemyData.firerate += eachAttackDelayRate;
                    }

                    // create attack prefab
                    Attack(eachAttackDelayNum);
                }
            }
        }

        private void Attack(int index = 0)
        {
            if (implementEachAttackDelay)
            {
                if (eachAttackDelayNum >= enemyHandler.attackOut.Length)
                {
                    eachAttackDelayNum = -1;
                    enemyHandler.enemyData.firerate = getFireRate;
                    ResetDelay();
                }
                else
                {
                    CreateAttack(enemyHandler.attackOut[index]);
                }
            }
            else
            {
                foreach (Transform t in enemyHandler.attackOut)
                {
                    CreateAttack(t);
                }

                ResetDelay();
            }
        }

        private void ResetDelay()
        {
            attackDelay = 0f;
        }

        /*
         * rotation and position of attack armament is base on attack out that is attached to gun object prefab 
         * NOTE:    make sure the attack object have attached enemy attack data from it's main object or from children
         */
        private void CreateAttack(Transform attackT)
        {
            GameObject attackObject = Instantiate(enemyHandler.enemyData.attackPrefab) as GameObject;
            attackObject.transform.eulerAngles = new Vector3(attackT.eulerAngles.x, attackT.eulerAngles.y, 0f);
            attackObject.transform.position = attackT.position;

            // get the attack data of character attack
            // check if attack data is attached to main attack object
            if (attackObject.GetComponent<EnemyAttackData>())
            {
                attackObject.GetComponent<EnemyAttackData>().attackData = enemyHandler.attackData;
            }
            else // if attack data is attached to it's children object
            {
                attackObject.GetComponentInChildren<EnemyAttackData>().attackData = enemyHandler.attackData;
            }
        }

    }
}
