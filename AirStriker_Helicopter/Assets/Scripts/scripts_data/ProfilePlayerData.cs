using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player data to serialized/deserialized json
/// </summary>
namespace game_ideas
{
    [System.Serializable]
    public class ProfilePlayerData
    {

        public string playerName;

        public int playerLevel;

        public int playerStage;

        public int playerStageLevel;

    }
}
