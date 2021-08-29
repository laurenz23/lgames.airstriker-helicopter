using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player selected data for serialization and deserialization of json
/// </summary>
namespace game_ideas
{
    [System.Serializable]
    public class ProfileSelectedData
    {

        public int selectedUnit;

        public int selectedStageLevel;

    }
}
