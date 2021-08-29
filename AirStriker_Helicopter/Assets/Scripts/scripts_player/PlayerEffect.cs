using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the effect of player
/// </summary>

namespace game_ideas
{
    public class PlayerEffect : MonoBehaviour
    { 
        [HideInInspector] public GameObject popupText_health;
        [HideInInspector] public GameObject popupText_damage;
        [HideInInspector] public GameObject popupText_points;
        [HideInInspector] public GameObject popupText_coins;
        [HideInInspector] public GameObject popupText_energy;

        private EffectPrefabManager effectPrefabManager;

        private void Start()
        {

            effectPrefabManager = FindObjectOfType<EffectPrefabManager>();

            // the following lines is referenced at player collider handler
            popupText_health = effectPrefabManager.popupText_health;
            popupText_damage = effectPrefabManager.popupText_damage;
            popupText_points = effectPrefabManager.popupText_points;
            popupText_coins = effectPrefabManager.popupText_coins;
            popupText_energy = effectPrefabManager.popupText_energy;

        }

        // create popup text prefab
        public void PlayerDisplayPopupText(Transform other, string poolName, string value)
        {
            effectPrefabManager.DisplayPopupText(other, poolName, value);
        }


        // create coin effect when collected 
        public void PlayerEffectOnCoinsCollect(Transform coinsPos)
        {

            // create the coins particle effect and destroy effect after a couple of seconds
            effectPrefabManager.PoolEffect("effectCoins", Quaternion.identity,
                new Vector3(10f, coinsPos.position.y + 2.5f, coinsPos.position.z), new Vector3(1f, 1f, 1f));
            
        }
        
        // create explosion effect for player explosion
        public void PlayerEffectExplosion(Transform playerTransform)
        {

            // create the effect prefab and destroy effect after a set time
            effectPrefabManager.PoolExplosion("explosionCharacter", Quaternion.identity,
                new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z), new Vector3(1f, 1f, 1f));

        }
    }
}
