using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: create a scriptable objects and fill up attack data
/// goal: handles the data of attacks or armaments as scriptable objects and later pass through armament attack data script
/// </summary>

namespace game_ideas
{

    public enum AttackType
    {
        NONE,
        BULLET,
        MISSILE,
        BOMB,
        ATOMIC,
        LASER
    }
    
    [CreateAssetMenu(fileName = "New Attack Data", menuName = "Project/Attack Data")]
    public class AttackData : ScriptableObject
    {

        public AttackType attackType;
        public int attackLevel;
        public int damage;
        public float speed;
        public float aoe;
        public string explosionPoolName;

        [Header("Optional")]
        public string muzzleFlashPoolName;
        public Vector3 muzzleFlashScale = new Vector3(1f, 1f, 1f);

    }

}
