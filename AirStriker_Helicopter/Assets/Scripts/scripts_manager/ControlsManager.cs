using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is script is attached to control manager object
/// handles the game control ui representation for player 
/// </summary>

namespace game_ideas
{
    public enum GameControls
    {
        JOYSTICK_CONTROLS,
        BUTTON_CONTROLS
    }

    public class ControlsManager : MonoBehaviour
    {

        // reference for game controls
        private GameControls gameControls;

        // set game controls
        public void SetGameControls(GameControls value)
        {
            gameControls = value;
        }

        // get game controls
        public GameControls GetGameControls()
        {
            return gameControls;
        }

    }
}
