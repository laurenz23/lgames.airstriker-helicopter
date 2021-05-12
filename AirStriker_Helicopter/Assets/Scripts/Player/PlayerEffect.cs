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

        private EffectHandler effectHandler;

        private void Start()
        {

            effectHandler = FindObjectOfType<EffectHandler>();

            // the following lines is referenced at player collider handler
            popupText_health = effectHandler.popupText_health;
            popupText_damage = effectHandler.popupText_damage;
            popupText_points = effectHandler.popupText_points;
            popupText_coins = effectHandler.popupText_coins;
            popupText_energy = effectHandler.popupText_energy;

        }

        // create popup text prefab
        public void PlayerDisplayPopupText(Transform other, GameObject popupText_prefab, string value)
        {
            effectHandler.DisplayPopupText(other, popupText_prefab, value);
        }


        // create coin effect when collected 
        public void PlayerEffectOnCoinsCollect(Transform coinsPos)
        {

            // create the coins particle effect and destroy effect after a couple of seconds
            effectHandler.CreatePrefabEffectAndDestroy(effectHandler.coinParticleEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                new Vector3(10f, coinsPos.position.y + 2.5f, coinsPos.position.z), 2f);
            
        }

        // create energy particle effect
        public void PlayerEffectOnEnergyCollect(Transform energyPos)
        {

            // create the energy particle effect and destroy effect after a set time seconds
            effectHandler.CreatePrefabEffectAndDestroy(effectHandler.energyParticleEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                new Vector3(10f, energyPos.position.y + 2.5f, energyPos.position.z), 2f);

        }

        // create explosion effect for plane explosion
        public void PlayerEffectExplosion(Transform playerTransform)
        {

            // create the effect prefab and destroy effect after a set time
            effectHandler.CreatePrefabEffectAndDestroy(effectHandler.largeExplosionEffect, effectHandler.transform, new Vector3(1f, 1f, 1f), Quaternion.identity,
                new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z), 5f);

        }
    }
}
