using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to guided attack target finder object, child of main guided attack object
/// find a target character(enemy)
/// </summary>

namespace game_ideas
{
    public class GuidedAttackTargetFinder : MonoBehaviour
    {
        public GuidedAttack guidedMissile;

        private CameraManager cameraManager;
        private EffectHandler effectHandler;

        private void Start()
        {
            cameraManager = FindObjectOfType<CameraManager>();
            effectHandler = FindObjectOfType<EffectHandler>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (guidedMissile.target == null)
            {
                if (other.CompareTag(GameTag.Enemy.ToString()))
                {
                    // if character is an enemy it will be target if already in camera field of view
                    if (cameraManager != null)
                    {
                        Vector3 screenBounds = cameraManager.screenBounds;

                        if (other.transform.position.magnitude > screenBounds.magnitude)
                        {
                            return; // no character will be target if not already in camera field of view 
                        }
                        else
                        {
                            guidedMissile.target = other.transform; // set the target character object
                        }
                    }
                }
            }
            else
            {
                // once the selected target has already exploded or destroy, will try to find another character enemy as new target and set the target to null
                if (!guidedMissile.target.gameObject.activeSelf)
                {
                    guidedMissile.target = null;
                    
                    // reset the rotation of the rigidbody to avoid weird rotation movement of armament when while waiting to destroy
                    guidedMissile.RIGIDBODY.angularVelocity = new Vector3(0f, 0f, 0f); 

                    StartCoroutine(SelfExplode());
                }
            }
        }

        // automatically explode 
        IEnumerator SelfExplode()
        {
            yield return new WaitForSeconds(2f);

            foreach (GameObject arma in guidedMissile.armament)
            {
                if (arma.activeSelf)
                {
                    if (effectHandler != null)
                    {
                        effectHandler.CreatePrefabEffectAndDestroy(effectHandler.explosionEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                            new Vector3(0f, transform.position.y, transform.position.z), 3f);
                    }

                    guidedMissile.DestroyArmament();
                }
            }
        }
    }
}
