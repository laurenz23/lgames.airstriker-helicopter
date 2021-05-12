using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// this script is attached to enemy artillery object
/// will attack the selected target of enemyArmamentFindTarget
/// NOTE:   also attached EnemyArmamentFindTarget where this script is attached
/// </summary>

namespace game_ideas
{

    public class EnemyArmamentAttackTarget : MonoBehaviour
    {
        public EnemyHandler enemyHandler;
        
        [SerializeField] private bool implementEachAttackDelay;
        [SerializeField] private float eachAttackDelayRate;

        private EnemyArmamentFindTarget enemyFindTarget;
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
            enemyFindTarget = GetComponent<EnemyArmamentFindTarget>();
            getFireRate = enemyHandler.enemyData.firerate;
        }

        private void Update()
        {
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                if (enemyFindTarget.ArtilleryReadyToFire())
                {
                    attackDelay += Time.deltaTime;

                    if (attackDelay >= enemyHandler.enemyData.firerate)
                    {
                        if (implementEachAttackDelay)
                        {
                            eachAttackDelayNum = eachAttackDelayNum + 1;
                            enemyHandler.enemyData.firerate += eachAttackDelayRate;
                        }

                        ArtilleryAttack(eachAttackDelayNum);
                    }
                }
            }
        }

        private void ArtilleryAttack(int index = 0)
        {

            if (implementEachAttackDelay) // if implemented attack delay, turret will attack once per turret and wait for delay
            {
                if (eachAttackDelayNum >= enemyHandler.attackOut.Length)
                {
                    // reset data
                    eachAttackDelayNum = -1;
                    enemyHandler.enemyData.firerate = getFireRate;
                    ResetDelay();
                }
                else
                { 
                    CreateArtilleryAttack(enemyHandler.attackOut[index]);
                }
            }
            else // not implemented attack delay, turret will fire at the same time
            {
                foreach (Transform t in enemyHandler.attackOut) // get how many attackOut is there
                {
                    CreateArtilleryAttack(t); // create attack base on the number of attack out
                }

                // reset data
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
        private void CreateArtilleryAttack(Transform attackT)
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
