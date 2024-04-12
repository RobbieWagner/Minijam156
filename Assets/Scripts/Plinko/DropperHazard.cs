using UnityEngine;

namespace RobbieWagnerGames.Plinko
{
    public class DropperHazard : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            DropperBall ball = other.collider.GetComponentInChildren<DropperBall>();

            if (ball != null)
            {
                ball.ChangeDropState(DropState.FINISHED);
            }   
        }
    }
}