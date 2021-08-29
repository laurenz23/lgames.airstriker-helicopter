using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// usage: attached this script to character or platform that going to rotate
/// goal: handles the swipe input of platform and platform rotation
/// </summary>

namespace game_ideas
{
    public class PlatformRotator : MonoBehaviour
    {

        [SerializeField] private float rotationSpeed; // assign the rotation speed of object

        [Header("Platform Swipe Settings")]
        [SerializeField] private float swipeRotationSpeed;

        [SerializeField] private float minXSwipeArea; // minimum horizontal platform touch area

        [SerializeField] private float maxXSwipeArea; // maximum horizontal platform touch area 

        [SerializeField] private float minYSwipeArea; // minimum vertical platform touch area

        [SerializeField] private float maxYSwipeArea; // maximum vertical platform touch area

        [Header("Touch Settings")]
        [SerializeField] private float minimumDistance; // minimum distance to recoginize a swipe input

        [SerializeField] private float maximumTime; // maximum time to reject the input is not a swipe input

        // reference for touch start position and touch start time
        private Vector2 startPosition; 
        private float startTime;

        // reference for touch current position after touch is detected
        private Vector2 performedPosition;

        // reference for touch last position before touch input ended
        private Vector2 endPosition;
        private float endTime;

        private bool touchDetected;
        private bool rotatePlatform;

        private InputManager inputManager;


        private void Awake()
        {
            inputManager = InputManager.GetInstance();
            rotatePlatform = true;
        }

        private void OnEnable()
        {
            // subscribe to touch input
            inputManager.OnStartTouch += SwipeStart;
            inputManager.OnEndTouch += SwipeEnd;
        }

        private void OnDisable()
        {
            // unsubscribe to touch input
            inputManager.OnStartTouch -= SwipeStart;
            inputManager.OnEndTouch -= SwipeEnd;
        }

        private void Update()
        {
            // touch is detected?
            if (touchDetected) 
            {
                performedPosition = inputManager.PrimaryTouchPosition(); // assign touch position 
                DetectSwipe();
            }
            
            // if touch is detected to platform area, pause platform rotation
            if (rotatePlatform)
            {
                // rotate object in y axis
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (rotationSpeed * Time.deltaTime), transform.eulerAngles.z);
            }
        }

        // assign start input touch position and time
        private void SwipeStart(Vector2 position, float time)
        {
            startPosition = position;
            startTime = time;
            touchDetected = true;
        }

        // assign last input touch position and time
        private void SwipeEnd(Vector2 position, float time)
        {
            endPosition = position;
            endTime = time;
            touchDetected = false;
            rotatePlatform = true; // continue platform rotation
        }
        
        private void DetectSwipe()
        {
            // check if touch input is inside platform touch area
            if (
                startPosition.x > minXSwipeArea && startPosition.x < maxXSwipeArea &&
                startPosition.y > minYSwipeArea && startPosition.y < maxYSwipeArea
                )
            {
                if (Vector3.Distance(startPosition, performedPosition) >= minimumDistance)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - (inputManager.PrimaryTouchDelta().x * swipeRotationSpeed), transform.eulerAngles.z);
#if UNITY_EDITOR
                    Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
#endif
                }
                
                rotatePlatform = false; // pause platform rotation
            }
        }

        // switching panel may change platform touch area
        // since were going to change the position of character when switching to other panels
        public void ArmoryPlatform(float newPosValue)
        {
            minXSwipeArea = minXSwipeArea - newPosValue;
            maxXSwipeArea = maxXSwipeArea - newPosValue;
        }

        public void MainMenuPlatform(float newPosValue)
        {
            minXSwipeArea = minXSwipeArea + newPosValue;
            maxXSwipeArea = maxXSwipeArea + newPosValue;
        }

    }
}
