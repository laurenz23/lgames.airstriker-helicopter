using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// usage: attached this script to enemy character as a child
/// goal: a character movement where can be designated the character next action or movement
/// </summary>

namespace game_ideas
{
    public class PathMovement : MonoBehaviour
    {
        
        public EnemyHandler enemyHandler;

        [Range(1, 10)]
        public float transitionSpeed;

        public bool dontMoveInTransition;

        public bool goingDownFirst;

        [Range(0, 3)]
        public float goingForward;

        [Range(0, 3)]
        public float goingUp;

        [Range(0, 3)]
        public float goingDown;

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                if (goingForward > 0f)
                {
                    goingForward -= 1f * Time.deltaTime;
                }

                if (!goingDownFirst || goingDown <= 0f)
                {
                    // if going up value is greater than 0
                    // then the character have an ability to go up if going forward is done
                    if (goingUp > 0f && goingForward <= 0f)
                    {
                        // decrease the value of going up
                        goingUp -= 1f * Time.deltaTime;

                        // move the character up
                        enemyHandler.transform.position = new Vector3(0f, enemyHandler.transform.position.y + (transitionSpeed * Time.deltaTime), enemyHandler.transform.position.z);

                        if (dontMoveInTransition)
                        {
                            return;
                        }
                    }
                }

                // if going up is done and going down is not
                // then the character will move downward until it's done
                if (goingDown > 0f && goingUp <= 0f || goingDown > 0f && goingForward <= 0f && goingDownFirst)
                {
                    // decrease the value of going down
                    goingDown -= 1f * Time.deltaTime;

                    // move the character down
                    enemyHandler.transform.position = new Vector3(0f, enemyHandler.transform.position.y - (transitionSpeed * Time.deltaTime), enemyHandler.transform.position.z);

                    if (dontMoveInTransition)
                    {
                        return;
                    }
                }

                // check where the character is facing
                if (enemyHandler.transform.eulerAngles.y.Equals(0f)) // character is facing at positive z axis 
                {
                    // move the character in forward direction
                    enemyHandler.transform.Translate(Vector3.forward * enemyHandler.enemyData.movementSpeed * Time.deltaTime);
                }
                else // character is facing at negative z axis
                {
                    // move the character in backward direction
                    enemyHandler.transform.Translate(Vector3.forward * enemyHandler.enemyData.movementSpeed * Time.deltaTime);
                }
            }

        }
        


    }
}
