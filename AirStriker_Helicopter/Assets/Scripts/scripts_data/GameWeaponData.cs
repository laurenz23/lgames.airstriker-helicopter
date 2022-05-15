using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage:  scriptableObject that stores game weapon datas
/// </summary>
namespace game_ideas
{
    [CreateAssetMenu(fileName = "New Game Weapon Data", menuName = "Project/Game Weapon Data")]
    [System.Serializable]
    public class GameWeaponData : ScriptableObject
    {
        
        public string id;

        public AttackType type;

        public string _name;

        public Sprite icon;

        [TextArea]
        public string description;

        public WeaponData[] weaponData;

    }
}
