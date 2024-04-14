using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RobbieWagnerGames.Managers;
using RobbieWagnerGames.Common;

namespace RobbieWagnerGames.Plinko
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {get; private set;}
        public static string persistentDataPath = "";
        [SerializeField] private Canvas mainMenuCanvas;
        private PlayerControls playerControls;
        public bool paused = false;
        private bool canPause => 
            !DropperMenu.Instance.isOn 
            && !ShopMenu.Instance.isOn
            && startedGame;
        public bool startedGame = false;
        
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
            playerControls.Menu.PauseGame.performed += PauseGame;
            playerControls.Enable();
            mainMenuCanvas.enabled = true;
        }

        private void StartGame(InputAction.CallbackContext context)
        {
            //playerControls.Disable();
            if(!startedGame)
            {
                startedGame = true;
                ResetLevel();
                mainMenuCanvas.enabled = false;
            }
            
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
            StaticGameStats.ResetRoundScores();
        }

        public void PauseGame(InputAction.CallbackContext context)
        {
            if(canPause && !paused)
            {
                TogglePause(true);
            }
            else if(canPause && paused)
            {
                TogglePause(false);
            }
        }

        public void TogglePause(bool on)
        {
            if(on)
            {
                Time.timeScale = 0;
                PauseMenu.Instance.SetupMenu();
            }
            else
            {
                Time.timeScale = 1;
                PauseMenu.Instance.DisableMenu();
            }
        }

        public void ResumeGame()
        {
            TogglePause(false);
        }

        public delegate void FinishRunDelegate();
        public event FinishRunDelegate OnFinishRun;
        public delegate void ResetDelegate();
        public event ResetDelegate OnReset;
    }
}