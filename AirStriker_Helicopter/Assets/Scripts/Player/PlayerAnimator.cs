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

        [Tooltip("Attached HitEffect object at GameManager -> GameUI-> HitEffect")]
        [SerializeField] private Animator hitEffectAnimator = null; // please attached here the hit effect animator

        [SerializeField] private PlayerManager playerManager = null;

        private void Start()
        {

#if UNITY_EDITOR
            if (hitEffectAnimator.Equals(null))
            {
                Debug.LogError("Player Animator Error: Please attached HitEffect object at GameManager -> GameUI-> HitEffect");
            }
#endif
            // set the integer health value of the animator
            // animation effect is change base on player current health
            hitEffectAnimator.SetInteger(PlayerAnimatorParameters.player_health.ToString(), playerManager.health);
        }

        public void OnPlayerHit(int remainingPlayerHealth)
        {
            hitEffectAnimator.SetTrigger(PlayerAnimatorParameters.player_on_hit.ToString()); // trigger the hit screen effect
            hitEffectAnimator.SetInteger(PlayerAnimatorParameters.player_health.ToString(), remainingPlayerHealth); // change the animation effect base on remaining health
        }

    }
}
