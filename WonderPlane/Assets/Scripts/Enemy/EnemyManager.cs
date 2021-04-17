using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// this script is attached to enemy character itself as parent
/// </summary>

namespace game_ideas
{

    public class EnemyManager : MonoBehaviour
    {

        public EnemyData enemyData;
        public CameraManager cameraManager;

        private EffectHandler effectHandler;

        private void Awake()
        {
            cameraManager = FindObjectOfType<CameraManager>();
        }

        private void Start()
        {
            effectHandler = FindObjectOfType<EffectHandler>();
        }

        // explode these character
        public void DestroyCharacter(GameObject explosionEffect = null)
        {
            GameObject newEffect; 

            this.gameObject.SetActive(false);

            // since we have different kind of explosion effect if the enemy collided
            // we assign explosion effect base on player armament type or player explosion effect itself
            if (explosionEffect != null)
            {
                newEffect = explosionEffect; // if didn't provide an explosion effect, set explosion effect as default
            }
            else
            {
                newEffect = effectHandler.explosionEffect; // assign the explosion effect
            }

            if (effectHandler != null)
            {
                // display the additional points for the player once it is destroyed
                effectHandler.DisplayPopupText(transform, effectHandler.popupText_points, "+" + enemyData.points.ToString());

                // create exposion effect
                effectHandler.CreatePrefabEffectAndDestroy(newEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z), 5f);
            }

            Destroy(this.gameObject, 6f);

        }

    }
}
