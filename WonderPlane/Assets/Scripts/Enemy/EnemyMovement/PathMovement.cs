using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to object as a child
/// it will move according to the path objects positions
/// NOTE:   if using this script attached the path objects
///         or use Path Movement Distributer script (READ SUMMARY SCRIPT)
/// </summary>

namespace game_ideas
{
    public class PathMovement : MonoBehaviour
    {

        [SerializeField] private Transform character; // attached here the object that going to move 
        [SerializeField] private float speed;

        [Header("If Apply Rotation")]
        [SerializeField] private bool applyRotation;
        [SerializeField] private float maxRotation;
        [SerializeField] private float rotationSpeed;

        public List<Transform> pathObjects = new List<Transform>();

        private GameManager gameManager;

        private int ListIndex = 0; // index value of path objects
        private float rotationZ = 1f;
        private float characterPrevPos;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {

            // reference for character prev position
            characterPrevPos = character.position.y;

            if (pathObjects.Count.Equals(0))
            {
#if UNITY_EDITOR
                Debug.LogError("Path Movement: Please attached path objects or use Path Movement Distributor");
#endif
                enabled = false; // disabled this script if no path objects attached
            }

        }

        private void Update()
        {

            if (gameManager.gameState.Equals(GameState.GAME_START) || gameManager.gameState.Equals(GameState.GAME_CONTINUE))
            {

                // if path objects is not equal or greater than to list index value
                // then move to next position of path object list. If the list index value is already greater than to path object list
                // then keep the character moving forward 
                if (ListIndex < pathObjects.Count)
                {

                    character.position = Vector3.MoveTowards(character.position, pathObjects[ListIndex].position, speed * Time.deltaTime);

                    // if character is able to rotate base on path position
                    // if path position is greater than to character prev position, rotate the character from Z axis by negative
                    // if path position is less than to character prev position, rotate the character from Z axis by positive
                    if (applyRotation)
                    {

                        // if character prev position is greater than to path object position
                        // rotate from Z axis by positive
                        if (characterPrevPos > pathObjects[ListIndex].position.y)
                        {
                            // return the max rotation positive if it gives negative values
                            maxRotation = Mathf.Abs(maxRotation);

                            // rotate from z axis while rotationz is less than to max rotation
                            if (rotationZ < maxRotation)
                            {
                                rotationZ += rotationSpeed * Time.deltaTime;
                            }
                        }

                        // if character prev position is less than to path object position
                        // rotate from Z axis by negative
                        if (characterPrevPos < pathObjects[ListIndex].position.y)
                        {
                            // set the max rotation to negative, since were going to rotate it in negative value
                            maxRotation = -maxRotation;

                            // rotate from z axis while rotationz is less than to max rotation (Remember that max rotatoin is already a negative value)
                            if (rotationZ > maxRotation)
                            {
                                rotationZ -= rotationSpeed * Time.deltaTime;
                            }
                        }
                        
                        // rotate the character
                        character.rotation = Quaternion.Euler(character.eulerAngles.x, character.eulerAngles.y, rotationZ);
                    }

                    // if the position of character is same to path objects 
                    // then proceed to the next path object by incrementing the index of list by 1
                    if (character.position == pathObjects[ListIndex].position)
                    {
                        characterPrevPos = pathObjects[ListIndex].position.y;
                        // increment the value of list index if position value is equal to paths objects
                        // keep increment until the list index is greater than to path object list
                        // to end disable this script
                        ListIndex++;
                    }

                }
                else
                {
                    // move the character forward
                    character.Translate(Vector3.forward * speed * Time.deltaTime);
                }

            }

        }

    }
}
