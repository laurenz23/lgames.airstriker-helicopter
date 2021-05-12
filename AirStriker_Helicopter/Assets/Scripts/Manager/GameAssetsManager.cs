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

        public Material defaultMaterial;

        public Material hitMaterial;
    }
}
