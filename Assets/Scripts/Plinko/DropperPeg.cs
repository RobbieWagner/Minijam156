using UnityEngine;
using RobbieWagnerGames.Common;
using System.Collections;

namespace RobbieWagnerGames.Plinko
{
    public class DropperPeg : MonoBehaviour
    {
        protected Coroutine currentBumpCo = null;

        [Header("Rendering")]
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Sprite inactiveSprite;
        [SerializeField] protected Sprite activeSprite;

        [Header("Mechanics")]
        [SerializeField] protected int pointValue = 100;
        [SerializeField] protected int leafGain = 5; //TODO decide if gain all leaves or x number of leaves per bounce
        [SerializeField] protected bool gainedLeaves = false;
        protected virtual void Awake()
        {
            spriteRenderer.sprite = inactiveSprite;
            GameManager.Instance.OnReset += Reset;
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
            yield return new WaitForSeconds(1f);
            spriteRenderer.sprite = inactiveSprite;
            currentBumpCo = null;
        }
    }
}

