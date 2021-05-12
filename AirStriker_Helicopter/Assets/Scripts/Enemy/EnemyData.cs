using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the data of enemmy as scriptable objects
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

        [Header("Enemy Attack Info (If character can attack)")]
        public float firerate;

        public GameObject attackPrefab;

        [Header("Enemy Death Info (If null use default explosion)")]
        public GameObject deathExplosion;
       
    }
}