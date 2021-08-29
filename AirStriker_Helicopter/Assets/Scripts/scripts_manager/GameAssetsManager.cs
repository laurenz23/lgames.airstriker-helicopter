using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attacted to GameAssetsManager object at the hierarchy
 * This script handle the assets of the game
*/
namespace game_ideas
{
    public class GameAssetsManager : MonoBehaviour
    {
        private static GameAssetsManager instance;

        public static GameAssetsManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }
        
        [Header("Material Assets")]
        public Material defaultMaterial;
        public Material hitMaterial;

        // sprite assets
        [Header("Sprite Assets")]
        public Sprite coin_icon;
        public Sprite supply_icon;
        public Sprite energy_capsule_icon;
        public Sprite more_coin_icon;
        public Sprite more_supply_icon;

    }
}
