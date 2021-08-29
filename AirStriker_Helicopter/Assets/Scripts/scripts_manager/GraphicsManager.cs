using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to graphics manager
/// handles the game graphics if low graphics settings or high graphics settings
/// </summary>

namespace game_ideas
{
    public enum GameGraphics
    {
        LOW_GRAPHICS,
        HIGH_GRAPHICS
    }

    public class GraphicsManager : MonoBehaviour
    {
        // reference for game graphics
        private GameGraphics gameGraphics;

        // set game controls
        public void SetGameGraphics(GameGraphics value)
        {
            gameGraphics = value;
        }

        // get game graphics
        public GameGraphics GetGameGraphics()
        {
            return gameGraphics;
        }
    }
}
