using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace RobbieWagnerGames.Plinko
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Collider2D col;
        [SerializeField] private ScoreType scoreType = ScoreType.LEAVES;
        [SerializeField] private int value = 50;
        public Vector2 dir;
        public float speed = 5;

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<DropperBall>() != null && (DropperBall.Instance.currentDropState == DropState.FALLING || DropperBall.Instance.currentDropState == DropState.FLOATING))
            {
                StaticGameStats.EffectScore(scoreType, value);
                StartCoroutine(PhaseOut());
                col.enabled = false;
            }
        }

        private void Update()
        {
            transform.position = (Vector2) transform.position + (dir * speed * Time.deltaTime);
        }

        public IEnumerator PhaseOut()
        {
            yield return spriteRenderer.DOColor(Color.clear, 2f).SetEase(Ease.Linear).WaitForCompletion();
            Destroy(gameObject);
        }
    }
}