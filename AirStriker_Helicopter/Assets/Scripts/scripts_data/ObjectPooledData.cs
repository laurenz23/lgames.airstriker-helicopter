using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage:  scriptableObject to create a pool data
///         NOTE: object pool name must unique
/// </summary>
namespace game_ideas
{
    [CreateAssetMenu(fileName = "New Object Pool", menuName = "Project/Object Pool")]
    public class ObjectPooledData : ScriptableObject
    {

        public string objectPooledName; // name must unique
        public GameObject objectPrefab;
        public int objectStartingSize;
        public bool objectOnEnabled;

    }
}
