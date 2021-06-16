using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the data of game settings
/// </summary>

namespace game_ideas
{
    [CreateAssetMenu(fileName = "New Game Settings Data", menuName = "Project/Game Settings Data")]
    public class GameSettingsData : ScriptableObject
    {

        public GameGraphics gameGraphics;
        public GameInUIStyle gameInUIStyle;
        public GameControls gameControls;
        public bool music;
        public bool soundFX;

    }
}
