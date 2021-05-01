using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the data of attacks or armaments
/// </summary>

namespace game_ideas
{

    public enum ArmamentType
    {
        NONE,
        BULLET,
        MISSILE,
        BOMB,
        AUTOMIC
    }

    [System.Serializable]
    public class ArmamentData
    {

        public ArmamentType armamentType;
        public int damage;
        public float speed;
        public float aoe;

    }

}
