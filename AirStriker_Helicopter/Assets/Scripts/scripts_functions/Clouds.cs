using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class Clouds : MonoBehaviour
    {

        public float speed;

        private float cloudHalfBounds;

        private SpriteRenderer spriteRenderer;

        private CameraManager cameraManager;

        private GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {

            cameraManager = FindObjectOfType<CameraManager>();

            gameManager = FindObjectOfType<GameManager>();

            // get width and height of sprite renderer
            spriteRenderer = GetComponent<SpriteRenderer>();

            cloudHalfBounds = spriteRenderer.bounds.size.z / 2f; // get the width of cloud and divide by 2

        }

        private void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);

                // hide the cloud if outside the camera field view by adding the cloud width to current position by x axis
                if ((transform.position.z + cloudHalfBounds) < ((cameraManager.transform.position.z * 2f) - cameraManager.screenBounds.z))
                {
                    // set a new position
                    transform.position = new Vector3(transform.position.x, transform.position.y, (cameraManager.screenBounds.z + cloudHalfBounds));
                }
            }

        }

    }
}
