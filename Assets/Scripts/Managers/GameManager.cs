using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RobbieWagnerGames.Managers;

namespace RobbieWagnerGames.Plinko
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {get; private set;}
        public static string persistentDataPath = "";
        [SerializeField] private Canvas mainMenuCanvas;
        private PlayerControls playerControls;
        
        private Coroutine finishRunCo = null;
        
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

            playerControls = new PlayerControls();
            playerControls.Menu.ExitMenu.performed += StartGame;
            playerControls.Enable();
            mainMenuCanvas.enabled = true;
        }

        private void StartGame(InputAction.CallbackContext context)
        {
            playerControls.Disable();
            ResetLevel();
            mainMenuCanvas.enabled = false;
        }

        public void FinishRun()
        {
            if(finishRunCo == null)
                finishRunCo = StartCoroutine(FinishRunCo());
        }
        
        private IEnumerator FinishRunCo()
        {
            OnFinishRun?.Invoke();
            yield return new WaitForSeconds(2);
            yield return StartCoroutine(UIManager.Instance.FadeInScreenCover());
            ResetLevel();
            yield return StartCoroutine(UIManager.Instance.FadeOutScreenCover());

            finishRunCo = null;
        }

        public void ResetLevel()
        {
            OnReset?.Invoke();
            StaticGameStats.RoundScore = 0;
        }
        public delegate void FinishRunDelegate();
        public event FinishRunDelegate OnFinishRun;
        public delegate void ResetDelegate();
        public event ResetDelegate OnReset;
    }
}