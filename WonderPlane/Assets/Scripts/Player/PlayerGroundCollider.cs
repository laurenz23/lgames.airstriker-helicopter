using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player wheels collider to check if plane is on ground
/// </summary>

namespace game_ideas
{
    public class PlayerGroundCollider : MonoBehaviour
    {
        public PlayerEffect playerEffect;
        private bool onGround;

        // if helicopter is landed to ground
        private void OnTriggerEnter(Collider collider)
        {

            // helicopter is landed to ground or terrain
            if (collider.CompareTag(GameTag.Ground.ToString()) || collider.CompareTag(GameTag.Terrain.ToString()))
            {
                
                onGround = true;

            }

        }

        private void OnTriggerStay(Collider collider)
        {

            // helicopter is landed to ground or terrain
            if (collider.CompareTag(GameTag.Ground.ToString()) || collider.CompareTag(GameTag.Terrain.ToString()))
            {
                
                onGround = true;

            }
        }

        // if helicopter wheels is leaving from ground
        private void OnTriggerExit(Collider collider)
        {

            // helicopter wheels is leaving from ground or terrain
            if (collider.CompareTag(GameTag.Ground.ToString()) || collider.CompareTag(GameTag.Terrain.ToString()))
            {

                onGround = false;

            }

        }

        // return the status of plane wheels on ground to acces for another script
        public bool OnGround()
        {

            return onGround;

        }
    }
}
