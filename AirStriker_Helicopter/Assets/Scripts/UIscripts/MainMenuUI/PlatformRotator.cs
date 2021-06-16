using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to object that going to rotate infinitely
/// </summary>

namespace game_ideas
{
    public class PlatformRotator : MonoBehaviour
    {

        [SerializeField] private float speed; // assign the rotation speed of object

        private void Update()
        {
            // rotate object in y axis
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (speed * Time.deltaTime), transform.eulerAngles.z);
        }

    }
}
