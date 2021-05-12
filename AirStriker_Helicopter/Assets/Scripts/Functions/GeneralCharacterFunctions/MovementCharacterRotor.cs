using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached this script to rotor itself
/// </summary>

namespace game_ideas
{
    public class MovementCharacterRotor : MonoBehaviour
    {

        private float speed = 1250f;

        private void Update()
        {

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + speed * Time.deltaTime);

        }

    }
}
