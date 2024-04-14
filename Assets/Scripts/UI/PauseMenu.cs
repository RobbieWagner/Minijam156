using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using RobbieWagnerGames.Managers;

namespace RobbieWagnerGames.Common
{
    public class PauseMenu : Menu
    {
        public static PauseMenu Instance { get; private set; }

        protected override void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            base.Awake();
        }

        public void SetupMenuHandler()
        {
            SetupMenu();
        }

        public override void SetupMenu()
        {
            base.SetupMenu();
        }


        private void DisableMenu()
        {
            StartCoroutine(DisableMenuCo());
        }

        public override IEnumerator DisableMenuCo(bool returnToPreviousMenu = true)
        {
            //Resume game with resume menu option selected
            yield return StartCoroutine(base.DisableMenuCo(returnToPreviousMenu));
        }

        protected override void SelectMenuItem(InputAction.CallbackContext context)
        {
            StartCoroutine(menuButtons[CurButton].SelectButton(this));
            InvokeOnSelectMenuItem();
        }
    }
}