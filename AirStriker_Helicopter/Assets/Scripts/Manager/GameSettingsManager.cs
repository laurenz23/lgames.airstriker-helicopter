using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to GameSettingsManager object
/// get the gameSettingsData ScriptableObject that stores the settings of game
/// can be use get and set
/// instead of attaching the game settings data to each object
/// we manage it by this script for easy script modification
/// </summary>

namespace game_ideas
{
    public class GameSettingsManager : MonoBehaviour
    {

        private static GameSettingsManager instance;

        public static GameSettingsManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        [SerializeField] private GameSettingsData data;

        public GameSettingsData GetData()
        {
            return data;
        }

    }
}
