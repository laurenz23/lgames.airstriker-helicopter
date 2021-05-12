using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to attack guided armament as a parent 
/// handling the movement of the armament
/// </summary>

namespace game_ideas
{
    public class EnemyAttackGuided : MonoBehaviour
    {

        public float armamentSpeed;
        public Transform target;

        [HideInInspector] public bool stop = false;
        [HideInInspector] public Rigidbody RIGIDBODY;

        private float rotateSpeed = 200f;

        private void Start()
        {
            RIGIDBODY = GetComponent<Rigidbody>();

#if UNITY_EDITOR
            if (!GetComponentInChildren<EnemyGuidedTrigger>())
            {
                Debug.LogError("Enemy Attack Guided Error: Can't find the child Enemy Guided Trigger, please create an object and attached that script.");
            }

            if (!GetComponentInChildren<EnemyGuidedFindTarget>())
            {
                Debug.LogError("Enemy Attack Guided Error: Can't find the child Enemy Guided Find Target, please create an object and attached that scirpt.");
            }
#endif

        }

        private void Update()
        {
            // implement cross product to follow the target
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;

                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.forward).x;

                RIGIDBODY.angularVelocity = new Vector3(-rotateAmount * rotateSpeed, 0f, 0f);
            }

            // once the bullet is disable it will stop to move and hide the bullet while waiting to destroy the object
            if (stop)
            {
                RIGIDBODY.isKinematic = true;
            }
            else
            {
                transform.position += transform.forward * armamentSpeed * Time.deltaTime;
            }
        }

        public void StopArmament()
        {
            // disable the bullet object
            stop = true;

            // wait to finish the trail effect before destroying the object
            // added a certain delay before destroying the bullet object for the trail effect
            Destroy(this.gameObject, 1f);
        }

    }
}
