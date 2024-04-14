using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobbieWagnerGames.Common
{
    public enum UnitAnimationState
    {
        // movement
        Idle = 0,
        Floating = 1,
        FallStart = 2,
        Falling = 3,
        Bumping = 4

    }

    public class UnitAnimator : MonoBehaviour
    {

        [SerializeField] public Animator animator;

        [SerializeField] private List<UnitAnimationState> states;
        private UnitAnimationState currentState;

        protected virtual void Awake()
        {
            OnAnimationStateChange += StartAnimation;
            ChangeAnimationState(UnitAnimationState.Idle);
        }

        public void ChangeAnimationState(UnitAnimationState state)
        {
            if(state != currentState && states.Contains(state)) 
            {
                currentState = state;
                
                OnAnimationStateChange?.Invoke(state);
            }
            else if(state != currentState)
            {
                Debug.Log("Animation Clip Not Set Up For Unit");
            }
        }

        public delegate void OnAnimationStateChangeDelegate(UnitAnimationState state);
        public event OnAnimationStateChangeDelegate OnAnimationStateChange;

        public UnitAnimationState GetAnimationState()
        {
            return currentState;
        }

        protected void StartAnimation(UnitAnimationState state)
        {
            animator.Play(state.ToString());
        }
    }
}