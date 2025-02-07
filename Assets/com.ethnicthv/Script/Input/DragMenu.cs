//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/com.ethnicthv/Script/Input/DragMenu.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace com.ethnicthv.Script.Input
{
    public partial class @DragMenu: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @DragMenu()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""DragMenu"",
    ""maps"": [
        {
            ""name"": ""Input"",
            ""id"": ""f00d155e-64cb-4721-9869-9bc08e0c1c9d"",
            ""actions"": [
                {
                    ""name"": ""Pressed"",
                    ""type"": ""Button"",
                    ""id"": ""2e351af5-4834-4161-a791-8878a99ee917"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DragDelta"",
                    ""type"": ""Value"",
                    ""id"": ""8098a260-b5e5-4143-b0f0-ec39867dcb42"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4d7709b1-eb56-4433-bb76-f0ce6f3d0976"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b375b895-e49a-4b02-aeae-1c8cab66831c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DragDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Input
            m_Input = asset.FindActionMap("Input", throwIfNotFound: true);
            m_Input_Pressed = m_Input.FindAction("Pressed", throwIfNotFound: true);
            m_Input_DragDelta = m_Input.FindAction("DragDelta", throwIfNotFound: true);
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

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Input
        private readonly InputActionMap m_Input;
        private List<IInputActions> m_InputActionsCallbackInterfaces = new List<IInputActions>();
        private readonly InputAction m_Input_Pressed;
        private readonly InputAction m_Input_DragDelta;
        public struct InputActions
        {
            private @DragMenu m_Wrapper;
            public InputActions(@DragMenu wrapper) { m_Wrapper = wrapper; }
            public InputAction @Pressed => m_Wrapper.m_Input_Pressed;
            public InputAction @DragDelta => m_Wrapper.m_Input_DragDelta;
            public InputActionMap Get() { return m_Wrapper.m_Input; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(InputActions set) { return set.Get(); }
            public void AddCallbacks(IInputActions instance)
            {
                if (instance == null || m_Wrapper.m_InputActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_InputActionsCallbackInterfaces.Add(instance);
                @Pressed.started += instance.OnPressed;
                @Pressed.performed += instance.OnPressed;
                @Pressed.canceled += instance.OnPressed;
                @DragDelta.started += instance.OnDragDelta;
                @DragDelta.performed += instance.OnDragDelta;
                @DragDelta.canceled += instance.OnDragDelta;
            }

            private void UnregisterCallbacks(IInputActions instance)
            {
                @Pressed.started -= instance.OnPressed;
                @Pressed.performed -= instance.OnPressed;
                @Pressed.canceled -= instance.OnPressed;
                @DragDelta.started -= instance.OnDragDelta;
                @DragDelta.performed -= instance.OnDragDelta;
                @DragDelta.canceled -= instance.OnDragDelta;
            }

            public void RemoveCallbacks(IInputActions instance)
            {
                if (m_Wrapper.m_InputActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IInputActions instance)
            {
                foreach (var item in m_Wrapper.m_InputActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_InputActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public InputActions @Input => new InputActions(this);
        public interface IInputActions
        {
            void OnPressed(InputAction.CallbackContext context);
            void OnDragDelta(InputAction.CallbackContext context);
        }
    }
}
