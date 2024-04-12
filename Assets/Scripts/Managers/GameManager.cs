using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RobbieWagnerGames.Plinko
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {get; private set;}
        public static string persistentDataPath = "";
        [SerializeField] private Canvas mainMenuCanvas;
        private PlayerControls playerControls;
        
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
            mainMenuCanvas.enabled = false;
            ResetLevel();
        }

        public void ResetLevel()
        {
            OnReset?.Invoke();
            StaticGameStats.RoundScore = 0;
        }
        public delegate void ResetDelegate();
        public event ResetDelegate OnReset;
    }
}