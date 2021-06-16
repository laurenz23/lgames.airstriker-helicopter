using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles enemy data 
/// </summary>

namespace game_ideas
{

    public enum EnemyType
    {
        Plane,
        Helicopter,
        Artillery,
        Tank,
        Battleship,
        Mini_Boss,
        Boss
    }

    [System.Serializable]
    public class EnemyData
    {
        [Header("Enemy Data")]
        public string characterName;

        public EnemyType enemyType;

        public int level;

        public int health;

        public int points;

        [Header("If character can move")]
        public float movementSpeed;

        [Header("Enemy Death Info (If null use default explosion)")]
        public GameObject deathExplosion;

        public string deathSFXName;
    }
}