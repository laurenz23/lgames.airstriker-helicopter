using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// attached this script to object as a child
/// NOTE:   apply this script to air objects only
/// </summary>

namespace game_ideas
{
    public class FlyOffMovement : MonoBehaviour
    {

        [SerializeField] private Transform character; // attached here the object that going to move
        [SerializeField] private float speed;
        [SerializeField] private float flyOffTime;
        [SerializeField] private bool applyAutomaticFly;

        private Transform targetPlayer;
        private GameManager gameManager;

        private float currentFlyOff = 0f;

        private bool fly;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();

            targetPlayer = FindObjectOfType<PlayerManager>().GetPlayerTransform();
        }

        private void Start()
        {

            // check if the script attached to is have also a movement script attached to it
            // disabled the other movement first and enabling the script again if character object already at fly off
            EnabledMovement(false);
        }

        private void Update()
        {

            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {
                float posZ = targetPlayer.position.z - character.position.z;

                if (posZ > 0f)
                {
                    fly = true;
                }

                if (fly)
                {
                    currentFlyOff += Time.deltaTime;

                    if (applyAutomaticFly)
                    {
                        character.Translate(Vector3.up * speed * Time.deltaTime);
                    }
                    else
                    {
                        character.Translate(Vector3.forward * speed * Time.deltaTime);
                    }

                    if (currentFlyOff >= flyOffTime && applyAutomaticFly.Equals(false))
                    {
                        character.Translate(Vector3.up * speed * Time.deltaTime);
                    }

                    if (currentFlyOff >= flyOffTime + 1f)
                    {

                        EnabledMovement(true);

                        enabled = false;
                    }

                }

            }

        }


        private void EnabledMovement(bool enabledMovement)
        {
            if (GetComponent<CrashMovement>())
            {
                GetComponent<CrashMovement>().enabled = enabledMovement;
            }
            else if (GetComponent<OneWayMovement>())
            {
                GetComponent<OneWayMovement>().enabled = enabledMovement;
            }
            else if (GetComponent<OnTargetMovementHorizontal>())
            {
                GetComponent<OnTargetMovementHorizontal>().enabled = enabledMovement;
            }
            else if (GetComponent<OnTargetMovementVertical>())
            {
                GetComponent<OnTargetMovementVertical>().enabled = enabledMovement;
            }
            else if (GetComponent<PathMovement>())
            {
                GetComponent<PathMovement>().enabled = enabledMovement;
            }
        }

    }
}
