// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InputActions/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""TouchInput"",
            ""id"": ""17d62e5e-5a2d-4993-beab-3c54ed4159cd"",
            ""actions"": [
                {
                    ""name"": ""PrimaryContact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fc319fe0-8d82-4696-9444-92cbd8e82c98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.5)""
                },
                {
                    ""name"": ""PrimaryPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4ccb64f3-0f50-4d86-b2b1-64288b56160f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryDelta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5c07b1ce-726f-4a22-893c-b716881388fe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2e833394-3a92-4d90-b444-78fe047efc3b"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4921ff47-a46c-411e-924f-4a61ffe29814"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b22084c2-3556-411f-8305-e98f20a98720"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerInput"",
            ""id"": ""d2abd70c-251d-4222-9257-1da2a6629f10"",
            ""actions"": [
                {
                    ""name"": ""AttackBasic"",
                    ""type"": ""Button"",
                    ""id"": ""fc4088e4-bc54-4820-876c-55938076362f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7344df92-3df8-4e11-82bd-bf36b0940c15"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackSkill1"",
                    ""type"": ""Button"",
                    ""id"": ""36bb675b-5a70-4436-a6b0-9a20561f0dfa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2d2aad5a-b8bb-4d18-b05a-ee245b76767a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackBasic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccc2c176-effa-4f0d-92a0-93cbf4929fe6"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackSkill1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6fbdccb-db71-44cb-a916-74b96cb0fe98"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""bdf7f07d-9c46-4598-a993-975105cadfc5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""09f14929-4915-4e7a-b2d1-9f89b6839c05"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9e117c49-ac13-4815-b918-9d32bd8ee81f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""92bbdf24-33e5-4302-aae5-959b5707c41c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7fc5b320-ad6d-417c-ae85-767eed58df35"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""SensorInput"",
            ""id"": ""fc7013a2-b770-41c9-958d-dd08844507d7"",
            ""actions"": [
                {
                    ""name"": ""Accelerometer"",
                    ""type"": ""Value"",
                    ""id"": ""98051792-ed0a-4df5-9138-a94ca163c6f9"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""979b2513-d498-4e0e-b910-5503a0487d66"",
                    ""path"": ""<Accelerometer>/acceleration"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerometer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TouchInput
        m_TouchInput = asset.FindActionMap("TouchInput", throwIfNotFound: true);
        m_TouchInput_PrimaryContact = m_TouchInput.FindAction("PrimaryContact", throwIfNotFound: true);
        m_TouchInput_PrimaryPosition = m_TouchInput.FindAction("PrimaryPosition", throwIfNotFound: true);
        m_TouchInput_PrimaryDelta = m_TouchInput.FindAction("PrimaryDelta", throwIfNotFound: true);
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_AttackBasic = m_PlayerInput.FindAction("AttackBasic", throwIfNotFound: true);
        m_PlayerInput_Move = m_PlayerInput.FindAction("Move", throwIfNotFound: true);
        m_PlayerInput_AttackSkill1 = m_PlayerInput.FindAction("AttackSkill1", throwIfNotFound: true);
        // SensorInput
        m_SensorInput = asset.FindActionMap("SensorInput", throwIfNotFound: true);
        m_SensorInput_Accelerometer = m_SensorInput.FindAction("Accelerometer", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // TouchInput
    private readonly InputActionMap m_TouchInput;
    private ITouchInputActions m_TouchInputActionsCallbackInterface;
    private readonly InputAction m_TouchInput_PrimaryContact;
    private readonly InputAction m_TouchInput_PrimaryPosition;
    private readonly InputAction m_TouchInput_PrimaryDelta;
    public struct TouchInputActions
    {
        private @InputControls m_Wrapper;
        public TouchInputActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryContact => m_Wrapper.m_TouchInput_PrimaryContact;
        public InputAction @PrimaryPosition => m_Wrapper.m_TouchInput_PrimaryPosition;
        public InputAction @PrimaryDelta => m_Wrapper.m_TouchInput_PrimaryDelta;
        public InputActionMap Get() { return m_Wrapper.m_TouchInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchInputActions set) { return set.Get(); }
        public void SetCallbacks(ITouchInputActions instance)
        {
            if (m_Wrapper.m_TouchInputActionsCallbackInterface != null)
            {
                @PrimaryContact.started -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryContact;
                @PrimaryContact.performed -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryContact;
                @PrimaryContact.canceled -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryContact;
                @PrimaryPosition.started -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.performed -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.canceled -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryDelta.started -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryDelta;
                @PrimaryDelta.performed -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryDelta;
                @PrimaryDelta.canceled -= m_Wrapper.m_TouchInputActionsCallbackInterface.OnPrimaryDelta;
            }
            m_Wrapper.m_TouchInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryContact.started += instance.OnPrimaryContact;
                @PrimaryContact.performed += instance.OnPrimaryContact;
                @PrimaryContact.canceled += instance.OnPrimaryContact;
                @PrimaryPosition.started += instance.OnPrimaryPosition;
                @PrimaryPosition.performed += instance.OnPrimaryPosition;
                @PrimaryPosition.canceled += instance.OnPrimaryPosition;
                @PrimaryDelta.started += instance.OnPrimaryDelta;
                @PrimaryDelta.performed += instance.OnPrimaryDelta;
                @PrimaryDelta.canceled += instance.OnPrimaryDelta;
            }
        }
    }
    public TouchInputActions @TouchInput => new TouchInputActions(this);

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_AttackBasic;
    private readonly InputAction m_PlayerInput_Move;
    private readonly InputAction m_PlayerInput_AttackSkill1;
    public struct PlayerInputActions
    {
        private @InputControls m_Wrapper;
        public PlayerInputActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AttackBasic => m_Wrapper.m_PlayerInput_AttackBasic;
        public InputAction @Move => m_Wrapper.m_PlayerInput_Move;
        public InputAction @AttackSkill1 => m_Wrapper.m_PlayerInput_AttackSkill1;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @AttackBasic.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttackBasic;
                @AttackBasic.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttackBasic;
                @AttackBasic.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttackBasic;
                @Move.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMove;
                @AttackSkill1.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttackSkill1;
                @AttackSkill1.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttackSkill1;
                @AttackSkill1.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnAttackSkill1;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AttackBasic.started += instance.OnAttackBasic;
                @AttackBasic.performed += instance.OnAttackBasic;
                @AttackBasic.canceled += instance.OnAttackBasic;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @AttackSkill1.started += instance.OnAttackSkill1;
                @AttackSkill1.performed += instance.OnAttackSkill1;
                @AttackSkill1.canceled += instance.OnAttackSkill1;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);

    // SensorInput
    private readonly InputActionMap m_SensorInput;
    private ISensorInputActions m_SensorInputActionsCallbackInterface;
    private readonly InputAction m_SensorInput_Accelerometer;
    public struct SensorInputActions
    {
        private @InputControls m_Wrapper;
        public SensorInputActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accelerometer => m_Wrapper.m_SensorInput_Accelerometer;
        public InputActionMap Get() { return m_Wrapper.m_SensorInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SensorInputActions set) { return set.Get(); }
        public void SetCallbacks(ISensorInputActions instance)
        {
            if (m_Wrapper.m_SensorInputActionsCallbackInterface != null)
            {
                @Accelerometer.started -= m_Wrapper.m_SensorInputActionsCallbackInterface.OnAccelerometer;
                @Accelerometer.performed -= m_Wrapper.m_SensorInputActionsCallbackInterface.OnAccelerometer;
                @Accelerometer.canceled -= m_Wrapper.m_SensorInputActionsCallbackInterface.OnAccelerometer;
            }
            m_Wrapper.m_SensorInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Accelerometer.started += instance.OnAccelerometer;
                @Accelerometer.performed += instance.OnAccelerometer;
                @Accelerometer.canceled += instance.OnAccelerometer;
            }
        }
    }
    public SensorInputActions @SensorInput => new SensorInputActions(this);
    public interface ITouchInputActions
    {
        void OnPrimaryContact(InputAction.CallbackContext context);
        void OnPrimaryPosition(InputAction.CallbackContext context);
        void OnPrimaryDelta(InputAction.CallbackContext context);
    }
    public interface IPlayerInputActions
    {
        void OnAttackBasic(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnAttackSkill1(InputAction.CallbackContext context);
    }
    public interface ISensorInputActions
    {
        void OnAccelerometer(InputAction.CallbackContext context);
    }
}
