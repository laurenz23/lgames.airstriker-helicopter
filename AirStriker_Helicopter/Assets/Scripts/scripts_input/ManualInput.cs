using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class ManualInput : MonoBehaviour
    {
        private PlayerManager playerManager;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            if (VirtualInput.Instance.moveAscending)
            {
                playerManager.moveAscending = true;
            }
            else
            {
                playerManager.moveAscending = false;
            }

            if (VirtualInput.Instance.moveDescending)
            {
                playerManager.moveDescending = true;
            }
            else
            {
                playerManager.moveDescending = false;
            }

            if (VirtualInput.Instance.moveForward)
            {
                playerManager.moveForward = true;
            }
            else
            {
                playerManager.moveForward = false;
            }

            if (VirtualInput.Instance.moveBackward)
            {
                playerManager.moveBackward = true;
            }
            else
            {
                playerManager.moveBackward = false;
            }

            if (VirtualInput.Instance.attack)
            {
                playerManager.attack = true;
            }
            else
            {
                playerManager.attack = false;
            }

            if (VirtualInput.Instance.activeSkill1)
            {
                playerManager.activeSkill1 = true;
            }
            else
            {
                playerManager.activeSkill1 = false;
            }
        }
    }
}
