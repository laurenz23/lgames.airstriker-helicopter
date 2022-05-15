using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage:      this script is attached to objective itself
/// functions:  stores the data of objective, there types and objective values.
///             we disabled this class at the start method to improve game performance
///             instead of getting PlayerUIManager Instance once collided
///             which can drops fps.
///             Once collided it the script will be enabled and moves the script attached object
///             to ui objective type
/// </summary>

namespace game_ideas
{
    public class ObjectiveHandler : MonoBehaviour
    {

        public GameObjective objectiveType;
        public int value;

        private float speed = 60f;

        private PlayerUIManager playerUIManager; // reference for game objective transform

        private void Start()
        {
            playerUIManager = PlayerUIManager.GetInstance();

            // disable the script to improve performance
            // since we don't want to check every frame, even the objective is not yet trigger by player
            if (playerUIManager != null)
            {
                enabled = false;
            }
        }

        public void DestroyObjective()
        {
            // wait a delay before disabling again both script and object
            // we set delay time for move towards action, to be able to see that this game object is moving to objective ui
            StartCoroutine(WaitForDisabling());
        }

        private void Update()
        {
            if (objectiveType == GameObjective.DIAMONDS) // move towards to diamond ui
            {
                transform.position = Vector3.MoveTowards(transform.position, playerUIManager.GetDiamondUIPosition(), speed * Time.deltaTime);
            }
            else if (objectiveType == GameObjective.COINS) // move towards to coin ui
            {
                transform.position = Vector3.MoveTowards(transform.position, playerUIManager.GetCoinUIPosition(), speed * Time.deltaTime);
            }
        }

        // disable this class and hide the game object attached to
        private IEnumerator WaitForDisabling()
        {
            yield return new WaitForSeconds(1f);
            enabled = false;
            gameObject.SetActive(false);
            StopCoroutine(WaitForDisabling());
        }

    }
}
