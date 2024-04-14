using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RobbieWagnerGames.Common
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] protected Canvas canvas;

        protected MenuControls menuControls;
        [SerializeField] protected bool OnByDefault;
        [HideInInspector] public bool isOn = false;
        [SerializeField] protected List<MenuButton> menuButtons;
        protected int curButton = 0;
        protected int CurButton
        {
            get => curButton;
            set
            {
                if (value == curButton) 
                    return;

                curButton = value;

                if (curButton >= menuButtons.Count) 
                    curButton = 0;
                if (curButton < 0) 
                    curButton = menuButtons.Count - 1;
            }
        }

        protected virtual void Awake()
        {
            canvas.enabled = false;
            menuControls = new MenuControls();
            menuControls.UIInput.Navigate.started += NavigateMenu;
            menuControls.UIInput.Select.performed += SelectMenuItem;

            if (OnByDefault) 
                SetupMenu();
        }

        public virtual void SetupMenu()
        {
            canvas.enabled = true;
            ConsiderMenuButton(CurButton);
            foreach (MenuButton button in menuButtons) 
                button.parentMenu = this;

            menuControls.Enable();
            isOn = true;
            OnOpenMenu?.Invoke();
        }

        public void CallOnOpen()
        {
            OnOpenMenu?.Invoke();
        }
        public delegate void MenuDelegate();
        public event MenuDelegate OnOpenMenu;

        public virtual void DisableMenu(bool returnToPreviousMenu = true)
        {
            StartCoroutine(DisableMenuCo(returnToPreviousMenu));
        }

        public virtual IEnumerator DisableMenuCo(bool returnToPreviousMenu = true)
        {
            yield return null;
            canvas.enabled = false;
            if (returnToPreviousMenu)
                ReturnToPreviousMenu?.Invoke();
            
            menuControls.Disable();
            isOn = false;
            OnCloseMenu?.Invoke();
        }
        public event MenuDelegate OnCloseMenu;
        public delegate void OnEnablePreviousMenuDelegate();
        public event OnEnablePreviousMenuDelegate ReturnToPreviousMenu;

        protected void ConsiderMenuButton(int activeButtonIndex)
        {
            foreach (MenuButton button in menuButtons)
                button.NavigateAway();
            if(menuButtons.Any())
                menuButtons[activeButtonIndex].NavigateTo();
            BasicAudioManager.Instance.PlayAudioSource(AudioSourceName.UINav);
        }

        protected virtual void NavigateMenu(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<float>();

            if (direction > 0)
                CurButton++;
            else
                CurButton--;

            ConsiderMenuButton(CurButton);
        }

        protected virtual void SelectMenuItem(InputAction.CallbackContext context)
        {
            DisableMenu();
            if(menuButtons.Any())
                StartCoroutine(menuButtons[CurButton].SelectButton(this));
            InvokeOnSelectMenuItem();
        }

        public delegate void OnSelectMenuItemDelegate();
        public event OnSelectMenuItemDelegate OnSelectMenuItem;
        protected void InvokeOnSelectMenuItem()
        {
            OnSelectMenuItem?.Invoke();
        }

        protected virtual void GoToPreviousMenu(InputAction.CallbackContext context)
        {
            if(ReturnToPreviousMenu.GetInvocationList().Count() > 0) 
                DisableMenu(true);
        }
    }
}