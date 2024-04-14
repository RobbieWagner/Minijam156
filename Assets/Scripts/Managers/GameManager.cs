using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RobbieWagnerGames.Managers;
using RobbieWagnerGames.Common;
using System;
using UnityEngine.UIElements;

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
        [HideInInspector] public float eventChance = 1;

        [SerializeField] private List<SpecialEvent> specialEvents;
        
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
            eventChance = 1;
        }

        private void StartGame(InputAction.CallbackContext context)
        {
            //playerControls.Disable();
            if(!startedGame)
            {
                startedGame = true;
                ResetLevel();
                mainMenuCanvas.enabled = false;
                DropperBall.Instance.OnLeaveState += CheckIfWasWaiting;
            }
            
        }

        private void CheckIfWasWaiting(DropState state)
        {
            if(state == DropState.WAITING)
            {
                if(UnityEngine.Random.Range(0, 100) < eventChance)
                {
                    SpawnEvent();
                }
            }
        }

        private void SpawnEvent()
        {
            Instantiate(specialEvents[UnityEngine.Random.Range(0, specialEvents.Count)]);
        }

        public void FinishRun()
        {
            if(finishRunCo == null)
                finishRunCo = StartCoroutine(FinishRunCo());
        }
        
        private IEnumerator FinishRunCo()
        {
            OnFinishRun?.Invoke();
            yield return new WaitForSeconds(1);
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