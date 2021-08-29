using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage:  scriptableObject that stores game unit (helicopter) data 
/// </summary>
namespace game_ideas
{
    [CreateAssetMenu(fileName = "New Game Unit Data", menuName = "Project/Game Unit Data")]
    [System.Serializable]
    public class GameUnitData : ScriptableObject
    {

        public string id;

        public string level;

        public string _name;
        
        [TextArea]
        public string description;

        public int health;

        public int speed;
        
        public int dmgS;

        public List<UnitPassiveData> listPassiveData;

        public int diamondCosts;

        public int coinCosts;

        public List<GameWeaponData> listGameWeaponData;

    }
}
