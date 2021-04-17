using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  This script is attached to EffectHandler game object at the hierarchy
 *  use this script to access the effect prefabs
 *  and to create prefab effects by calling the CreatePrefabEffect method to other scripts
 */
namespace game_ideas
{
    public class EffectHandler : MonoBehaviour
    {

        // popup text prefab
        [Header("Popup Text")]
        public GameObject popupText_health;
        public GameObject popupText_damage;
        public GameObject popupText_points;
        public GameObject popupText_coins;
        public GameObject popupText_energy;

        // particle effects prefab
        [Header("Particel Effects")]
        public GameObject playerTrailEffect;
        public GameObject coinParticleEffect;
        public GameObject energyParticleEffect;
        public GameObject basicBulletHitEffect;
        public GameObject explosionEffect;
        public GameObject largeExplosionEffect;
        public GameObject mushroomExplosionEffect;


        // call this method to display the popup text
        public void DisplayPopupText(Transform other, GameObject popupText_prefab, string value)
        {
            // set text value
            popupText_prefab.GetComponent<PopupTextHandler>().SetTextValue(value);

            // create a prefab for popup text
            CreatePrefabEffectAndDestroy(popupText_prefab, transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                new Vector3(12f, other.position.y + 3f, other.position.z + 1.5f));

        }

        // the properties of creation for object is assigned independently 
        public void CreatePrefabEffect(GameObject prefabObject, Transform parent, Vector3 scale, Quaternion rotation, Vector3 position)
        {

            GameObject newObj = Instantiate(prefabObject) as GameObject;
            newObj.transform.parent = parent;
            newObj.transform.localScale = scale;
            newObj.transform.rotation = rotation;
            newObj.transform.position = position;

        }

        // the properties of creation is assigned independtly and destroy the created prefab according to assign destroy time with a default value of 1f
        public void CreatePrefabEffectAndDestroy(GameObject prefabObject, Transform parent, Vector3 scale, Quaternion rotation, Vector3 position, float destroyTime = 1f)
        {

            GameObject newObj = Instantiate(prefabObject) as GameObject;
            newObj.transform.parent = parent;
            newObj.transform.localScale = scale;
            newObj.transform.rotation = rotation;
            newObj.transform.position = position;
            Destroy(newObj, destroyTime);  

        }

    }
}
