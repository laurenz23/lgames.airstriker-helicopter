using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached this class to boss 1 itself as a parent
/// goal: control boss 1 animator and etc.
/// </summary>

namespace game_ideas
{
    public class Boss1Handler : MonoBehaviour
    {
        private Animator animator;

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = GameManager.GetInstance();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            animator.enabled = false;
        }

        private void Update()
        {
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                animator.enabled = true;
            }
            else
            {
                animator.enabled = false;
            }
        }
    }
}
