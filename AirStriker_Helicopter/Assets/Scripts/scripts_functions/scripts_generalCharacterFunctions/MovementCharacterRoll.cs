using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to character as a child
/// handles the roll movement of character 
/// </summary>

namespace game_ideas
{
    public class MovementCharacterRoll : MonoBehaviour
    {
        public Transform character; // character that going to roll
        public float speed; 

        private float newPosY; // reference for new position in y axis when character is changing position
        private float prevPosY; // reference from previous position in y axis when character is changing position
        private float rollTransition; // reference for character rotation in z axis
        private bool fromTop; // use for calculating the roll transition

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameManager.GetInstance();
        }

        private void Update()
        {
            // current position of characters
            newPosY = character.position.y;

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                // if the character ascending
                if (newPosY > prevPosY)
                {
                    // maximum z axis rotation at negative value is -60 degress
                    if (rollTransition >= -60f)
                    {
                        // transition for z axis decrementing since we have -60 degress maximum value
                        rollTransition -= speed * Time.deltaTime;
                    }

                    // calculation reference later
                    fromTop = true;
                }
                // if the character is descending
                else if (newPosY < prevPosY)
                {
                    // maximum z axis rotation at positive value is 40 degress
                    if (rollTransition <= 40f)
                    {
                        // transition for z axis incrementing since we have 40 degress maximum value
                        rollTransition += speed * Time.deltaTime;
                    }

                    // calculation reference later
                    fromTop = false;
                }
                // if character is stationary
                else
                {
                    // if character from top
                    // the calculation will be positive since our transition for going top is decrement
                    if (fromTop)
                    {
                        // if the roll transition is not equal to or greater than 0
                        if (rollTransition >= 0f)
                        {
                            rollTransition = 0f;
                            return;
                        }

                        // then keep incrementing the value
                        rollTransition += speed * Time.deltaTime;
                    }
                    // the calculation will be negative since the transtion is increment
                    else
                    {
                        // if the roll transition is not eqaul to or less than 0
                        if (rollTransition <= 0f)
                        {
                            rollTransition = 0f;
                            return;
                        }

                        // then keep decrementing the value
                        rollTransition -= speed * Time.deltaTime;
                    }

                }

                // assign the rotation to character in degress value
                character.eulerAngles = new Vector3(character.eulerAngles.x, character.eulerAngles.y, rollTransition);
            }
        }

        private void LateUpdate()
        {

            // previous position of character
            prevPosY = character.position.y;

        }

    }
}
