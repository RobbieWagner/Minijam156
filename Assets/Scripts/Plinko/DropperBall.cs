using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using RobbieWagnerGames.Common;
using RobbieWagnerGames.Managers;
using DG.Tweening;

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
        private DropState currentDropState = DropState.NONE;

        [Header("General")]
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private Collider2D coll;
        [SerializeField] private Vector3 cameraOffset;
        
        [Header("Swinging")]
        [SerializeField] private Vector2 startPos;
        [SerializeField] private float startLandingLength = 6;

        [Header("Animation")]
        public UnitAnimator unitAnimator;

        private PlayerControls playerControls;

        private Coroutine finishRunCo = null;

        private void Awake()
        {
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            playerControls = new PlayerControls();
            playerControls.Dropper.DropBall.performed += DropBall;
            //playerControls.Dropper.Enable();
            GameManager.Instance.OnReset += Reset;
            DisableControls();
            //ChangeDropState(DropState.WAITING);
        }

        private void EnableControls() => playerControls.Dropper.Enable();

        private void DisableControls() => playerControls.Dropper.Disable();

        public void ChangeDropState(DropState newDropState)
        {
            if(newDropState == currentDropState)
                return;

            currentDropState = newDropState;
            switch(currentDropState)
            {
                case DropState.FALLING:
                EnterFallState();
                break;
                case DropState.PAUSED:
                EnterPauseState();
                break;
                case DropState.FINISHED:
                FinishRun();
                break;
                default:
                break;
            }
        }

        private void FinishRun()
        {
            if(finishRunCo == null)
                finishRunCo = StartCoroutine(FinishRunCo());
        }
        
        private IEnumerator FinishRunCo()
        {
            StopFalling();
            rb2d.velocity = new Vector2(0, -5);
            coll.enabled = false;
            rb2d.gravityScale = 1;
            Camera.main.transform.SetParent(null);
            yield return new WaitForSeconds(2);
            yield return StartCoroutine(UIManager.Instance.FadeInScreenCover());
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = cameraOffset;
            Reset();
            coll.enabled = true;
            yield return StartCoroutine(UIManager.Instance.FadeOutScreenCover());

            finishRunCo = null;
        }

        private void EnterPauseState()
        {
            rb2d.velocity *= 0;
            StopFalling();
        }

        private void Reset()
        {
            StopFalling();
            transform.position = startPos;
            ChangeDropState(DropState.WAITING);
            StartCoroutine(SwingCo(startPos, startLandingLength));
            Debug.Log("reset");
        }

        private void StopFalling()
        {
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            unitAnimator.ChangeAnimationState(UnitAnimationState.Floating);
            EnableControls();
        }

        public void Swing(Vector2 middle, float landingLength)
        {
            StartCoroutine(SwingCo(middle, landingLength));
        }

        private IEnumerator SwingCo(Vector2 middle, float landingLength)
        {
            Camera.main.transform.SetParent(null);
            yield return Camera.main.transform.DOMove((Vector3) middle + cameraOffset, .5f).SetEase(Ease.Linear).WaitForCompletion();
            bool swingRight = true;
            Vector2 maxPos = middle + (Vector2.right * landingLength/2);
            Vector2 minPos = middle + (Vector2.left * landingLength/2);
            float speed = 3f;
            Debug.Log("swing");
            
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
                Debug.Log("fall");
                StartCoroutine(ParentCameraToBall());
            }
        }

        private IEnumerator ParentCameraToBall()
        {
            yield return Camera.main.transform.DOMove(transform.position + cameraOffset, .5f).SetEase(Ease.Linear).WaitForCompletion();
            Camera.main.transform.SetParent(transform);
        }

        private void EnterFallState()
        {
            rb2d.gravityScale = 1;
            unitAnimator.ChangeAnimationState(UnitAnimationState.FallStart);
        }
    }
}

