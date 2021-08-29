using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// usage: attached this script to music object or soundManager child object
/// enter sound data info for music
/// goal: handles the music of game for easy acces and manipulation or modification if changes are made
/// can manipulate here the pitch, volume and etc. for a certain music
/// </summary>

namespace game_ideas
{
    public class MusicHandler : MonoBehaviour
    {
        
        [SerializeField] private SoundData[] music_mainMenu;
        
        [SerializeField] private SoundData[] music_inGame;
        
        [SerializeField] private SoundData[] music_boss;

        private void Awake()
        {
            SoundDataDistribute(music_mainMenu);

            SoundDataDistribute(music_inGame);

            SoundDataDistribute(music_boss);
        }

        private void SoundDataDistribute(SoundData[] soundDataArray)
        {
            foreach (SoundData s in soundDataArray)
            {
                s.audioSource = gameObject.AddComponent<AudioSource>();
                s.audioSource.clip = s.audioClip;
                s.audioSource.mute = s.mute;
                s.audioSource.loop = s.loop;
                s.audioSource.volume = s.volume;
                s.audioSource.pitch = s.pitch;
            }
        }

        private void PlaySound(SoundData[] soundDataArray, string name)
        {
            SoundData s = Array.Find(soundDataArray, soundData => soundData.soundName == name);

            if (s == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Music: " + name + " sound not found.");
#endif 
                return;
            }

            s.audioSource.Play();
        }

        public void MUSIC_MAINMENU(string name)
        {
            PlaySound(music_mainMenu, name);
        }

        public void MUSIC_INGAME(string name)
        {
            PlaySound(music_inGame, name);
        }

        public void MUSIC_BOSS(string name)
        {
            PlaySound(music_boss, name);
        }

    }
}
