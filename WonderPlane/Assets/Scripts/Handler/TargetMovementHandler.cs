using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to target movement object
/// reference for background and camera movement from y and z axis
/// </summary>

namespace game_ideas
{
    public class TargetMovementHandler : MonoBehaviour
    {

        [SerializeField] private GameManager gameManager = null;
        private float movementForward = 5f; // object movement forward

        // Update is called once per frame
        void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {

                transform.Translate(Vector3.forward * movementForward * Time.deltaTime);
            }

        }
    }
}
