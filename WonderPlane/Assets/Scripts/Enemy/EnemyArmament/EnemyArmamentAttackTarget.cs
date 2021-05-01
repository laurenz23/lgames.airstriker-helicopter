using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// this script is attached to enemy artillery object
/// handles the attack of the artillery that includes the firerate of the artillery
/// </summary>

namespace game_ideas
{

    public class EnemyArmamentAttackTarget : MonoBehaviour
    {

        public float fireRate;
        
        [SerializeField] private GameObject attackPrefab = null;
        [SerializeField] private EnemyArmamentFindTarget enemyArtilleryTarget = null;
        [SerializeField] private Transform[] attackOut = null;
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
            getFireRate = fireRate;
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
                        if (implementEachAttackDelay)
                        {
                            eachAttackDelayNum = eachAttackDelayNum + 1;
                            fireRate = fireRate + eachAttackDelayRate;
                        }

                        ArtilleryAttack(eachAttackDelayNum);
                    }
                }
            }
        }

        private void ArtilleryAttack(int index = 0)
        {

            if (implementEachAttackDelay)
            {
                if (eachAttackDelayNum >= attackOut.Length)
                {
                    eachAttackDelayNum = 0;
                    fireRate = getFireRate;
                    ResetDelay();
                }
                else
                { 
                    CreateArtilleryAttack(attackOut[index]);
                }
            }
            else
            {
                foreach (Transform t in attackOut)
                {
                    CreateArtilleryAttack(t);
                }

                ResetDelay();
            }

        }

        private void ResetDelay()
        {
            attackDelay = 0f;
        }

        // rotation and position of attack armament is base on attack out that is attached to gun object prefab
        private void CreateArtilleryAttack(Transform attackT)
        {
            GameObject newObj = Instantiate(attackPrefab) as GameObject;
            newObj.transform.rotation = attackT.rotation; 
            newObj.transform.position = attackT.position;
        }

    }
}
