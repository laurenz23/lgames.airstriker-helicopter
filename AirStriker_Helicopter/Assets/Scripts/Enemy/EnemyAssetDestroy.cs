using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to where enemy manager is
/// attached only this script that have destroyed object
/// </summary>

namespace game_ideas
{
    public class EnemyAssetDestroy : MonoBehaviour
    {

        public GameObject objectDestroy;

        public void CreateAssetDestroy(Transform goingDestroy)
        {
            GameObject newObj = Instantiate(objectDestroy, goingDestroy.transform.localPosition, goingDestroy.transform.localRotation, goingDestroy.transform.parent) as GameObject;
            newObj.SetActive(true);
        }

    }
}
