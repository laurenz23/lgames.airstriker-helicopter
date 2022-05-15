using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  usage: attached to EffectPrefabObject game object at the hierarchy
 *  goal: use this script to access effect prefabs
 *  and to create prefab effects by calling the CreatePrefabEffect method to other scripts
 */
namespace game_ideas
{
    public class EffectPrefabManager : MonoBehaviour
    {
        private static EffectPrefabManager instance;

        public static EffectPrefabManager GetInstance()
        {
            return instance;
        }

        // popup text prefab
        [Header("Popup Text")]
        public GameObject popupText_health;
        public GameObject popupText_damage;
        public GameObject popupText_points;

        // particle effects prefab
        [Header("Particel Effects")]
        public GameObject playerTrailEffect;
        public GameObject coinParticleEffect;
        public GameObject basicBulletHitEffect;
        public GameObject explosionEffect;
        public GameObject largeExplosionEffect;
        public GameObject mushroomExplosionEffect;

        private PoolingManager poolingManager;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            poolingManager = FindObjectOfType<PoolingManager>();
        }

        // call this method to display the popup text
        public void DisplayPopupText(Transform other, string poolName, string value)
        {
            // create a prefab for popup text
            PoolPopupText(poolName, Quaternion.identity,
                new Vector3(12f, other.position.y + 3f, other.position.z + 1.5f), new Vector3(1f, 1f, 1f), value);
        }

        // the properties of creation is assigned independtly and destroy the created prefab according to assign destroy time with a default value of 1f
        public void PoolEffect(string poolName, Quaternion rotation, Vector3 position, Vector3 scale)
        {
            GameObject poolObj = poolingManager.GetPooledObjectEffects(poolName);
            poolObj.transform.rotation = rotation;
            poolObj.transform.position = position;
            poolObj.transform.localScale = scale;
            poolObj.SetActive(true);
        }

        public void PoolExplosion(string poolName, Quaternion rotation, Vector3 position, Vector3 scale)
        {
            GameObject poolObj = poolingManager.GetPooledObjectExplosion(poolName);
            poolObj.transform.rotation = rotation;
            poolObj.transform.position = position;
            poolObj.transform.localScale = scale;
            poolObj.SetActive(true);
        }

        private void PoolPopupText(string poolName, Quaternion rotation, Vector3 position, Vector3 scale, string textValue)
        {
            GameObject poolObj = poolingManager.GetPooledObjectPopupText(poolName);
            poolObj.transform.rotation = rotation;
            poolObj.transform.position = position;
            poolObj.transform.localScale = scale;
            poolObj.GetComponent<PopupTextHandler>().SetTextValue(textValue);
            poolObj.SetActive(true);
        }

    }
}
