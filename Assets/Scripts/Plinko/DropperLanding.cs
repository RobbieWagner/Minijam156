using UnityEngine;

namespace RobbieWagnerGames.Plinko
{
    public class DropperLanding : MonoBehaviour
    {
        [SerializeField] private float landingLength = 6;
        [SerializeField] private float yOffset = .5f;
        [SerializeField] private Collider2D coll;
        [SerializeField] private ScoreType currencyGainType = ScoreType.LEAVES;
        [SerializeField] private int currencyGain = 100;

        private void OnCollisionEnter2D(Collision2D other)
        {
            DropperBall ball = other.collider.GetComponentInChildren<DropperBall>();

            if (ball != null)
            {
                ball.ChangeDropState(DropState.FINISHED);
                StaticGameStats.EffectScore(currencyGainType, currencyGain);
                // ball.transform.position = new Vector2(ball.transform.position.x, transform.position.y + yOffset);
                // ball.Swing((Vector2) transform.position + Vector2.up * yOffset, landingLength);
                // coll.enabled = false;
            }   
        }
    }
}