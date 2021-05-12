using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to enemy group object as a parent
/// children must have object optimize handler script
/// Handles the activation of the child object (enemy objects)
/// To synchronize the enemy group movements and their attacks.
/// </summary>

namespace game_ideas
{
    public class EnemyGroupHandler : MonoBehaviour
    {

        // if the fixed position is true in inspector, once the group handler is enabled it will not change its position
        // otherwise it will set position according to player y axis
        public bool fixedPosition = false;
 
        [HideInInspector]
        public bool alreadyEnabled = false; // know if the group is already enabled

        // call this method to enabled the group
        public void EnableGroup()
        {
            // don't proceed to the following lines if already enabled or no child
            if (alreadyEnabled || transform.childCount == 0)
                return;

            // proceed to this lines if have a child
            for (int x = 0; x < transform.childCount; x++)
            {
                if (transform.GetChild(x).GetComponent<ObjectOptimizeHandler>())
                {
                    transform.GetChild(x).GetComponent<ObjectOptimizeHandler>().enable = true;
                    transform.GetChild(x).GetComponent<ObjectOptimizeHandler>().ObjectEnabled(true);
                }
            }

            // once the method call assign the already enabled as true  
            alreadyEnabled = true;
        }
    }
}
