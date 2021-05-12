using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script handles the hit effect when character is hit
/// the character will change the material to make the material red when hit
/// call the SetMaterial at Start function of the main character
/// </summary>

namespace game_ideas
{
    public class OnHitCharacter : MonoBehaviour
    {

        private Renderer[] renderers = null;
        private Material default_material = null; // reference for default material
        private Material hit_material = null; // reference for hit material

        private GameAssetsManager gameAssetManager;

        private void Awake()
        {
            gameAssetManager = GameAssetsManager.GetInstance();
        }

        public void SetMaterial(Transform parentMaterials = null)
        {
            // if the parentMateri
            if (parentMaterials == null)
            {
                renderers = GetComponentsInChildren<Renderer>(); // get all objects that have mesh renderer
            }
            else
            {
                renderers = parentMaterials.GetComponentsInChildren<Renderer>(); // get all objects that have mesh renderer
            }

            default_material = gameAssetManager.defaultMaterial;
            hit_material = gameAssetManager.hitMaterial;

            foreach (Renderer r in renderers)
            {
                r.material = default_material;
            }
        }

        public void OnHit()
        {
            foreach (Renderer r in renderers)
            {
                r.material = hit_material;
            }

            StartCoroutine(RestoreDefaultMaterial());
        }

        IEnumerator RestoreDefaultMaterial()
        {
            yield return new WaitForSeconds(0.2f);

            // change the material to default
            foreach (Renderer r in renderers)
            {
                r.material = default_material;
            }

            // stop the running coroutine
            StopCoroutine(RestoreDefaultMaterial());
        }

    }
}
