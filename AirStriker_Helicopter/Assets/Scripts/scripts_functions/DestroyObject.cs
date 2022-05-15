using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage:      attached this script to object that going to destroy(hide or disabled the object)
/// functions:  instead of destroying the object we disabled it to be abled to use again by poolManager
/// </summary>
namespace game_ideas
{
    public class DestroyObject : MonoBehaviour
    {
        public float delay = 1f;
        private float delayTime = 0f;

        private void Update()
        {
            delayTime += 1f * Time.deltaTime;

            if (delayTime >= delay)
            {
                delayTime = 0f; 
                gameObject.SetActive(false);
            }
        }
    }
}
