using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to explosion trigger child of guided missile armament
/// handling the movement of the armament
/// </summary>

namespace game_ideas
{
    public class GuidedAttack : MonoBehaviour
    {

        public GameObject[] armament;
        public GameObject targetUI_prefab; // reference for target ui prefab

        public Transform target; // will set the target
        [HideInInspector] public Rigidbody RIGIDBODY;

        [SerializeField] private ArmamentAttackData armamentAttackData = null;

        private float rotateSpeed = 1000f;
        private GameObject targetUI;

        private void Start()
        {
            RIGIDBODY = GetComponent<Rigidbody>();
            armamentAttackData = GetComponentInChildren<ArmamentAttackData>();

#if UNITY_EDITOR
            if (!GetComponentInChildren<GuidedAttackTargetFinder>())
            {
                Debug.LogError("Guided Attack Error : Can't find child GUIDED ATTACK TARGET FINDER, please create an object and attached that script.");
            }

            if (!GetComponentInChildren<GuidedAttackExplosionTrigger>())
            {
                Debug.LogError("Guided Attack Error : Can't find child GUIDED ATTACK EXPLOSION TRIGGER, please create an object and attached that script.");
            }
#endif

            // create a target lock ui prefab and hide it first while there is no target 
            targetUI = Instantiate(targetUI_prefab);
            targetUI.SetActive(false);

        }

        private void Update()
        {

            // implement cross product to follow the target
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;

                direction.Normalize();

                float rotateAmount = Vector3.Cross(direction, transform.forward).x;

                RIGIDBODY.angularVelocity = new Vector3(-rotateAmount * rotateSpeed * Time.deltaTime, 0f, 0f);

                // check if the target ui prefab is not destroy to avoid errors
                // when script trying to acces the object even it is destroyed
                if (targetUI != null)
                {
                    // show target ui
                    targetUI.SetActive(true);

                    // update the position of target ui prefab
                    targetUI.transform.position = target.position;
                }

            }
            else
            {

                // check if the target ui prefab is not destroy to avoid errors
                // when script trying to access the object even it is destroyed
                if (targetUI != null)
                {
                    // if there is no target, hide the target ui prefab 
                    targetUI.SetActive(false);
                }

            }
            
            transform.position += transform.forward * armamentAttackData.GetSpeed() * Time.deltaTime;
        }

        public void DestroyArmament()
        {
            // reset variables of guided attack
            target = null;
            transform.rotation = Quaternion.identity;
            RIGIDBODY.angularVelocity = Vector3.zero;

            // we don't need to destory the object, since the object is added to pooling object list
            // instead set active to false to be use later at pooling manager script
            gameObject.SetActive(false);

            // check if target ui is not destroy to avoid errors
            if (targetUI != null)
            {
                // once the guided attack is trigger or exploded
                // destroy the target ui prefab
                Destroy(targetUI);
            }

        }

    }
}
