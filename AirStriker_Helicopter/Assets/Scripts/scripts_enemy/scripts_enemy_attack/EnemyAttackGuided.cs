using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached to attack guided armament as a parent 
/// goal: handling the movement of EnemyAttackGuided
/// </summary>

namespace game_ideas
{
    public class EnemyAttackGuided : MonoBehaviour
    {
       
        public Transform target;

        [HideInInspector] public Rigidbody RIGIDBODY;
        [HideInInspector] public ArmamentAttackData armamentAttackData;

        private float armamentSpeed;
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
            if (!GetComponent<ArmamentAttackData>())
            {
                armamentAttackData = GetComponentInChildren<ArmamentAttackData>();
            }
            else
            {
                armamentAttackData = GetComponent<ArmamentAttackData>();
            }

            armamentSpeed = armamentAttackData.GetSpeed();
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

            transform.position += transform.forward * armamentSpeed * Time.deltaTime;
        }

        public void DestroyArmament()
        {
            // reset armament variables and transforms
            target = null;
            RIGIDBODY.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;

            // set the armament object active false, to be later use at pooling manager script
            gameObject.SetActive(false);
        }

    }
}
