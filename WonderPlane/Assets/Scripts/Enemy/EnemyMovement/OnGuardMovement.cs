using UnityEngine;
using System.Collections;

// object moves while following the player with specific direction to follow
namespace game_ideas
{
    public class OnGuardMovement : MonoBehaviour
    {

        public Transform character;

        public float defaultSpeed;

        public float advancedPos;

        public bool directionVertical;

        public bool directionHorizontal;

        [HideInInspector]
        public GameManager gameManager;

        [HideInInspector]
        public Transform targetPlayer;


        private float speed;

        private float posY;

        private float posZ;


        public void Awake()
        {

            gameManager = GameManager.GetInstance();

            if (FindObjectOfType<PlayerManager>())
            {
                targetPlayer = FindObjectOfType<PlayerManager>().transform;
            }

        }

        private void LateUpdate()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {

                if (targetPlayer != null)
                {
                    if (directionVertical)
                    {
                        posY = targetPlayer.position.y;
                        speed = defaultSpeed;
                    }
                    else
                    {
                        posY = character.position.y;
                        speed = defaultSpeed;
                    }

                    if (directionHorizontal)
                    {
                        posZ = targetPlayer.position.z + advancedPos;
                        speed = defaultSpeed * 2;
                    }
                    else
                    {
                        posZ = character.position.z + advancedPos;
                        speed = defaultSpeed;
                    }

                    character.position = Vector3.MoveTowards(character.position, new Vector3(0f, posY, posZ), speed * Time.deltaTime);
                }
                
            }

        }

    }
}
