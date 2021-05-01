using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to optimization object
/// Handles the disabled of object that exist from the trigger collider
/// </summary>

namespace game_ideas
{
    public class ObjectDisabler : MonoBehaviour
    {

        private void OnTriggerExit(Collider other)
        {
             /*
              * the object will be deactived if exits the trigger 
              * we do not use the destroy method to reserve the object for later use
              * and avoid object lost while in game
              */

            if (
                other.CompareTag(GameTag.Cloud.ToString()) ||
                other.CompareTag(GameTag.Enemy.ToString()) ||
                other.CompareTag(GameTag.Ground.ToString()) ||
                other.CompareTag(GameTag.Coins.ToString()) ||
                other.CompareTag(GameTag.Energy.ToString()) ||
                other.CompareTag(GameTag.Tree.ToString()) ||
                other.CompareTag(GameTag.Props.ToString())
                )
            {
                if (other.GetComponent<ObjectOptimizeHandler>())
                {
                    other.GetComponent<ObjectOptimizeHandler>().ObjectEnabled(false);
                }
            }

        }
    }
}
