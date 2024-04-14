using System;
using System.Collections;
using UnityEngine;

namespace RobbieWagnerGames.Plinko
{
    public class SpecialEvent : MonoBehaviour
    {
        [SerializeField] private Pickup pickup;

        private void Awake()
        {
            GameManager.Instance.OnReset += DestroyEvent;
            StartCoroutine(EventCo());
        }

        private void DestroyEvent()
        {
            GameManager.Instance.OnReset -= DestroyEvent;
            Destroy(gameObject);
        }

        private IEnumerator EventCo()
        {
            bool middle = false;

            SpawnPickup(new Vector2(-3, -DropperManager.Instance.dropperHeight + 12.5f), Vector2.up);
            SpawnPickup(new Vector2(3, -DropperManager.Instance.dropperHeight + 12.5f), Vector2.up);
            SpawnPickup(new Vector2(0, -DropperManager.Instance.dropperHeight + 12.5f), Vector2.up);
            SpawnPickup(new Vector2(-7, -DropperManager.Instance.dropperHeight + 12.5f), Vector2.up);
            SpawnPickup(new Vector2(7, -DropperManager.Instance.dropperHeight + 12.5f), Vector2.up);
            SpawnPickup(new Vector2(-3, -DropperManager.Instance.dropperHeight + 25f), Vector2.up);
            SpawnPickup(new Vector2(3, -DropperManager.Instance.dropperHeight + 25f), Vector2.up);
            SpawnPickup(new Vector2(0, -DropperManager.Instance.dropperHeight + 25f), Vector2.up);
            SpawnPickup(new Vector2(-7, -DropperManager.Instance.dropperHeight + 25f), Vector2.up);
            SpawnPickup(new Vector2(7, -DropperManager.Instance.dropperHeight + 25f), Vector2.up);

            while(true)
            {
                if(!middle)
                {
                    SpawnPickup(new Vector2(-3, -DropperManager.Instance.dropperHeight), Vector2.up);
                    SpawnPickup(new Vector2(3, -DropperManager.Instance.dropperHeight), Vector2.up);
                }
                else
                {
                    SpawnPickup(new Vector2(0, -DropperManager.Instance.dropperHeight), Vector2.up);
                    SpawnPickup(new Vector2(-7, -DropperManager.Instance.dropperHeight), Vector2.up);
                    SpawnPickup(new Vector2(7, -DropperManager.Instance.dropperHeight), Vector2.up);
                }
                yield return new WaitForSeconds(2f);
                middle = !middle;
            }
        }

        private void SpawnPickup(Vector2 location, Vector2 dir)
        {
            Pickup newPickup = Instantiate(pickup, transform);
            newPickup.transform.position = location;
            newPickup.dir = dir;
        }
    }
}