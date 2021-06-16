using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to guided attack target finder object, child of main guided attack object
/// find a target character(player)
/// </summary>

namespace game_ideas
{
    public class EnemyGuidedFindTarget : MonoBehaviour
    {

        public EnemyAttackGuided guidedAttack;

        private void OnTriggerStay(Collider other)
        {
            if (guidedAttack.target.Equals(null))
            {
                if (other.CompareTag(GameTag.Player.ToString()))
                {
                    guidedAttack.target = other.transform;
                }
            }
        }

    }
}
