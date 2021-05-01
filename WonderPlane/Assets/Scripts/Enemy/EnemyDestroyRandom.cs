using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script will show a random destroy object
/// </summary>

namespace game_ideas
{
    public class EnemyDestroyRandom : MonoBehaviour
    {

        public GameObject[] destroyedObjects;

        private void Start()
        {

            // set all destoyed objects to false from the array so we can select a random objects that going to display when the enemy is destroyed
            foreach (GameObject g in destroyedObjects)
            {
                g.SetActive(false);
            }

            // random number from 0 to highest number of destroyed objects
            int randomNum = Random.Range(0, destroyedObjects.Length);

            // set an index for random objects array, these destroyed object will going to display when enemy is destroyed
            destroyedObjects[randomNum].SetActive(true);

        }

    }
}
