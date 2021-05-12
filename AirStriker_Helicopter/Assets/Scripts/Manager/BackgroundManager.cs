using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to Background object at the hierarchy
 * Manage the background of the game 
 */
namespace game_ideas
{
    public class BackgroundManager : MonoBehaviour
    {
        public Transform targetObject; // target player position

        private void Update()
        {

            // set the position of background to follow the player movement to forward
            transform.position = new Vector3(transform.position.x, transform.position.y, targetObject.position.z);

        }
    }
}
