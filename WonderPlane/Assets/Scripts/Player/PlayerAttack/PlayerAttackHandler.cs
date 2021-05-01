using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to player gameobject itself
/// handles every player attacks
/// </summary>

namespace game_ideas
{

    public class PlayerAttackHandler : MonoBehaviour
    {

        // player attack abilities
        [Header("Player can use armaments:")]
        public bool gatling_armament;
        public bool missile_armament;
        public bool dropMissile_armament;
        public bool guidedMissile_armament;
        public bool laser_armament;
        public bool automic_armament;

        // game object references
        [Header("Player Armament Objects")]
        public GameObject gatlingArmament;
        public GameObject missileArmament;
        public GameObject dropMissileArmament;
        public GameObject guidedMissileArmament;
        public GameObject laserArmament;
        public GameObject automicArmament;
        public GameObject mainWing;
        
        private PlayerGatlingGun playerGatlingGun;
        private PlayerMissile playerMissile;
        private PlayerDropMissile playerDropMissile;
        private PlayerGuidedMissile playerGuidedMissile;
        private PlayerAutomic playerAutomic;

        private void Awake()
        {
            playerGatlingGun = GetComponentInChildren<PlayerGatlingGun>();
            playerMissile = GetComponentInChildren<PlayerMissile>();
            playerDropMissile = GetComponentInChildren<PlayerDropMissile>();
            playerGuidedMissile = GetComponentInChildren<PlayerGuidedMissile>();
            playerAutomic = GetComponentInChildren<PlayerAutomic>();
        }

        private void Start()
        {

            // show the object armaments if attack is enabled and hide is enabled
            if (gatling_armament)
            {
                gatlingArmament.SetActive(true);
                playerGatlingGun.enabled = true;
            }
            else
            {
                gatlingArmament.SetActive(false);
                playerGatlingGun.enabled = false;
            }

            if (missile_armament)
            {
                missileArmament.SetActive(true);
                playerMissile.enabled = true;
            }
            else
            {
                missileArmament.SetActive(false);
                playerMissile.enabled = false;
            }

            if (dropMissile_armament)
            {
                dropMissileArmament.SetActive(true);
                playerDropMissile.enabled = true;
            }
            else
            {
                dropMissileArmament.SetActive(false);
                playerDropMissile.enabled = false;
            }

            if (guidedMissile_armament)
            {
                guidedMissileArmament.SetActive(true);
                playerGuidedMissile.enabled = true;
            }
            else
            {
                guidedMissileArmament.SetActive(false);
                playerGuidedMissile.enabled = false;
            }

            if (laser_armament)
            {
                laserArmament.SetActive(true);
            }
            else
            {
                laserArmament.SetActive(false);
            }

            if (automic_armament)
            {
                automicArmament.SetActive(true);
                playerAutomic.enabled = true;
            }
            else
            {
                automicArmament.SetActive(false);
                playerAutomic.enabled = false;
            }

            if (missile_armament || guidedMissile_armament)
            {
                mainWing.SetActive(true);
            }
            else
            {
                mainWing.SetActive(false);
            }
            
        }

        public void Attack(Transform playerTransform)
        {
            //check if player can do the attacks
            if (gatling_armament)
                playerGatlingGun.GatlingAttack(playerTransform);

            if (missile_armament)
                playerMissile.MissileAttack(playerTransform);

            if (dropMissile_armament)
                playerDropMissile.DropMissileAttack(playerTransform);

            if (guidedMissile_armament)
                playerGuidedMissile.GuidedMissileAttack(playerTransform);

        }

        public void AutomicAttack(Transform playerTransform)
        {
            //check if player can do the attacks
            if (automic_armament)
                playerAutomic.AutomicAttack(playerTransform);
        }
        

    }
}
