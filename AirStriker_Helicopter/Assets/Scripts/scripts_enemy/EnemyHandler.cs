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

        [Header("Get the child character (can be null)")]
        public Transform character; // get the child character, can be null

        [HideInInspector] public CameraManager cameraManager;

        [HideInInspector] public GameAssetsManager gameAssetManager;

        [HideInInspector] public OnHitCharacter onHitCharacter;

        [HideInInspector] public SoundFXHandler soundFXHandler;

        [HideInInspector] public EffectPrefabManager effectPrefabManager;

        private void Awake()
        {
            soundFXHandler = FindObjectOfType<SoundFXHandler>();

            cameraManager = FindObjectOfType<CameraManager>();

            gameAssetManager = GameAssetsManager.GetInstance();

            effectPrefabManager = FindObjectOfType<EffectPrefabManager>();

            onHitCharacter = GetComponent<OnHitCharacter>();

            if (onHitCharacter != null)
            {
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
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Please attached OnHitCharacter class to this object.");
#endif
            }
        }

        // explode these character
        public void DestroyCharacter()
        {
            soundFXHandler.SFX_EXPLODE("explode3");

            string explosionPoolName;
            
            gameObject.SetActive(false); // reference for guided attack to avoid ab normal behavior of the guided attack

            // since we have different kind of explosion effect if the enemy collided
            // we assign explosion effect base on player armament type or player explosion effect itself
            if (enemyData.deathExplosionPoolName == null)
            {
                explosionPoolName = "explosionCharacter";  // if didn't provide an explosion effect, set explosion effect as default
            }
            else
            {
                explosionPoolName = enemyData.deathExplosionPoolName; // assign the explosion effect
            }

            if (effectPrefabManager != null)
            {
                // display the additional points for the player once it is destroyed
                effectPrefabManager.DisplayPopupText(transform, "popupTextPoints", "+" + enemyData.points.ToString());

                // create exposion effect
                effectPrefabManager.PoolExplosion(explosionPoolName, Quaternion.identity,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(1f, 1f, 1f));
            }

            // check if game object have destroy assets object to replace the current object to destroy
            if (GetComponent<EnemyAssetDestroy>())
            {
                GetComponent<EnemyAssetDestroy>().CreateAssetDestroy(transform);
            }

            Destroy(gameObject, 0.1f);

        }

    }
}
