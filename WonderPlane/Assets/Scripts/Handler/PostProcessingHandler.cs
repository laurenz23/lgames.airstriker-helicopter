using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/*
 * This script is attached to PostProcessing object at the heirarchy
 * use this script to handle the post processing of the game
 */
namespace game_ideas
{
    public class PostProcessingHandler : MonoBehaviour
    {
        public Transform cameraTransform; // reference for position of post processing
        public GameObject postProcessing_ground;
        public Transform postProcessing_terrianDetector; // reference for the height of terrain detector

        private Volume volume_ground;
        private float maxHeightGround = 15f;
        private float effectValue;
        private float newValue;

        private void Start()
        {

            volume_ground = postProcessing_ground.GetComponent<Volume>(); // assign the volume of post processing attached to it's object
            effectValue = maxHeightGround; // assign the max height ground as effect value

        }

        private void Update()
        {

            // follow the camera movements of y and z only, x axis have fix position to avoid bugs
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y, cameraTransform.position.z);

            // change the weight volume of post processing ground
            VolumeGround();

        }


        //Ground Volume Detector
        private RaycastHit groundHit;
        

        // this method will set the weight volume of post processing ground according to terrain detector height
        private void VolumeGround()
        {

            // raycast terrain detector to bottom, to check the height of terrain detector
            if (Physics.Raycast(postProcessing_terrianDetector.position, -Vector3.up, out groundHit))
            {

                // check if the raycast already detect the terrain
                if (groundHit.collider.CompareTag(GameTag.Terrain.ToString()))
                {

                    // if the detected terrain is equal or lessthan to maximum height of the ground, then change the weight volume of post processing ground
                    if (groundHit.distance <= maxHeightGround)
                    {

                        newValue = ((effectValue - groundHit.distance) / 100) * 3; // formula to have smooth effect for changing values
                        volume_ground.weight = 0.7f + newValue; // set the volume weight of post processing

                    }

                }

            }

        }

    }
}
