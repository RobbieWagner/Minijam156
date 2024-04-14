using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using RobbieWagnerGames.Common;
using RobbieWagnerGames.Managers;
using DG.Tweening;
using UnityEditor.Rendering;

namespace RobbieWagnerGames.Plinko
{
    public enum DropState
    {
        NONE = -1,
        WAITING = 0,
        FALLING = 1,
        FLOATING = 2,
        STOPPED = 3,
        FINISHED = 4,
        PAUSED = 5
    }

    public class DropperBall : MonoBehaviour
    {
        [HideInInspector] public DropState currentDropState = DropState.NONE;

        [Header("General")]
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private Collider2D coll;
        [SerializeField] private float maxSpeed = 25;
        [SerializeField] private Vector2 playerMoveVector;
        public float inputSpeed = 5f;
        public float glideSpeed = 5f;
        private Tweener stopTween;
        [SerializeField] private Vector3 cameraOffset;
        [Header("Glider")]
        public float glideInputSpeed = 5f;
        public float maxGlideTime = 2f;
        private float glideTime = 2f;
        public float GlideTime
        {
            get { return glideTime; }
            set 
            { 
                if (glideTime == value)
                    return;
                glideTime = value;
                OnGlideTimeChanged(glideTime);
            }
        }
        public delegate void GlideTimeDelegate(float time);
        public event GlideTimeDelegate OnGlideTimeChanged;
        public float gliderCooldown = 5f;
        private bool canGlide = true;
        [SerializeField] private SpriteRenderer gliderSprite;
        
        [Header("Swinging")]
        [SerializeField] private Vector2 startPos;
        [SerializeField] private float startLandingLength = 6;

        [Header("Animation")]
        public UnitAnimator unitAnimator;

        private PlayerControls playerControls;

        public static DropperBall Instance {get; private set;}

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

            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            playerControls = new PlayerControls();
            playerControls.Dropper.DropBall.performed += DropBall;
            playerControls.Dropper.Float.performed += ToggleFloat;
            playerControls.Dropper.Move.performed += Move;
            playerControls.Dropper.Move.canceled += Stop;
            //playerControls.Dropper.Enable();
            GameManager.Instance.OnReset += Reset;
            GameManager.Instance.OnFinishRun += FinishRun;
            DisableControls();
            //ChangeDropState(DropState.WAITING);

            PauseMenu.Instance.OnOpenMenu += DisableControls;
            PauseMenu.Instance.OnCloseMenu += ResetControlSetup;
            DropperMenu.Instance.OnOpenMenu += DisableControls;
            DropperMenu.Instance.OnCloseMenu += ResetControlSetup;
            ShopMenu.Instance.OnOpenMenu += DisableControls;
            ShopMenu.Instance.OnCloseMenu += ResetControlSetup;

            gliderSprite.enabled = false;
            playerMoveVector = Vector2.zero;
        }

        private void Stop(InputAction.CallbackContext context)
        {
            switch(currentDropState)
            {
                case DropState.FLOATING:
                case DropState.FALLING:
                stopTween = DOTween.To(()=> playerMoveVector, x=>playerMoveVector = x, Vector2.zero, 1);
                break;
                default:
                break;
            }
        }
        
        private void Stop()
        {
            playerMoveVector = Vector2.zero;
            rb2d.velocity = Vector2.zero;
        }

        private void Move(InputAction.CallbackContext context)
        {
            playerMoveVector = context.ReadValue<Vector2>();
        }

        private void FinishRun()
        {
            StopFalling();
            coll.enabled = false;
        }

        private void Update()
        {
            if(currentDropState != DropState.WAITING)
                Camera.main.transform.position = new Vector3(0, transform.position.y, 0) +  cameraOffset;
            if(currentDropState == DropState.FLOATING)
            {
                GlideTime -= Time.deltaTime;
                if(GlideTime <= .01f)
                    ChangeDropState(DropState.FALLING);
            }
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);

