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

        private GameSettingsManager gameSettingsManager;

        private void Awake()
        {
            gameSettingsManager = GameSettingsManager.GetInstance();
        }

        // set music if enabled or disabled
        public void SetMusic()
        {
            music_enabled = gameSettingsManager.GetData().music;

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
            soundFX_enabled = gameSettingsManager.GetData().soundFX;

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
