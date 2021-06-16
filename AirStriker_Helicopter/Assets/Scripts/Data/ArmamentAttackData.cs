using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached this script to armament object
/// goal: global function to use for all characters in order to get data from attack data scriptable objects
/// </summary>

namespace game_ideas
{
    public class ArmamentAttackData : MonoBehaviour
    {
        [HideInInspector]
        public AttackType attackType;

        [HideInInspector]
        public int damage;

        [HideInInspector]
        public float speed;

        [HideInInspector]
        public float aoe;
    }
}
