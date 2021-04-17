using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{

    public enum PlayerAnimatorParameters
    {
        player_on_hit,
        player_health
    }

    public class PlayerAnimator : MonoBehaviour
    {

        [SerializeField] private Animator hitEffectAnimator = null;

        [Header("Script Reference")]
        [SerializeField] private PlayerManager playerManager = null;

        private void Start()
        {
            hitEffectAnimator.SetInteger(PlayerAnimatorParameters.player_health.ToString(), playerManager.health);
        }

        public void OnPlayerHit(int remainingPlayerHealth)
        {
            hitEffectAnimator.SetTrigger(PlayerAnimatorParameters.player_on_hit.ToString());
            hitEffectAnimator.SetInteger(PlayerAnimatorParameters.player_health.ToString(), remainingPlayerHealth);
        }

    }
}
