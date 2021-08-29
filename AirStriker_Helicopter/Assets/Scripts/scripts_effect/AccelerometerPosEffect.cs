using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// usage:      handles the effect of background image when device is tilting
/// </summary>
namespace game_ideas
{
    public class AccelerometerPosEffect : MonoBehaviour
    {

        [SerializeField]
        private float accelerometerSpeed;

        [SerializeField]
        private float clampPos;

        private InputManager inputManager;

        private void Awake()
        {
            inputManager = InputManager.GetInstance();
        }

        private void Update()
        {
            Vector3 accelerometer = inputManager.DeviceAccelerometer();

            transform.position = new Vector3(Mathf.Clamp(transform.position.x + accelerometer.x * accelerometerSpeed * Time.deltaTime, -clampPos, clampPos), transform.position.y, transform.position.z);
        }
    }
}
