using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: call this script to MusicHandler or SoundFXHandler
/// goal: in order to manipulate sound data easily
/// </summary>

namespace game_ideas
{
    [System.Serializable]
    public class SoundData
    {
        public string soundName;

        public AudioClip audioClip;

        public bool mute;

        public bool loop;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;

        [HideInInspector]
        public AudioSource audioSource;
    }
}
