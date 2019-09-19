// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Landkreuzer
{
    public class Input : IInputActionCollection
    {
        private InputActionAsset asset;
        public Input()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Panzer"",
            ""id"": ""83c285b3-282f-4a46-8507-feb01deda114"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""a86f9c8c-5662-4212-a09d-bc7a0a67cb57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""a55a05ae-48a9-4a43-af2f-b6f14c33b366"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""0405ae9e-7494-4b59-98ad-db99a22b3c88"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3899d3cc-f687-4249-af64-51096fcec249"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23b6481f-4190-4230-bd55-79da471b93e8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""6b668cfb-8999-4cc1-9235-b6f22c8cddbe"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""394e8423-52bb-408d-9d25-2c4d9b5de79a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3010df4b-be26-4baf-ae01-623b02d38d10"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""023fde65-a760-4a5f-aff6-0555119ffc8d"",
                    ""path"": ""2DVector(normalize=false)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""16938450-1912-447e-9c4b-411058167f18"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ae41e379-d874-4d47-8677-27791f60db31"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""40833d10-9e56-4736-94c1-be627e4db350"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8557fb0b-0e17-4602-b0f7-264b13aee016"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3b20860b-698b-4d28-bedf-d5ca5dab3c10"",
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
                    ""id"": ""f2753a6b-ad2e-42f3-84dc-cdf409a1baec"",
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
                    ""id"": ""1d948d82-5971-4bc2-afeb-01241ab50699"",
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
                    ""id"": ""1ecfeb12-610e-44ae-a52a-a34bedddba21"",
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
                    ""id"": ""a74acb33-4fbf-4eb6-ab86-85c8439ced10"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Panzer
            m_Panzer = asset.GetActionMap("Panzer");
            m_Panzer_Shoot = m_Panzer.GetAction("Shoot");
            m_Panzer_Move = m_Panzer.GetAction("Move");
            m_Panzer_ChangeWeapon = m_Panzer.GetAction("ChangeWeapon");
        }

        ~Input()
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

        // Panzer
        private readonly InputActionMap m_Panzer;
        private IPanzerActions m_PanzerActionsCallbackInterface;
        private readonly InputAction m_Panzer_Shoot;
        private readonly InputAction m_Panzer_Move;
        private readonly InputAction m_Panzer_ChangeWeapon;
        public struct PanzerActions
        {
            private Input m_Wrapper;
            public PanzerActions(Input wrapper) { m_Wrapper = wrapper; }
            public InputAction @Shoot => m_Wrapper.m_Panzer_Shoot;
            public InputAction @Move => m_Wrapper.m_Panzer_Move;
            public InputAction @ChangeWeapon => m_Wrapper.m_Panzer_ChangeWeapon;
            public InputActionMap Get() { return m_Wrapper.m_Panzer; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PanzerActions set) { return set.Get(); }
            public void SetCallbacks(IPanzerActions instance)
            {
                if (m_Wrapper.m_PanzerActionsCallbackInterface != null)
                {
                    Shoot.started -= m_Wrapper.m_PanzerActionsCallbackInterface.OnShoot;
                    Shoot.performed -= m_Wrapper.m_PanzerActionsCallbackInterface.OnShoot;
                    Shoot.canceled -= m_Wrapper.m_PanzerActionsCallbackInterface.OnShoot;
                    Move.started -= m_Wrapper.m_PanzerActionsCallbackInterface.OnMove;
                    Move.performed -= m_Wrapper.m_PanzerActionsCallbackInterface.OnMove;
                    Move.canceled -= m_Wrapper.m_PanzerActionsCallbackInterface.OnMove;
                    ChangeWeapon.started -= m_Wrapper.m_PanzerActionsCallbackInterface.OnChangeWeapon;
                    ChangeWeapon.performed -= m_Wrapper.m_PanzerActionsCallbackInterface.OnChangeWeapon;
                    ChangeWeapon.canceled -= m_Wrapper.m_PanzerActionsCallbackInterface.OnChangeWeapon;
                }
                m_Wrapper.m_PanzerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Shoot.started += instance.OnShoot;
                    Shoot.performed += instance.OnShoot;
                    Shoot.canceled += instance.OnShoot;
                    Move.started += instance.OnMove;
                    Move.performed += instance.OnMove;
                    Move.canceled += instance.OnMove;
                    ChangeWeapon.started += instance.OnChangeWeapon;
                    ChangeWeapon.performed += instance.OnChangeWeapon;
                    ChangeWeapon.canceled += instance.OnChangeWeapon;
                }
            }
        }
        public PanzerActions @Panzer => new PanzerActions(this);
        public interface IPanzerActions
        {
            void OnShoot(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
            void OnChangeWeapon(InputAction.CallbackContext context);
        }
    }
}
