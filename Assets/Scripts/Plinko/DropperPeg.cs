using UnityEngine;
using RobbieWagnerGames.Common;
using System.Collections;
using UnityEngine.Experimental.GlobalIllumination;
using System.Numerics;

namespace RobbieWagnerGames.Plinko
{
    public class DropperPeg : MonoBehaviour
    {
        protected Coroutine currentBumpCo = null;

        [Header("Rendering")]
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] private UnitAnimator unitAnimator;

        [Header("Mechanics")]
        [SerializeField] protected int pointValue = 100;
        [SerializeField] protected int leafGain = 5; //TODO decide if gain all leaves or x number of leaves per bounce
        [SerializeField] protected bool bouncedOnce = false;
        
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private Collider2D coll;
        [SerializeField] private bool canFall = true;

        private int bumps = 0;

        protected virtual void Awake()
        {
            unitAnimator.ChangeAnimationState(UnitAnimationState.Idle);
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            DropperBall ball = other.collider.GetComponentInChildren<DropperBall>();

            if(ball != null)
            {
                ball.unitAnimator.ChangeAnimationState(UnitAnimationState.Falling);
                bumps++;

                if(currentBumpCo == null)
                    currentBumpCo = StartCoroutine(BumpCo());

                if(!bouncedOnce)
                {
                    bouncedOnce = true;
                    StaticGameStats.EffectScore(ScoreType.LEAVES, leafGain);
                    StaticGameStats.EffectScore(ScoreType.SCORE, pointValue);
                }
                else
                {
                    StaticGameStats.EffectScore(ScoreType.SCORE, 5);
                    StaticGameStats.EffectScore(ScoreType.LEAVES, 1);
                }
            }  
        }

        protected virtual IEnumerator BumpCo()
        {
            unitAnimator.ChangeAnimationState(UnitAnimationState.Bumping);
            BasicAudioManager.Instance.PlayAudioSource(AudioSourceName.Bounce);
            if(CheckBumpLimit())
                StartCoroutine(FallOffCo());
            else
            {
                yield return new WaitForSeconds(1f);
                if(CheckBumpLimit())
                    StartCoroutine(FallOffCo());
                else unitAnimator.ChangeAnimationState(UnitAnimationState.Idle);
            }
            currentBumpCo = null;
        }

        private bool CheckBumpLimit()
        {
            return bumps >= StaticGameStats.bumpLimit && canFall;
        }

        private IEnumerator FallOffCo()
        {
            unitAnimator.ChangeAnimationState(UnitAnimationState.Falling);
            coll.enabled = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            spriteRenderer.sortingOrder = -1;
            yield return null;
        }
        
    }
}

