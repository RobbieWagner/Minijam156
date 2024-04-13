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
            throw new NotImplementedException();
        }

        private void ToggleShop(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
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

        private void DisableStandingUI()
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

        private void EnableStandingUI()
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

        public void PauseGame()
        {
            Time.timeScale = 0;
            
            OnPauseGame?.Invoke();
        }
        public delegate void ActionDelegate();
        public event ActionDelegate OnPauseGame;

        public void ResumeGame()
        {
            Time.timeScale = 1;
            OnResumeGame?.Invoke();
        }
        public event ActionDelegate OnResumeGame;
    }
}