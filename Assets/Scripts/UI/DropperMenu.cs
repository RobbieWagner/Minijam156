using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobbieWagnerGames.Common;
using UnityEngine.InputSystem;
//using UnityEditor.PackageManager.UI;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.Rendering;
using RobbieWagnerGames.Managers;

namespace RobbieWagnerGames.Plinko
{
    public class DropperMenu : Menu
    {
        private DropperMenuControls controls;
        private Vector2 cameraUpdateTranslation;
        private DropperSegment currentSegment;
        private int currentSegmentIndex;
        [SerializeField] private float cameraSpeed = 6;
        [SerializeField] private Image leftArrow;
        [SerializeField] private Image rightArrow;
        [SerializeField] private TextMeshProUGUI currentSegmentText;
        [SerializeField] private Color DISABLED_COLOR;
        public static DropperMenu Instance {get; private set;}
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
            controls = new DropperMenuControls();
            controls.Menu.Navigate.performed += NavigateDroppers;
            controls.Menu.Pan.performed += PanCamera;
            controls.Menu.Pan.canceled += StopCamera;
            cameraUpdateTranslation = Vector2.zero;
            currentSegmentIndex = 0;
        }

        private void NavigateDroppers(InputAction.CallbackContext context)
        {
            float dir = context.ReadValue<float>();
            if (dir > 0)
                currentSegmentIndex = Math.Clamp(currentSegmentIndex + 1, 0, DropperManager.Instance.unlockedSegments.Count-1);
            else if (dir < 0)
                currentSegmentIndex = Math.Clamp(currentSegmentIndex - 1, 0, DropperManager.Instance.unlockedSegments.Count-1);
        
            ChangeCurrentSegment(DropperManager.Instance.unlockedSegments[currentSegmentIndex]);
        }

        private void PanCamera(InputAction.CallbackContext context)
        {
            float dir = context.ReadValue<float>();

            cameraUpdateTranslation = Vector2.up * dir;
        }

        private void StopCamera(InputAction.CallbackContext context)
        {
            cameraUpdateTranslation = Vector2.zero;
        }

        private void Update()
        {
            if(isOn)
            {
                Camera.main.transform.Translate(cameraUpdateTranslation * Time.deltaTime * cameraSpeed);
                Camera.main.transform.position = new Vector3(
                                                Camera.main.transform.position.x, 
                                                Mathf.Clamp(Camera.main.transform.position.y, -currentSegment.height/2 + 7, currentSegment.height/2 - 7),
                                                Camera.main.transform.position.z);

            }
        }

        private void ChangeCurrentSegment(DropperSegment segment)
        {
            if(currentSegment != null)
                Destroy(currentSegment.gameObject);
            currentSegment = Instantiate(segment);
            currentSegment.transform.position = SampleLocation;

            leftArrow.color = Color.white;
            rightArrow.color = Color.white;
            if (currentSegmentIndex == 0)
                leftArrow.color = DISABLED_COLOR;
            else if(currentSegmentIndex == DropperManager.Instance.unlockedSegments.Count-1)
                rightArrow.color = DISABLED_COLOR;

            currentSegmentText.text = $"{currentSegmentIndex+1}/{DropperManager.Instance.unlockedSegments.Count}";
        }
        
        [SerializeField] private Vector2 SampleLocation;
        public override void SetupMenu()
        {
            if(DropperBall.Instance.currentDropState == DropState.WAITING)
            {
                base.SetupMenu();
                Camera.main.transform.position = (Vector3) SampleLocation + new Vector3(0,0,-10);
                OnOpenMenu?.Invoke();
                ChangeCurrentSegment(DropperManager.Instance.unlockedSegments[currentSegmentIndex]);
                controls.Enable();
                ScoreCanvas.Instance.canvas.enabled = false;
                UIManager.Instance.DisableStandingUI();
            }
        }
        public delegate void MenuDelegate();
        public event MenuDelegate OnOpenMenu;

        public override void DisableMenu(bool returnToPreviousMenu = true)
        {
            DropperBall.Instance.ResetCamera();
            controls.Disable();
            ScoreCanvas.Instance.canvas.enabled = true;
            UIManager.Instance.EnableStandingUI();
            OnCloseMenu?.Invoke();
            base.DisableMenu(returnToPreviousMenu);
        }
        public event MenuDelegate OnCloseMenu;
    }
}
