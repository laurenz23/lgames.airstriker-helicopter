using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script handles the movement in y axis direction
/// object moves in y axis direction while following the target
/// </summary>

namespace game_ideas
{
    public class OnTargetMovementVertical : MonoBehaviour
    {

        public Transform character; // attached here the object that will going to move

        public float speed;

        public float keepDistance; // this will make the object keeping it's distance from the target
        

        private float posY;

        public Transform playerTransform;

        private CameraManager cameraManager;

        private Vector3 screenBounds;

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameManager.GetInstance();

            cameraManager = FindObjectOfType<CameraManager>();

            playerTransform = FindObjectOfType<PlayerManager>().GetPlayerTransform(); // set the player as the target
        }

        private void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                screenBounds = cameraManager.screenBounds;

                posY = playerTransform.position.y + keepDistance;

                character.position = Vector3.MoveTowards(character.position, new Vector3(0f, posY, character.position.z), speed * Time.deltaTime);
            }

        }

    }
}
