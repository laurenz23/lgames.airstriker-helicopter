using UnityEngine;
using System.Collections;

// this script is attached to enemy manager itself
// object will back and forth to specific area
namespace game_ideas
{
    public class PatrolMovement : MonoBehaviour
    {

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



            }

        }

    }
}
