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
        public EnemyHandler enemyHandler;

        [HideInInspector]
        public GameManager gameManager;

        private void Awake()
        {

            gameManager = GameManager.GetInstance();

        }

        private void Start()
        {
            // check if character is facing forward
            if (enemyHandler.transform.eulerAngles.y.Equals(0f))
            {
                enemyHandler.enemyData.movementSpeed += 5f;
            }
        }

        private void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {

                enemyHandler.transform.Translate(Vector3.forward * enemyHandler.enemyData.movementSpeed * Time.deltaTime);

            }

        }
    }
}
