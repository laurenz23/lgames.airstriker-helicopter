using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// usage: attached this script to soundFx object or soundManager child object
/// enter sound data info for sound fx
/// goal: handle the sound fx of game for easy access and modification if changes are made
/// can manipulate here the pitch, volume and etc. for a certain sound fx
/// </summary>

namespace game_ideas
{
    public class SoundFXHandler : MonoBehaviour
    {
        
        [SerializeField] private SoundData[] sfx_alert_warning;
        
        [SerializeField] private SoundData[] sfx_armory;

        [SerializeField] private SoundData[] sfx_bomb_drop;
        
        [SerializeField] private SoundData[] sfx_collect_coin;
        
        [SerializeField] private SoundData[] sfx_explode_big;
        
        [SerializeField] private SoundData[] sfx_explode_sciFi;
        
        [SerializeField] private SoundData[] sfx_explode;
        
        [SerializeField] private SoundData[] sfx_hit_metal;
        
        [SerializeField] private SoundData[] sfx_shoot_laser;
        
        [SerializeField] private SoundData[] sfx_shoot_missile;
        
        [SerializeField] private SoundData[] sfx_shoot;
        
        [SerializeField] private SoundData[] sfx_ui_click;
        
        [SerializeField] private SoundData[] sfx_ui_transition;
        
        [SerializeField] private SoundData[] sfx_other;

        private void Awake()
        {

            SoundDataDistribute(sfx_alert_warning);

            SoundDataDistribute(sfx_armory);

            SoundDataDistribute(sfx_bomb_drop);

            SoundDataDistribute(sfx_collect_coin);

            SoundDataDistribute(sfx_explode_big);

            SoundDataDistribute(sfx_explode_sciFi);

            SoundDataDistribute(sfx_explode);

            SoundDataDistribute(sfx_hit_metal);

            SoundDataDistribute(sfx_shoot_laser);

            SoundDataDistribute(sfx_shoot_missile);

            SoundDataDistribute(sfx_shoot);

            SoundDataDistribute(sfx_ui_click);

            SoundDataDistribute(sfx_ui_transition);
            
            SoundDataDistribute(sfx_other);

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
                Debug.LogWarning("SoundFX: " + name + " sound not found.");
#endif
                return;
            }

            s.audioSource.PlayOneShot(s.audioClip);
        }
        
        public void SFX_ALERT_WARNING(string name)
        {
            PlaySound(sfx_alert_warning, name);
        }

        public void SFX_ARMORY(string name)
        {
            PlaySound(sfx_armory, name);
        }

        public void SFX_BOMB_DROP(string name)
        {
            PlaySound(sfx_bomb_drop, name);
        }

        public void SFX_COLLECT_COIN(string name)
        {
            PlaySound(sfx_collect_coin, name);
        }

        public void SFX_EXPLODE_BIG(string name)
        {
            PlaySound(sfx_explode_big, name);
        }

        public void SFX_EXPLODE_SCIFI(string name)
        {
            PlaySound(sfx_explode_sciFi, name);
        }

        public void SFX_EXPLODE(string name)
        {
            PlaySound(sfx_explode, name);
        }

        public void SFX_HIT_METAL(string name)
        {
            PlaySound(sfx_hit_metal, name);
        }

        public void SFX_SHOOT_LASER(string name)
        {
            PlaySound(sfx_shoot_laser, name);
        }

        public void SFX_SHOOT_MISSILE(string name)
        {
            PlaySound(sfx_shoot_missile, name);
        }
             
        public void SFX_SHOOT(string name)
        {
            PlaySound(sfx_shoot, name);
        }

        public void SFX_UI_CLICK(string name)
        {
            PlaySound(sfx_ui_click, name);
        }

        public void SFX_UI_TRANSITION(string name)
        {
            PlaySound(sfx_ui_transition, name);
        }

        public void SFX_OTHER(string name)
        {
            PlaySound(sfx_other, name);
        }

    }
}
