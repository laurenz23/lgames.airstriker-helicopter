using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to artillery object
/// find a target of artillery and lock on to the target 
/// it includes the rotation of the turret
/// </summary>

namespace game_ideas
{
    public class EnemyArtilleryTarget : MonoBehaviour
    {

        public Transform artilleryTurret = null;
        public float range;
        public float turretRotationSpeed;
        public float maxRotation;
        public float minRotation;

        private PlayerManager playerManager;
        private Transform target;

        private Vector3 relativePosition;
        private Quaternion targetRotation;
        private bool rotating = false;
        private float rotationTime;

        private bool readyToFire = false;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        private void Start()
        {
            target = playerManager.GetPlayerTransform();
        }

        private void Update()
        {
            // check if inside the attack range of the turret
            if (Vector3.Distance(transform.position, playerManager.GetPlayerTransform().position) < range)
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

                if (angleRotation >= maxRotation || angleRotation <= minRotation)
                {
                    artilleryTurret.rotation = Quaternion.Lerp(artilleryTurret.rotation, targetRotation, rotationTime);
                    readyToFire = true;
                }
                else
                {
                    readyToFire = false;
                }

                //Debug.Log(angleRotation);
            }

            if (rotationTime > 1)
            {
                rotating = false;
            }
        }

        public bool ArtilleryReadyToFire()
        {
            return readyToFire;
        }
    }
}
