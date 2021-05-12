using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the data of attacks or armaments as scriptable objects
/// </summary>

namespace game_ideas
{

    public enum AttackType
    {
        NONE,
        BULLET,
        MISSILE,
        BOMB,
        AUTOMIC
    }
    
    [CreateAssetMenu(fileName = "New Attack Data", menuName = "Project/Attack Data")]
    public class AttackData : ScriptableObject
    {

        public AttackType attackType;
        public int attackLevel;
        public int damage;
        public float speed;
        public float aoe;

    }

}
