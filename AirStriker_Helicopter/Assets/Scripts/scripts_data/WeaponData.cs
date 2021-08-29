using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// game weapon data to serialized/deserialized json data
/// </summary>
namespace game_ideas
{
    [System.Serializable]
    public class WeaponData
    {

        public int level;

        public Sprite icon;

        public int damage;

        public int firerate;

        public int speed;

        public int aoe;

        public int cost;

    }
}
