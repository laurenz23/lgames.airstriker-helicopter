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
        // game object references
        [Header("Player Armament Objects")]
        public GameObject armament1;
        public GameObject armament2;
        public GameObject armament3;
        public GameObject armament4;
        public GameObject armament5;
        public GameObject mainWing;

        [Header("Script Reference")]
        public PlayerManager playerManager;
        
        private bool attackBasic;
        private bool attackPassive1;
        private bool attackPassive2;
        private bool attackPassive3;
        private bool attackActive1;

        private PlayerAttackBasic playerAttackBasic;
        private PlayerAttackPassive1 playerAttackPassive1;
        private PlayerAttackPassive2 playerAttackPassive2;
        private PlayerAttackPassive3 playerAttackPassive3;
        private PlayerAttackActive1 playerAttackActive1;

        private void Awake()
        {
            playerAttackBasic = GetComponentInChildren<PlayerAttackBasic>();
            playerAttackPassive1 = GetComponentInChildren<PlayerAttackPassive1>();
            playerAttackPassive2 = GetComponentInChildren<PlayerAttackPassive2>();
            playerAttackPassive3 = GetComponentInChildren<PlayerAttackPassive3>();
            playerAttackActive1 = GetComponentInChildren<PlayerAttackActive1>();
        }

        private void Start()
        {
            attackBasic = playerManager.HasArmament1();
            attackPassive1 = playerManager.HasArmament2();
            attackPassive2 = playerManager.HasArmament3();
            attackPassive3 = playerManager.HasArmament4();
            attackActive1 = playerManager.HasArmament5();

            // show the object armaments if attack is enabled and hide is enabled
            if (attackBasic)
            {
                armament1.SetActive(true);
                playerAttackBasic.enabled = true;
            }
            else
            {
                armament1.SetActive(false);
                playerAttackBasic.enabled = false;
            }

            if (attackPassive1)
            {
                armament2.SetActive(true);
                playerAttackPassive1.enabled = true;
            }
            else
            {
                armament2.SetActive(false);
                playerAttackPassive1.enabled = false;
            }

            if (attackPassive2)
            {
                armament3.SetActive(true);
                playerAttackPassive2.enabled = true;
            }
            else
            {
                armament3.SetActive(false);
                playerAttackPassive2.enabled = false;
            }

            if (attackPassive3)
            {
                armament4.SetActive(true);
                playerAttackPassive3.enabled = true;
            }
            else
            {
                armament4.SetActive(false);
                playerAttackPassive3.enabled = false;
            }

            if (attackActive1)
            {
                armament5.SetActive(true);
                playerAttackActive1.enabled = true;
            }
            else
            {
                armament5.SetActive(false);
                playerAttackActive1.enabled = false;
            }

            if (attackPassive1 || attackPassive3)
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
            if (attackBasic)
                playerAttackBasic.AttackAction(playerTransform);

            if (attackPassive1)
                playerAttackPassive1.AttackAction(playerTransform);

            if (attackPassive2)
                playerAttackPassive2.AttackAction(playerTransform);

            if (attackPassive3)
                playerAttackPassive3.AttackAction(playerTransform);

        }

        public void AtomicAttack(Transform playerTransform)
        {
            //check if player can do the attacks
            if (attackActive1)
                playerAttackActive1.AttackAction(playerTransform);
        }
        

    }
}
