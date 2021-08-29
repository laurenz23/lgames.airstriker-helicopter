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

        public string attackPoolName;

        public float attackFirerate;

        public float eachAttackDelayRate;

        public Transform[] attackPointing;

        public string soundFXName;


        private GameManager gameManager;

        private SoundFXHandler soundFXHandler;

        private CameraManager cameraManager;

        private PoolingManager poolingManager;

        private float nextAttack;

        private float nextAttackFirerate;

        private int nextAttackIndex;
        
        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();

            cameraManager = FindObjectOfType<CameraManager>();

            soundFXHandler = FindObjectOfType<SoundFXHandler>();

            poolingManager = FindObjectOfType<PoolingManager>();

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
            // check if inside the camera view
            // if inside camera view play sound fx
            if (cameraManager.IfView(transform))
            {
                soundFXHandler.SFX_SHOOT_LASER(soundFXName);
            }

            // pool attack object from pooling manager 
            GameObject poolObj = poolingManager.GetPooledObjectEnemyArm(attackPoolName);
            poolObj.transform.eulerAngles = new Vector3(apT.eulerAngles.x, apT.eulerAngles.y, 0f);
            poolObj.transform.position = apT.position;
            poolObj.SetActive(true);

            ArmamentAttackData armamentAttackData;

            if (poolObj.GetComponent<ArmamentAttackData>())
            {
                armamentAttackData = poolObj.GetComponent<ArmamentAttackData>();
            }
            else
            {
                armamentAttackData = poolObj.GetComponentInChildren<ArmamentAttackData>();
            }

            // if theres a muzzleF lash object attached
            // create a muzzleFlash effect
            if (armamentAttackData.HasMuzzleFlash())
            {
                GameObject muzPoolObj = poolingManager.GetPooledObjectMuzzleFlash(armamentAttackData.GetMuzzleFlashPoolName());
                muzPoolObj.GetComponent<MuzzleFlash>().attackPointTransform = apT;
                muzPoolObj.transform.position = apT.position;
                muzPoolObj.transform.eulerAngles = new Vector3(apT.eulerAngles.x, apT.eulerAngles.y, 0f);
                muzPoolObj.transform.localScale = armamentAttackData.GetMuzzleFlashScale();
                muzPoolObj.SetActive(true);
            }
        }

    }
}
