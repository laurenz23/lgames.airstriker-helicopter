using UnityEngine;
using System.Collections;

/*
 * This script is attached to enemy object itself
 * object move at one direction only, can be change depends to rotation of object
 */
namespace game_ideas
{
    public class OneWayMovement : MonoBehaviour
    {

        public Transform character;

        public float speed;

        [HideInInspector]
        public GameManager gameManager;

        private void Awake()
        {

            gameManager = GameManager.GetInstance();

        }

        private void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {

                character.Translate(Vector3.forward * speed * Time.deltaTime);

            }

        }
    }
}
