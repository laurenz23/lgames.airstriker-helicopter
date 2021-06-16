using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// usage: attached this script to character as a child if character have ability to attack
/// goal: handle character attacks 
/// </summary>

namespace game_ideas
{
    public class AttackHandler : MonoBehaviour
    {

        public AttackData attackData;

        public float attackFirerate;

        public float eachAttackDelayRate;

        public Transform[] attackPointing;

        public string soundFXName;


        private GameManager gameManager;

        private SoundFXHandler soundFXHandler;

        private float nextAttack;

        private float nextAttackFirerate;

        private int nextAttackIndex;


        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();

            soundFXHandler = FindObjectOfType<SoundFXHandler>();
        }

        private void Start()
        {
            ResetAttack();
        }

        private void Update()
        {
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {

                nextAttack += Time.deltaTime;

                if (nextAttack >= nextAttackFirerate)
                {

                    if (eachAttackDelayRate > 0f)
                    {
                        if (nextAttackIndex >= attackPointing.Length)
                        {
                            ResetAttack();
                        }
                        else
                        {
                            CreateAttack(attackPointing[nextAttackIndex]);

                            nextAttackFirerate += eachAttackDelayRate;

                            nextAttackIndex++;
                        }

                    }
                    else
                    {
                        foreach (Transform t in attackPointing)
                        {
                            CreateAttack(t);
                        }

                        ResetAttack();

                    }

                }
            }
        }

        private void ResetAttack()
        {
            nextAttack = 0f;
            nextAttackFirerate = attackFirerate;
            nextAttackIndex = 0;
        }

        private void CreateAttack(Transform apT) // apT = attackPointingTransform
        {
            soundFXHandler.SFX_SHOOT_LASER(soundFXName);

            GameObject createdAttack = Instantiate(attackData.attackPrefab) as GameObject;

            createdAttack.transform.eulerAngles = new Vector3(apT.eulerAngles.x, apT.eulerAngles.y, 0f);

            createdAttack.transform.position = apT.position;

            ArmamentAttackData armamentAttackData;

            if (createdAttack.GetComponent<ArmamentAttackData>())
            {
                armamentAttackData = createdAttack.GetComponent<ArmamentAttackData>();
            }
            else
            {
                armamentAttackData = createdAttack.GetComponent<ArmamentAttackData>();
            }

            armamentAttackData.attackType = attackData.attackType;
            armamentAttackData.damage = attackData.damage;
            armamentAttackData.speed = attackData.speed;
            armamentAttackData.aoe = attackData.aoe;
        }

    }
}
