using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script handles red screen effect when player is hit
/// </summary>

namespace game_ideas
{
    public enum PlayerAnimatorParameters
    {
        player_on_hit,
        player_health
    }

    public class PlayerAnimator : MonoBehaviour
    {

        [SerializeField] private PlayerManager playerManager = null;
        

        private HitEffectAnimator hitEffectAnimator;

        private Animator hitEffect_animator = null;

        private void Start()
        {
            hitEffectAnimator = FindObjectOfType<HitEffectAnimator>();

            hitEffect_animator = hitEffectAnimator.GetComponent<Animator>();

            // set the integer health value of the animator
            // animation effect is change base on player current health
            hitEffect_animator.SetInteger(PlayerAnimatorParameters.player_health.ToString(), playerManager.health);
        }

        public void OnPlayerHit(int remainingPlayerHealth)
        {
            hitEffect_animator.SetTrigger(PlayerAnimatorParameters.player_on_hit.ToString()); // trigger the hit screen effect
            hitEffect_animator.SetInteger(PlayerAnimatorParameters.player_health.ToString(), remainingPlayerHealth); // change the animation effect base on remaining health
        }

    }
}
