using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached as a parent of character objects
/// with Path Movement script attached to
/// NOTE:   
///         variable characters with List<PathMovement> data type and
///         variable paths with List<Transform> data type
///         please attached them manually for now
/// </summary>

namespace game_ideas
{
    public class PathMovementDistributor : MonoBehaviour
    {

        [SerializeField] private List<PathMovement> characters = new List<PathMovement>();
        [SerializeField] private List<Transform> paths = new List<Transform>();

        private void Start()
        {

            // check if characters is not empty
            if (characters.Count.Equals(0))
            {

#if UNITY_EDITOR
                Debug.LogError("Path Movement Distributor: Please attached character path movement");
#endif

                return;
            }

            if (paths.Count.Equals(0))
            {

#if UNITY_EDITOR
                Debug.LogError("Path Movement Distributor: Please attached path objects");
#endif

                return;

            }

            // assign the path objects to characters with path movement script
            foreach (PathMovement c in characters)
            {
                c.pathObjects = paths;
            }
        }

    }
}
