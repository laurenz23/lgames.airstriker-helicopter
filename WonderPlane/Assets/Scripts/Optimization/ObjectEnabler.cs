using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to optimization object
/// Handles the activation of objects that enters the trigger collider 
/// We use this method improve our performance
/// </summary>

namespace game_ideas
{
    public class ObjectEnabler : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {

            /*
             * if the obect enter the collider it will call the object enable function to enable the object
             * and start rendering its mesh and start executing the scripts
             * And it will also mark the object as enabled for disabling it again when exits the trigger
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
                    other.GetComponent<ObjectOptimizeHandler>().ObjectEnabled(true); // call the object enable functions

                    /*
                     * check if the object is child of enemy group handler 
                     * if it is collided object that have parent with attached script of enemy group handler
                     * will enable the object as group with the siblings to synchronize the movement and attack
                     * of its siblings(enemy)
                     */

                    if (other.GetComponentInParent<EnemyGroupHandler>())
                    {
                        Transform enemyGroup = other.GetComponentInParent<EnemyGroupHandler>().transform;

                        // don't change position when enemy group handler is already enabled to fix the issue of group object change position when the player change position
                        // if the group is fixed position then it will not change it's default position once the group is enabled
                        if (!enemyGroup.GetComponent<EnemyGroupHandler>().alreadyEnabled && !enemyGroup.GetComponent<EnemyGroupHandler>().fixedPosition)
                        {
                            enemyGroup.position = new Vector3(0f, transform.position.y, enemyGroup.position.z);
                        }
                        
                        enemyGroup.GetComponent<EnemyGroupHandler>().EnableGroup();
                    }
                }
            }

        }
        
    }
}
