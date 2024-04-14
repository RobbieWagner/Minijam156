using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RobbieWagnerGames.Plinko;
using RobbieWagnerGames.Common;
using UnityEngine.InputSystem;
using System;

namespace RobbieWagnerGames.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] public Canvas screenCoverCanvas;
        [SerializeField] public Image screenCoverImage;
        private PlayerControls playerControls;
        private Coroutine currentEnableCo = null;
        private Coroutine currentDisableCo = null; 
        [SerializeField] private StandingUI standingUI;
        private UIControls uiControls;

        public static UIManager Instance {get; private set;}

        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(gameObject); 
            } 
            else 
            { 
                Instance = this; 
            } 

            screenCoverCanvas.enabled = false;
            DropperBall.Instance.OnChangeDropState += CheckMenuState;
            uiControls = new UIControls();
            uiControls.Enable();
            uiControls.UI.OpenShop.performed += ToggleShop;
            uiControls.UI.OpenDroppers.performed += ToggleDroppers;
        }

        private void ToggleDroppers(InputAction.CallbackContext context)
        {
            if(!ShopMenu.Instance.isOn && !DropperMenu.Instance.isOn && !GameManager.Instance.paused)
                DropperMenu.Instance.SetupMenu();
            else if(DropperMenu.Instance.isOn)
                DropperMenu.Instance.DisableMenu();
        }

        private void ToggleShop(InputAction.CallbackContext context)
        {
            if(!ShopMenu.Instance.isOn && !DropperMenu.Instance.isOn && !GameManager.Instance.paused)
                ShopMenu.Instance.SetupMenu();
            else if(ShopMenu.Instance.isOn)
                ShopMenu.Instance.DisableMenu();
        }

        private void CheckMenuState(DropState dropState)
        {
            switch (dropState)
            {
                case DropState.WAITING:
                case DropState.STOPPED:
                case DropState.PAUSED:
                EnableStandingUI();
                break;
                default:
                DisableStandingUI();
                break;
            }
        }

        public void DisableStandingUI()
        {
            if(currentDisableCo == null)
            {
                currentDisableCo = StartCoroutine(DisableStandingUICo());

            }
        }

        private IEnumerator DisableStandingUICo()
        {
            if(currentEnableCo != null)
            {
                StopCoroutine(currentEnableCo);
                currentEnableCo = null;
            }

            yield return standingUI.GetComponent<RectTransform>().DOAnchorPos(standingUI.offPosition, 2f).WaitForCompletion();
            standingUI.DisableUI();
        }

        public void EnableStandingUI()
        {
            if(currentEnableCo == null)
            {
                currentEnableCo = StartCoroutine(EnableStandingUICo());

            }
        }

        private IEnumerator EnableStandingUICo()
        {
            if(currentDisableCo != null)
            {
                StopCoroutine(currentDisableCo);
                currentDisableCo = null;
            }

            standingUI.EnableUI();
            yield return standingUI.GetComponent<RectTransform>().DOAnchorPos(standingUI.onPosition, 2f).WaitForCompletion();
        }

        public IEnumerator FadeInScreenCover()
        {
            screenCoverImage.color = Color.clear;
            screenCoverCanvas.enabled = true;
            yield return screenCoverImage.DOColor(Color.black, 1).SetEase(Ease.Linear).WaitForCompletion();
        }

        public IEnumerator FadeOutScreenCover()
        {
            screenCoverImage.color = Color.black;
            yield return screenCoverImage.DOColor(Color.clear, 1).SetEase(Ease.Linear).WaitForCompletion();
            screenCoverCanvas.enabled = false;
        }
    }
}