// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/InputActions/PCInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PCInputActions : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @PCInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PCInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""30ce39fa-e027-4174-a744-0402bce7f9d5"",
            ""actions"": [
                {
                    ""name"": ""DPadMove"",
                    ""type"": ""Button"",
                    ""id"": ""da7d7930-59c1-41e8-8278-1688b6024b2c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6348c776-dc5b-4d73-af22-8989800cb48d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovementPress"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b2e5f322-5540-41d9-a909-5eb0b19162ca"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovementRelease"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c766af32-6600-4e24-85af-d27b33662bea"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""914b6c65-52f1-44bf-a46b-8b42fd38fe26"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JumpPress"",
                    ""type"": ""Button"",
                    ""id"": ""61e11eb7-ce6b-492d-b4fd-10ef48c63e0c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JumpRelease"",
                    ""type"": ""Button"",
                    ""id"": ""8a14cd99-3b92-45f2-99a9-1623871092b4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""f5577764-22a0-44c2-81f4-21c3798c6216"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialAction"",
                    ""type"": ""Button"",
                    ""id"": ""9b56b737-e962-4a4a-b55e-3ca29262f7e3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""af3df8e5-a8b8-4d3f-b8a0-8263a369fdbb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(pressPoint=1E-08)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""JumpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50e88017-b6c9-4cbd-8cca-e4a945da910a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(pressPoint=1E-08)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""JumpPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d730748f-824b-4057-85b2-75ddec974dc8"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": ""Press(pressPoint=1E-09)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""419fcfe9-cc3e-4066-b208-caaa68e30900"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": ""Press(pressPoint=1E-08)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""4682ea6f-e197-4b25-b977-800274e607da"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press(pressPoint=1E-08)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementPress"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""49888050-0ba7-47e2-83df-4c7741a299df"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""51f6e308-831b-425a-89b6-9345d0078aa7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""de7905c9-0311-4238-b602-e93c35fd8f32"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""17dd2e6f-09e5-4f95-90c6-937acafd07f6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""9c0648ba-51f2-489d-bca1-f751625955d5"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press(pressPoint=1E-08)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementPress"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3f282032-3667-4cb8-961c-1e9e495552c3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e3ed3edc-1803-4fba-bdf6-4cb3df432eac"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d5527ab1-4788-497f-9965-77df9b8fb423"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""18d5e681-cb50-4a0d-91c2-70b09fa9efe2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""be61d5eb-4b68-4a75-8761-91211f7f4f8b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cce3d844-a93d-4752-a386-784cbc26d363"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7412024-9835-44e3-a955-2057a100e381"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f9f6345-3686-4314-be1f-985e0a05b831"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(pressPoint=1E-08,behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""JumpRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23e4c042-99ec-4534-b97b-9c1f0f1f38e4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(pressPoint=1E-08,behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""JumpRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9904d168-7c67-4428-9c25-b03bb27241e7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SpecialAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88d707a7-8ef1-4ebc-8d5d-eb461f8babcc"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": ""Press(pressPoint=1E-09,behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9023e60-3ba1-4be8-a728-37e3aedb1f24"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": ""Press(pressPoint=1E-08,behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""6e29a79f-48e4-4151-baa7-718ec08e3a21"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press(pressPoint=1E-08,behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7bffd071-122d-4e26-9a1d-a17913bce8c4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c87cc9ec-eaaa-43d9-ade7-5b85e521b00e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a65cbb02-e5da-4a80-ac44-d4d5949ff951"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4929a8fa-cbe1-4a95-89ca-bc34ad5a8835"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""987dd263-6e9d-404a-8faf-22019ddf4637"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press(pressPoint=1E-08,behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9aa3d5b7-1c0d-49d0-b474-e49a6bd8af19"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dea10913-e813-4df8-8e10-c05231d866a2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2331047e-df51-4f58-998f-c364efcd3b79"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""09270bc9-7b54-4515-9898-c96b40aa281e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovementRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2168c997-bc65-4c4e-9f0f-db15a6028473"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""b7313dc1-b20f-4d28-ae42-2539546c291e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPadMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""876b7285-2d02-40ea-a636-c562c565576c"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DPadMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0bf22575-0029-4421-87d3-4939aabf5cd1"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DPadMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""559697be-9d94-46ea-911d-0187773e88af"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DPadMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""df9b9e01-ce7b-41b7-a0ca-aaa2763d02cc"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DPadMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""046a8cd3-039b-4eac-b35e-8fefc4eeacc7"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""63d8f749-d0d9-47ee-a0b5-08a8be2e5a0d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""c3dc87c9-31bd-4ebf-8b08-e298797456e6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""UDLR"",
                    ""id"": ""65b1b51e-5abd-43a2-a26f-288b28233c81"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c95f5c3d-5632-42a1-af6c-ebff760ca78b"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d7a2a2f1-f102-4852-baa1-ada1c3c3a1d9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3f4eaae0-e923-4ac3-baa3-1e8e6cf207cb"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""66995342-2158-4231-a448-322c86553555"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3e4d0805-1e79-4200-92a1-44fca4607e8f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ab16734-5514-496a-96c6-4ce833047cb7"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_DPadMove = m_Player.FindAction("DPadMove", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_MovementPress = m_Player.FindAction("MovementPress", throwIfNotFound: true);
        m_Player_MovementRelease = m_Player.FindAction("MovementRelease", throwIfNotFound: true);
        m_Player_Action = m_Player.FindAction("Action", throwIfNotFound: true);
        m_Player_JumpPress = m_Player.FindAction("JumpPress", throwIfNotFound: true);
        m_Player_JumpRelease = m_Player.FindAction("JumpRelease", throwIfNotFound: true);
        m_Player_Menu = m_Player.FindAction("Menu", throwIfNotFound: true);
        m_Player_SpecialAction = m_Player.FindAction("SpecialAction", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Movement = m_UI.FindAction("Movement", throwIfNotFound: true);
        m_UI_Action = m_UI.FindAction("Action", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_DPadMove;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_MovementPress;
    private readonly InputAction m_Player_MovementRelease;
    private readonly InputAction m_Player_Action;
    private readonly InputAction m_Player_JumpPress;
    private readonly InputAction m_Player_JumpRelease;
    private readonly InputAction m_Player_Menu;
    private readonly InputAction m_Player_SpecialAction;
    public struct PlayerActions
    {
        private @PCInputActions m_Wrapper;
        public PlayerActions(@PCInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @DPadMove => m_Wrapper.m_Player_DPadMove;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @MovementPress => m_Wrapper.m_Player_MovementPress;
        public InputAction @MovementRelease => m_Wrapper.m_Player_MovementRelease;
        public InputAction @Action => m_Wrapper.m_Player_Action;
        public InputAction @JumpPress => m_Wrapper.m_Player_JumpPress;
        public InputAction @JumpRelease => m_Wrapper.m_Player_JumpRelease;
        public InputAction @Menu => m_Wrapper.m_Player_Menu;
        public InputAction @SpecialAction => m_Wrapper.m_Player_SpecialAction;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @DPadMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDPadMove;
                @DPadMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDPadMove;
                @DPadMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDPadMove;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @MovementPress.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementPress;
                @MovementPress.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementPress;
                @MovementPress.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementPress;
                @MovementRelease.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementRelease;
                @MovementRelease.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementRelease;
                @MovementRelease.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovementRelease;
                @Action.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                @JumpPress.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpPress;
                @JumpPress.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpPress;
                @JumpPress.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpPress;
                @JumpRelease.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpRelease;
                @JumpRelease.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpRelease;
                @JumpRelease.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJumpRelease;
                @Menu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMenu;
                @SpecialAction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialAction;
                @SpecialAction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialAction;
                @SpecialAction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpecialAction;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DPadMove.started += instance.OnDPadMove;
                @DPadMove.performed += instance.OnDPadMove;
                @DPadMove.canceled += instance.OnDPadMove;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MovementPress.started += instance.OnMovementPress;
                @MovementPress.performed += instance.OnMovementPress;
                @MovementPress.canceled += instance.OnMovementPress;
                @MovementRelease.started += instance.OnMovementRelease;
                @MovementRelease.performed += instance.OnMovementRelease;
                @MovementRelease.canceled += instance.OnMovementRelease;
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
                @JumpPress.started += instance.OnJumpPress;
                @JumpPress.performed += instance.OnJumpPress;
                @JumpPress.canceled += instance.OnJumpPress;
                @JumpRelease.started += instance.OnJumpRelease;
                @JumpRelease.performed += instance.OnJumpRelease;
                @JumpRelease.canceled += instance.OnJumpRelease;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @SpecialAction.started += instance.OnSpecialAction;
                @SpecialAction.performed += instance.OnSpecialAction;
                @SpecialAction.canceled += instance.OnSpecialAction;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Movement;
    private readonly InputAction m_UI_Action;
    public struct UIActions
    {
        private @PCInputActions m_Wrapper;
        public UIActions(@PCInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_UI_Movement;
        public InputAction @Action => m_Wrapper.m_UI_Action;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMovement;
                @Action.started -= m_Wrapper.m_UIActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnAction;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnDPadMove(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnMovementPress(InputAction.CallbackContext context);
        void OnMovementRelease(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnJumpPress(InputAction.CallbackContext context);
        void OnJumpRelease(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnSpecialAction(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
    }
}
