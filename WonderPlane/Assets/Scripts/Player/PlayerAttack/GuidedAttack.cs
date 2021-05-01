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

        [HideInInspector] public Transform target; // will set the target
        [HideInInspector] public Rigidbody RIGIDBODY;

        [SerializeField] private PlayerAttackInfo playerAttackInfo = null;

        private float rotateSpeed = 200f;
        private GameObject targetUI;

        // we cannot destroy the bullet object instantly, wait for trail effect to finish to have a nice effect
        // so we assign the disabled bullet to hide and stop the movement of bullet once it collided
        private bool disabledBullet = false;

        private void Start()
        {
            RIGIDBODY = GetComponent<Rigidbody>();
            playerAttackInfo = GetComponentInChildren<PlayerAttackInfo>();

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

                RIGIDBODY.angularVelocity = new Vector3(-rotateAmount * rotateSpeed, 0f, 0f);

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

            // once the bullet is disable it will stop to move and hide the bullet while waiting to destroy the object
            if (disabledBullet)
            {
                RIGIDBODY.isKinematic = true;
            }
            else
            {
                transform.position += transform.forward * playerAttackInfo.playerAttackData.speed * Time.deltaTime;
            }
        }

        public void DestroyArmament()
        {

            // disable the bullet object 
            disabledBullet = true;

            // hide the bullet so the player won't see while waiting to destroy the object
            foreach (GameObject arma in armament)
            {
                arma.SetActive(false);
            }

            // check if target ui is not destroy to avoid errors
            if (targetUI != null)
            {
                // once the guided attack is trigger or exploded
                // destroy the target ui prefab
                Destroy(targetUI);
            }

            // wait to finish the trail effect before destroying the object
            // added a certain delay before destroying the bullet object for the trail effect
            Destroy(gameObject, 1f);

        }

    }
}
