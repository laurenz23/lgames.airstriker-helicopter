using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// object moves towards to player and crashing
namespace game_ideas
{
    public class CrashMovement : MonoBehaviour
    {
        public Transform character;

        public float speed;

        public float maximumPitch;

        [HideInInspector]
        public GameManager gameManager;

        [HideInInspector]
        public Transform targetPlayer;

        private void Awake()
        {

            gameManager = GameManager.GetInstance();

            if (FindObjectOfType<PlayerManager>())
            {
                targetPlayer = FindObjectOfType<PlayerManager>().transform;
            }

        }

        private void Update()
        {
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                if (targetPlayer != null)
                {
                    Vector3 direction = character.position - targetPlayer.position;
                    float anglePitch = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;

                    if (anglePitch >= -maximumPitch && anglePitch <= maximumPitch)
                    {

                        character.rotation = Quaternion.Euler(anglePitch, 180f, 0f);

                    }

                    character.Translate(Vector3.forward * speed * Time.deltaTime);
                }

            }
        }

    }
}
