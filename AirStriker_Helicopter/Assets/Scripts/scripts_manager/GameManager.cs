using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to Game Manager object
/// This class Handles the status of Game
/// Game is Start?, Game is Pause?, Game is Over?, Game is Pause?, Game is Continue?, Game is Finish?
/// </summary>

namespace game_ideas
{
    public enum GameState
    {
        MAINMENU,
        WAITING_TO_START,
        GAME_START,
        GAME_PAUSE,
        GAME_CONTINUE,
        LEVEL_COMPLETE,
        GAMEOVER,
        GAME_FINISH
    }

    public enum GameObjective
    {
        HEALTH,
        COINS,
        ENERGY
    }

    public enum GameTag
    {
        Player,
        Obstacle,
        Ground,
        Terrain,
        GameBoundary,
        BasicAttack,
        Enemy,
        Cloud,
        Tree,
        Props,
        EnemyAttack,
        Coins
    }

    public enum GameLayers
    {
        Character
    }

    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            Application.targetFrameRate = 120;
        }

        public float horizontalForwardSpeed; // speed of game moving forward

        public GameState gameState;

        public string musicTheme;

        [Header("Script Reference")]
        [SerializeField] private SoundManager soundManager;
        
        private void Start()
        {
            if (gameState == GameState.MAINMENU)
            {
                soundManager.musicHandler.MUSIC_MAINMENU(musicTheme);
            }
            else if (gameState == GameState.WAITING_TO_START)
            {
                soundManager.musicHandler.MUSIC_INGAME(musicTheme);
            }
        }

        private void Update()
        {

            switch (gameState)
            {

                case GameState.GAME_PAUSE:
                    Time.timeScale = 0f;
                    break;
                case GameState.GAME_CONTINUE:
                    Time.timeScale = 1f;
                    break;

            }
            
        }
    } 
}
