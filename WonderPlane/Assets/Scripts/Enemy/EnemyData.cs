using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public EnemyType enemyType;
        public int health;
        public int points;
       
    }
}
