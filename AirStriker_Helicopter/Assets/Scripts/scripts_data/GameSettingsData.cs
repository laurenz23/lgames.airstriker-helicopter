using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the data of game settings
/// game data fields for serialized/derialized json 
/// </summary>

namespace game_ideas
{
    [System.Serializable]
    public class GameSettingsData
    {

        public string gameGraphics;
        public string gameInUIStyle;
        public string gameControls;
        public bool music;
        public bool soundFX;

        public GameSettingsData(string gameGraphics, string gameInUIStyle, string gameControls, bool music, bool soundFX)
        {
            this.gameGraphics = gameGraphics;
            this.gameInUIStyle = gameInUIStyle;
            this.gameControls = gameControls;
            this.music = music;
            this.soundFX = soundFX;
        }

        public GameSettingsData()
        {

        }

    }
}
