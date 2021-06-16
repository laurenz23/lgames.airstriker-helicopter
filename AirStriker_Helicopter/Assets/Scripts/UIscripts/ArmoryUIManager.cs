using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached this script to armoryUIManager object or create one if doesn't exist
/// function: manages the ui of armory and events 
/// </summary>

namespace game_ideas
{
    public class ArmoryUIManager : MonoBehaviour
    {

        [Header("Script Reference")]
        [SerializeField] private SoundManager soundManager;

        public void Research()
        {
            soundManager.soundFXHandler.SFX_ARMORY("armory2");
        }

        public void Upgrade()
        {
            soundManager.soundFXHandler.SFX_ARMORY("armory1");
        }

    }
}
