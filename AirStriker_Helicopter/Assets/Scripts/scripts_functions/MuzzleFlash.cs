using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class MuzzleFlash : MonoBehaviour
    {
        [HideInInspector]
        public Transform attackPointTransform;

        private float delay = 0.2f;
        private float delayTime = 0f;

        private void Update()
        {
            if (attackPointTransform != null)
            {
                transform.position = attackPointTransform.position;
            }

            delayTime += 1f * Time.deltaTime;

            if (delayTime >= delay)
            {
                DestroyMuzzleFlash();
            }
        }

        private void DestroyMuzzleFlash()
        {
            delayTime = 0f;
            gameObject.SetActive(false);
        }
    }
}
