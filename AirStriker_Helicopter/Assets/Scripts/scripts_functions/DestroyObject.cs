using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game_ideas
{
    public class DestroyObject : MonoBehaviour
    {
        public float delay = 1f;
        private float delayTime = 0f;

        private void Update()
        {
            delayTime += 1f * Time.deltaTime;

            if (delayTime >= delay)
            {
                delayTime = 0f; 
                gameObject.SetActive(false);
            }
        }
    }
}
