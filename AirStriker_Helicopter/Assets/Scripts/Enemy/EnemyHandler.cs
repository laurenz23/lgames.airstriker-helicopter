using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// this script is attached to enemy character itself as parent
/// </summary>

namespace game_ideas
{

    public class EnemyHandler : MonoBehaviour
    {
        
        public EnemyData enemyData;

        [Space]
        [Header("Get the child character (can be null)")]
        public Transform character; // get the child character, can be null

        [Header("If character can attack")]
        public AttackData attackData;

        public Transform[] attackOut; 

        [HideInInspector] public CameraManager cameraManager;

        [HideInInspector] public GameAssetsManager gameAssetManager;

        [HideInInspector] public OnHitCharacter onHitCharacter;

        private EffectHandler effectHandler;

        private void Awake()
        {
            cameraManager = FindObjectOfType<CameraManager>();

            gameAssetManager = GameAssetsManager.GetInstance();
        }

        private void Start()
        {
            effectHandler = FindObjectOfType<EffectHandler>();

            onHitCharacter = GetComponent<OnHitCharacter>();

            // preparation for on hit material
            if (character == null)
            {
                onHitCharacter.SetMaterial();
            }
            else
            {
                onHitCharacter.SetMaterial(character);
            }
        }

        // explode these character
        public void DestroyCharacter()
        {
            GameObject newEffect; 

            this.gameObject.SetActive(false); // reference for guided attack to avoid ab normal behavior of the guided attack

            // since we have different kind of explosion effect if the enemy collided
            // we assign explosion effect base on player armament type or player explosion effect itself
            if (enemyData.deathExplosion == null)
            {
                newEffect = effectHandler.explosionEffect;  // if didn't provide an explosion effect, set explosion effect as default
            }
            else
            {
                newEffect = enemyData.deathExplosion; // assign the explosion effect
            }

            if (effectHandler != null)
            {
                // display the additional points for the player once it is destroyed
                effectHandler.DisplayPopupText(transform, effectHandler.popupText_points, "+" + enemyData.points.ToString());

                // create exposion effect
                effectHandler.CreatePrefabEffectAndDestroy(newEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z), 5f);
            }

            if (GetComponent<EnemyAssetDestroy>())
            {
                GetComponent<EnemyAssetDestroy>().CreateAssetDestroy(transform);
            }

            Destroy(this.gameObject, 0.1f);

        }

    }
}
