using UnityEngine;
using UnityEngine.InputSystem;

namespace game_ideas
{
    public class InputManager : MonoBehaviour
    {

        #region Events
        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;

        public delegate void EndTouch(Vector2 position, float time);
        public event EndTouch OnEndTouch;
        #endregion

        [HideInInspector]
        public InputControls inputControls;

        private GameManager gameManager;
        private Camera mainCamera;

        private static InputManager instance;

        public static InputManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            inputControls = new InputControls();

            gameManager = GameManager.GetInstance();

            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            if (Accelerometer.current != null)
            {
                InputSystem.EnableDevice(Accelerometer.current);
            }

            inputControls.Enable();
        }

        private void OnDisable()
        {
            inputControls.Disable();
        }

        private void Start()
        {
            // subscription to touchInputs
            inputControls.TouchInput.PrimaryContact.started += context => StartTouchPrimary(context);
            inputControls.TouchInput.PrimaryContact.canceled += context => EndTouchPrimary(context);
            // end of subscription from touch inputs

            // subscription to playerInputs
            inputControls.PlayerInput.AttackBasic.started += context => StartAttackBasic(context);
            inputControls.PlayerInput.AttackBasic.canceled += context => EndAttackBasic(context);

            inputControls.PlayerInput.AttackSkill1.started += context => StartAttackSkill1(context);
            inputControls.PlayerInput.AttackSkill1.canceled += context => EndAttackSkill1(context);
            // end of subscription from player touch inputs
        }

        private void Update()
        {
            // gameplay? if in game get stick value
            if (gameManager.gameState == GameState.GAME_START || gameManager.gameState == GameState.GAME_CONTINUE)
            {

                Vector2 stickValue = inputControls.PlayerInput.Move.ReadValue<Vector2>();

                // player movements
                if (stickValue.x > .5f)
                {
                    VirtualInput.Instance.moveForward = true;
                }
                else
                {
                    VirtualInput.Instance.moveForward = false;
                }

                if (stickValue.x < -.5f)
                {
                    VirtualInput.Instance.moveBackward = true;
                }
                else
                {
                    VirtualInput.Instance.moveBackward = false;
                }

                if (stickValue.y > .5f)
                {
                    VirtualInput.Instance.moveAscending = true;
                }
                else
                {
                    VirtualInput.Instance.moveAscending = false;
                }

                if (stickValue.y < -.5f)
                {
                    VirtualInput.Instance.moveDescending = true;
                }
                else
                {
                    VirtualInput.Instance.moveDescending = false;
                }

            }
        }

        // touch inputs 
        private void StartTouchPrimary(InputAction.CallbackContext context)
        {
            OnStartTouch?.Invoke(Utils.ScreenToWorld(mainCamera, inputControls.TouchInput.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }

        private void EndTouchPrimary(InputAction.CallbackContext context)
        {
            OnEndTouch?.Invoke(Utils.ScreenToWorld(mainCamera, inputControls.TouchInput.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }

        public Vector2 PrimaryTouchPosition()
        {
            return Utils.ScreenToWorld(mainCamera, inputControls.TouchInput.PrimaryPosition.ReadValue<Vector2>());
        }

        public Vector2 PrimaryTouchDelta()
        {
            return inputControls.TouchInput.PrimaryDelta.ReadValue<Vector2>();
        }
        
        public Vector2 PrimaryPosition()
        {
            return Utils.ScreenToWorld(mainCamera, inputControls.TouchInput.PrimaryContact.ReadValue<Vector2>());
        }
        // end touch inputs

        // player inputs in game only
        private void StartAttackBasic(InputAction.CallbackContext context)
        {
            VirtualInput.Instance.attack = true;
        }

        private void EndAttackBasic(InputAction.CallbackContext context)
        {
            VirtualInput.Instance.attack = false;
        }

        private void StartAttackSkill1(InputAction.CallbackContext context)
        {
            VirtualInput.Instance.activeSkill1 = true;
        }

        private void EndAttackSkill1(InputAction.CallbackContext context)
        {
            VirtualInput.Instance.activeSkill1 = false;
        }
        // end player inputs

        // device sensor inputs
        public Vector3 DeviceAccelerometer()
        {
            return inputControls.SensorInput.Accelerometer.ReadValue<Vector3>();
        }
        // end device sensor inputs
    }
}
