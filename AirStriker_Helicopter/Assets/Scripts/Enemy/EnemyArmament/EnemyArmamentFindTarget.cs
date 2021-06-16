using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached to object that have an artillery as a child
/// goal: find a target of artillery and lock on to the target 
/// it includes the rotation of the turret
/// assign a min and max rotation of the turret
/// if turret will rotate at 360 degress use TurretRotation instead
/// </summary>

namespace game_ideas
{
    public class EnemyArmamentFindTarget : MonoBehaviour
    {
        public Transform artilleryTurret = null;
        public float range;
        public float turretRotationSpeed;

        [SerializeField] private Transform artilleryMain;
        [SerializeField] private AttackHandler attackHandler;

        private PlayerManager playerManager;
        private Transform target;

        private Vector3 relativePosition;
        private Quaternion targetRotation;
        private bool rotating = false;
        private float rotationTime;

        // maximum and minimum turret rotation
        private float maxRotation = 0f;
        private float minRotation = 0f;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        private void Start()
        {
            target = playerManager.playerTransform;

            attackHandler.enabled = false; // don't attack

            // assign artillery turret rotation to avoid abnormal rotation transition
            if (artilleryMain.transform.eulerAngles.y == 0f && artilleryMain.transform.eulerAngles.z == 0f) // if artillery is facing forward from game world
            {
                artilleryTurret.localEulerAngles = new Vector3(90f, 0f, 0f);
                maxRotation = 0f;
                minRotation = 80f;
            }
            else if (artilleryMain.transform.eulerAngles.y == 180f && artilleryMain.transform.eulerAngles.z == 0f) // if artillery is facing backward from game world
            {
                artilleryTurret.localEulerAngles = new Vector3(90f, 0f, 0f);
                maxRotation = 100f;
                minRotation = 180f;
            }
            else if (artilleryMain.transform.eulerAngles.y == 180f && artilleryMain.transform.eulerAngles.z == 180f) // if artiller is facing backward and downward from game world
            {
                artilleryTurret.localEulerAngles = new Vector3(90f, 0f, -180f);
                maxRotation = -180f;
                minRotation = -100f;
            }
            else if (artilleryMain.transform.eulerAngles.y == 0f && artilleryMain.transform.eulerAngles.z == 180f) // if artillery is facing forward and downward from game world
            {
                artilleryTurret.localEulerAngles = new Vector3(90f, 0f, -180f);
                maxRotation = -80f;
                minRotation = 0f;
            }
            
        }

        private void Update()
        {
            // check if inside the attack range of the turret
            if (Vector3.Distance(transform.position, playerManager.playerTransform.position) < range)
            {
                relativePosition = target.position - transform.position;
                targetRotation = Quaternion.LookRotation(relativePosition);
                rotating = true;
                rotationTime = 0;
            }

            if (rotating)
            {
                rotationTime += Time.deltaTime * turretRotationSpeed;

                // compute the maximum and minimum rotation of the turret
                Vector3 direction = target.position - transform.position;
                float angleRotation = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
                
                //Debug.Log(angleRotation);

                if (angleRotation >= maxRotation && angleRotation <= minRotation)
                {
                    artilleryTurret.rotation = Quaternion.Lerp(artilleryTurret.rotation, targetRotation, rotationTime);
                    attackHandler.enabled = true; // start attacking
                }
                else
                {
                    attackHandler.enabled = false; // don't attack
                }
                
            }

            if (rotationTime > 1)
            {
                rotating = false;
            }
        }

    }
}
