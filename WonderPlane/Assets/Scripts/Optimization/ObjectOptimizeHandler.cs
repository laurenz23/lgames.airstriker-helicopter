using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to object that will going to optimize 
/// NOTE: the object that is attached to this script will be disabled and enabled 
///       or it will not do anything unless it is in the field view of the player(inside the camera view)
///       to improve game performance. Do not attached this script to object that will run 
///       from start till the end of game.
///       
///       In order to optimize the object set it as a child of this script
///       and this script that attached to have collider 
/// </summary>

namespace game_ideas
{
    public class ObjectOptimizeHandler : MonoBehaviour
    {

        public bool enable;

        private void Start()
        {
            // check if the object is enable or disable when game is started
            ObjectEnabled(enable);
        }

        public void ObjectEnabled(bool enabled)
        {
            // if the script that attached to has no child will do nothing also send warning
            if (transform.childCount == 0)
            {
                Debug.LogError("OBJECT OPTIMIZATION SCRIPT : Requires at least one child");
                return;
            }

            // enable and disable the object
            if (enabled)
            {
                if (transform.GetChild(0))
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(true);
                }

                // mark the object as enabled for reference when disabling if exits the collider
                if (GetComponent<EnemyColliderHandler>())
                    GetComponent<EnemyColliderHandler>().enabled = true;

            }
            else
            {
                if (transform.GetChild(0))
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }

                // mark the object as disabled for reference when enabling if enters the collider
                if (GetComponent<EnemyColliderHandler>())
                    GetComponent<EnemyColliderHandler>().enabled = false;
            }
        }

    }
}
