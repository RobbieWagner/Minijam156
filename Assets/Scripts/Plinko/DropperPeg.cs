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
        [SerializeField] protected Sprite inactiveSprite;
        [SerializeField] protected Sprite activeSprite;
        [SerializeField] private UnitAnimator unitAnimator;

        [Header("Mechanics")]
        [SerializeField] protected int pointValue = 100;
        [SerializeField] protected int leafGain = 5; //TODO decide if gain all leaves or x number of leaves per bounce
        [SerializeField] protected bool gainedLeaves = false;
        
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private Collider2D coll;

        private int bumps = 0;
        [SerializeField] private int bumpLimit = 2;

        protected virtual void Awake()
        {
            spriteRenderer.sprite = inactiveSprite;
            GameManager.Instance.OnReset += Reset;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        protected virtual void OnDestroy()
        {
            GameManager.Instance.OnReset -= Reset;
        }

        protected virtual void Reset()
        {
            gainedLeaves = false;
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

                StaticGameStats.RoundScore += pointValue;
                if(!gainedLeaves)
                {
                    gainedLeaves = true;
                    StaticGameStats.Leaves += leafGain;
                }
            }  
        }

        protected virtual IEnumerator BumpCo()
        {
            spriteRenderer.sprite = activeSprite;
            if(CheckBumpLimit())
                StartCoroutine(FallOffCo());
            else
            {
                yield return new WaitForSeconds(1f);
                if(CheckBumpLimit())
                    StartCoroutine(FallOffCo());
                else spriteRenderer.sprite = inactiveSprite;
            }
            currentBumpCo = null;
        }

        private bool CheckBumpLimit()
        {
            Debug.Log($"bumps {bumps}");
            return bumps >= bumpLimit;
        }

        private IEnumerator FallOffCo()
        {
            Debug.Log("Fall Off");
            unitAnimator.ChangeAnimationState(UnitAnimationState.Falling);
            coll.enabled = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            spriteRenderer.sortingOrder = -1;
            yield return null;
        }
        
    }
}

