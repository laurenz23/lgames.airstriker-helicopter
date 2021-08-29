using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script is attached to detection object
/// handles the detection and return true if detected to a certain object
/// </summary>

namespace game_ideas
{
    public class DetectionHandler : MonoBehaviour
    {
        [SerializeField] private Transform character;
        [SerializeField] private GameTag[] gameTag;
        [SerializeField] private float distance;
        
        private bool onDetected;

        private void Start()
        {
            if (character.eulerAngles.y.Equals(180f) || character.eulerAngles.y.Equals(-180f))
            {
                transform.localPosition = new Vector3(0f, transform.localPosition.y, -transform.localPosition.z);
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, 0f);
            }
        }
         
        private void Update()
        {   
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {

#if UNITY_EDITOR
                // draw red line for each detector objects
                Debug.DrawRay(transform.position, transform.forward, Color.red, distance * 10);
#endif

                foreach (GameTag gt in gameTag)
                {
                    if (hit.transform.tag.Equals(gt.ToString()))
                    {
                        onDetected = true;
                        return;
                    }
                }

            }

            onDetected = false;
        }

        public bool OnDetected()
        {
            return onDetected;
        }

    }
}
