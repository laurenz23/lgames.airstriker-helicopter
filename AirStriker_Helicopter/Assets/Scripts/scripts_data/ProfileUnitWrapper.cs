using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    /// <summary>
    /// list of profileUnitData for serialized/deserialized json
    /// </summary>
    [System.Serializable]
    public class ProfileUnitWrapper
    {

        public List<ProfileUnitData> unitData = new List<ProfileUnitData>();

    }

    /// <summary>
    /// profileUnitData fields for serialized/deserialized json
    /// </summary>
    [System.Serializable]
    public class ProfileUnitData
    {

        public string unitID;

        public List<ProfileWeaponData> weaponData = new List<ProfileWeaponData>();

    }
}
