//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Dropper"",
            ""id"": ""0cea584b-bf86-46a0-ab83-9e67234d1f70"",
            ""actions"": [
                {
                    ""name"": ""DropBall"",
                    ""type"": ""Button"",
                    ""id"": ""9408c5ae-0191-4a5e-b7a0-973811ade1bb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f4cfa4de-d359-453c-8764-0a0211a2dea2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropBall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""561db597-ebd3-40c6-bcd1-3b36ab17c4b1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropBall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""957c3edc-42c0-47de-9723-7d02f96fdbd0"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropBall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06a0b4e2-b18d-4936-a53c-a12bc5fdc169"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropBall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a958f5d-8e54-4ca2-84cd-c1a8a59bf79e"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropBall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""2110518c-8f43-4528-b7ce-560d867b1f6b"",
            ""actions"": [
                {
                    ""name"": ""ExitMenu"",
                    ""type"": ""Button"",
                    ""id"": ""d956a3c4-e355-4994-8d26-b75c0d3865ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""083b31b8-a9e0-4e73-9ad3-fe3698ee7aa1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffd80063-744a-4a4c-aab0-8bacd2ce33af"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a2d0be2-5201-4ced-a805-0e96038a32c3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Dropper
        m_Dropper = asset.FindActionMap("Dropper", throwIfNotFound: true);
        m_Dropper_DropBall = m_Dropper.FindAction("DropBall", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_ExitMenu = m_Menu.FindAction("ExitMenu", throwIfNotFound: true);
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

    // Dropper
    private readonly InputActionMap m_Dropper;
    private List<IDropperActions> m_DropperActionsCallbackInterfaces = new List<IDropperActions>();
    private readonly InputAction m_Dropper_DropBall;
    public struct DropperActions
    {
        private @PlayerControls m_Wrapper;
        public DropperActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DropBall => m_Wrapper.m_Dropper_DropBall;
        public InputActionMap Get() { return m_Wrapper.m_Dropper; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DropperActions set) { return set.Get(); }
        public void AddCallbacks(IDropperActions instance)
        {
            if (instance == null || m_Wrapper.m_DropperActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DropperActionsCallbackInterfaces.Add(instance);
            @DropBall.started += instance.OnDropBall;
            @DropBall.performed += instance.OnDropBall;
            @DropBall.canceled += instance.OnDropBall;
        }

        private void UnregisterCallbacks(IDropperActions instance)
        {
            @DropBall.started -= instance.OnDropBall;
            @DropBall.performed -= instance.OnDropBall;
            @DropBall.canceled -= instance.OnDropBall;
        }

        public void RemoveCallbacks(IDropperActions instance)
        {
            if (m_Wrapper.m_DropperActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDropperActions instance)
        {
            foreach (var item in m_Wrapper.m_DropperActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DropperActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DropperActions @Dropper => new DropperActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private List<IMenuActions> m_MenuActionsCallbackInterfaces = new List<IMenuActions>();
    private readonly InputAction m_Menu_ExitMenu;
    public struct MenuActions
    {
        private @PlayerControls m_Wrapper;
        public MenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ExitMenu => m_Wrapper.m_Menu_ExitMenu;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void AddCallbacks(IMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MenuActionsCallbackInterfaces.Add(instance);
            @ExitMenu.started += instance.OnExitMenu;
            @ExitMenu.performed += instance.OnExitMenu;
            @ExitMenu.canceled += instance.OnExitMenu;
        }

        private void UnregisterCallbacks(IMenuActions instance)
        {
            @ExitMenu.started -= instance.OnExitMenu;
            @ExitMenu.performed -= instance.OnExitMenu;
            @ExitMenu.canceled -= instance.OnExitMenu;
        }

        public void RemoveCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IDropperActions
    {
        void OnDropBall(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnExitMenu(InputAction.CallbackContext context);
    }
}