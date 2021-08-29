using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached this script to turret itself
/// use this class if the turret rotates at 360 degress
/// </summary>

namespace game_ideas
{
    public class TurretRotation : MonoBehaviour
    {
        private Transform turret; 
        private Transform target; // player as target

        private bool turretRotated;

        private void Awake()
        {
            target = FindObjectOfType<PlayerManager>().playerTransform;
            turret = transform;
        }

        private void Start()
        {
            // check if turret is rotated preparation for angle rotation computation
            // the rotation of turret is base on it's parent, it might affect computation
            // the compuation is base on turret rotation
            if (transform.eulerAngles.y == 0f)
            {
                turretRotated = false;
            }
            else
            {
                turretRotated = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 direction;
            float angleRotation;

            // if turret is rotated apply a computation base on turret rotation
            if (turretRotated)
            {
                direction = target.position - transform.position;

                angleRotation = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
            }
            else
            {
                direction = transform.position - target.position;

                angleRotation = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;

                angleRotation = -angleRotation;
            }

            turret.localEulerAngles = new Vector3(angleRotation, 0f, 0f);

        }
    }
}
