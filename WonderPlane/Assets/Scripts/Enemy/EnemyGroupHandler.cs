using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to enemy group object that have a child of enemy objects 
/// Handles the activation of the child object (enemy objects)
/// To synchronize the enemy group movements and their attacks
/// </summary>

namespace game_ideas
{
    public class EnemyGroupHandler : MonoBehaviour
    {

        public bool alreadyEnabled = false;

        public void EnableGroup()
        {
            // don't proceed to the following lines if already enabled or no child
            if (alreadyEnabled || transform.childCount == 0)
                return;

            // proceed to this lines if have a child
            for (int x = 0; x < transform.childCount; x++)
            {
                transform.GetChild(x).GetComponent<ObjectOptimizeHandler>().enable = true;
                transform.GetChild(x).GetComponent<ObjectOptimizeHandler>().ObjectEnabled(true);
            }

            // once the method call assign the already enabled as true  
            alreadyEnabled = true;
        }
    }
}
