using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to object sound Manager
/// handles the music and sound fx of the the game
/// </summary>

namespace game_ideas
{
    public class SoundManager : MonoBehaviour
    {
        // music reference
        public MusicHandler musicHandler;
        private bool music_enabled;

        // soundFX reference
        public SoundFXHandler soundFXHandler;
        private bool soundFX_enabled;

        private DataManager dataManager;

        private GameSettingsData gameSettingsData = new GameSettingsData();

        private static SoundManager instance;

        public static SoundManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            dataManager = DataManager.GetInstance();
        }

        private void Start()
        {
            gameSettingsData = dataManager.gameSettingsData;
            SetMusic();
            SetSoundFX();
        }

        // set music if enabled or disabled
        public void SetMusic()
        {
            music_enabled = gameSettingsData.music;

            AudioSource[] audioSourceList = musicHandler.transform.GetComponents<AudioSource>();

            if (music_enabled)
            {
                foreach (AudioSource a in audioSourceList)
                {
                    a.mute = false;
                }
            }
            else
            {
                foreach (AudioSource a in audioSourceList)
                {
                    a.mute = true;
                }
            }
        }

        // set soundFX if enabled or disabled
        public void SetSoundFX()
        {
            soundFX_enabled = gameSettingsData.soundFX;

            AudioSource[] audioSourceList = soundFXHandler.transform.GetComponents<AudioSource>();

            if (soundFX_enabled)
            {
                foreach (AudioSource a in audioSourceList)
                {
                    a.mute = false;
                }
            }
            else
            {
                foreach (AudioSource a in audioSourceList)
                {
                    a.mute = true;
                }
            }
        }
    }
}
