using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

namespace game_ideas
{
    public class CharacterSelectionManager : MonoBehaviour
    {
        [Header("Script Reference")]
        [SerializeField] private SoundManager soundManager;

        public void NextCharacter()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click1");
        }

        public void PrevCharacter()
        {
            soundManager.soundFXHandler.SFX_UI_CLICK("click1");
        }
    }
}
