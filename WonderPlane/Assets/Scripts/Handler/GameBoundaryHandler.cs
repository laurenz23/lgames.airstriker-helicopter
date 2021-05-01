using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to Game Boundary Object
/// Handles the boundary area of the game or simply in game area
/// Player is the center boundary
/// </summary> 

namespace game_ideas
{
    public class GameBoundaryHandler : MonoBehaviour
    {

        public Transform playerTransform;

        private void Start()
        {
#if UNITY_EDITOR
            if (playerTransform.Equals(null))
            {
                Debug.LogError("Game Boundary Handler is missing a reference, please attached Player Manager object");
            }
#endif
        }

        private void FixedUpdate()
        {

            transform.position = playerTransform.position;

        }

    }
}