            if(currentDropState == DropState.FLOATING)
                rb2d.AddForce(playerMoveVector * glideSpeed * Time.deltaTime);
            else if(currentDropState == DropState.FALLING)
                rb2d.AddForce(playerMoveVector * inputSpeed * Time.deltaTime);
            //rb2d.MovePosition(rb2d.position + playerMoveVector * Time.deltaTime);
        }

        private void EnableControls() => playerControls.Dropper.Enable();

        private void DisableControls() => playerControls.Dropper.Disable();

        private void ResetControlSetup()
        {
            //Debug.Log($"drop state :{currentDropState}");
            switch(currentDropState)
            {
                case DropState.WAITING:
                EnableControls();
                break;
                default:
                break;
            }
        }

        public void ResetCamera()
        {
            Camera.main.transform.position = (Vector3.up * transform.position.y) + cameraOffset;
        }

        public void ChangeDropState(DropState newDropState)
        {
            if(newDropState == currentDropState)
                return;
            switch(currentDropState)
            {
                case DropState.FLOATING:
                ExitFloatState();
                break;
                default:
                break;
            }
            currentDropState = newDropState;
            switch(currentDropState)
            {
                case DropState.FALLING:
                EnterFallState();
                break;
                case DropState.FLOATING:
                EnterFloatState();
                break;
                case DropState.PAUSED:
                EnterPauseState();
                break;
                case DropState.FINISHED:
                GameManager.Instance.FinishRun();
                break;
                default:
                break;
            }

            OnChangeDropState?.Invoke(newDropState);
        }
        public delegate void ChangeDropStateDelegate(DropState state);
        public event ChangeDropStateDelegate OnChangeDropState;

        private void EnterPauseState()
        {
            rb2d.velocity *= 0;
            StopFalling();
        }

        public void Reset()
        {
            StopFalling();
            transform.position = startPos;
            Camera.main.transform.position = new Vector3(0, transform.position.y, 0) +  cameraOffset;
            ChangeDropState(DropState.WAITING);
            StartCoroutine(SwingCo(startPos, startLandingLength));
            GlideTime = maxGlideTime;
        }

        public void StopFalling()
        {
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            unitAnimator.ChangeAnimationState(UnitAnimationState.Floating);
            Stop();
            EnableControls();
        }

        public void Swing(Vector2 middle, float landingLength)
        {
            StartCoroutine(SwingCo(middle, landingLength));
        }

        private IEnumerator SwingCo(Vector2 middle, float landingLength)
        {
            bool swingRight = true;
            Vector2 maxPos = middle + (Vector2.right * landingLength/2);
            Vector2 minPos = middle + (Vector2.left * landingLength/2);
            float speed = 3f;
            
            while(currentDropState == DropState.WAITING || currentDropState == DropState.PAUSED)
            {
                if(swingRight)
                {
                    transform.position = Vector2.MoveTowards(transform.position, maxPos, speed * Time.deltaTime);
                    yield return null;
                    if(Vector2.Distance(transform.position, maxPos) < .01f)
                        swingRight = false;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, minPos, speed * Time.deltaTime);
                    yield return null;
                    if(Vector2.Distance(transform.position, minPos) < .01f)
                        swingRight = true;
                }
            }
        }

        private void DropBall(InputAction.CallbackContext context)
        {
            if(currentDropState == DropState.WAITING || currentDropState == DropState.FLOATING || currentDropState == DropState.PAUSED)
            {
                ChangeDropState(DropState.FALLING);
                coll.enabled = true;
            }
        }

        private void EnterFallState()
        {
            rb2d.gravityScale = 1;
            unitAnimator.ChangeAnimationState(UnitAnimationState.FallStart);
        }

        private void ToggleFloat(InputAction.CallbackContext context)
        {
            switch(currentDropState)
            {
                case DropState.FALLING:
                ChangeDropState(DropState.FLOATING);
                break;
                case DropState.FLOATING:
                ChangeDropState(DropState.FALLING);
                break;
                default:
                break;
            }
        }

        private void EnterFloatState()
        {
            if(canGlide)
            {
                rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, 3);
                rb2d.gravityScale = .2f;
                gliderSprite.enabled = true;
                unitAnimator.ChangeAnimationState(UnitAnimationState.Floating);
            }
            else
                ChangeDropState(DropState.FALLING);
        }

        private void ExitFloatState()
        {
            gliderSprite.enabled = false;
            if(canGlide)
                StartCoroutine(CooldownFloatState());
        }

        private IEnumerator CooldownFloatState()
        {
            canGlide = false;
            OnCooldown?.Invoke(gliderCooldown - .05f);
            yield return new WaitForSeconds(gliderCooldown);
            canGlide = true;
            GlideTime = maxGlideTime;
        }
        public delegate void CooldownDelegate(float cooldownTime);
        public event CooldownDelegate OnCooldown;
    }
}

