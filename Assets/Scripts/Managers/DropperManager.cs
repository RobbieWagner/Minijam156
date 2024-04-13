using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RobbieWagnerGames.Plinko
{
    public class DropperManager : MonoBehaviour
    {
        [SerializeField] private List<DropperSegment> unlockedSegments;
        [SerializeField] private List<DropperSegment> currentSegments; //TODO: Remove [serializefield] after testing
        public int middleSegmentCount = 1;
        [SerializeField] private DropperSegment topPrefab;
        [SerializeField] private DropperSegment bottomPrefab;
        [SerializeField] private DropperSegment topInstance;
        [SerializeField] private DropperSegment bottomInstance;

        private float dropperHeight = 0;
        private float topYValue = 0;


        public static DropperManager Instance {get; private set;}

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

            GameManager.Instance.OnReset += ResetDropper;
        }

        private void ResetDropper()
        {
            foreach(DropperSegment segment in currentSegments)
            {
                Destroy(segment.gameObject);
            }
            currentSegments.Clear();
            if(topInstance != null) 
            {
                Destroy(topInstance.gameObject);
                topInstance = null;
            }
            if(bottomInstance != null)
            {
                Destroy(bottomInstance.gameObject);
                bottomInstance = null;
            }

            BuildLevel();

        }

        private void BuildLevel()
        {
            topInstance = Instantiate(topPrefab, transform);
            bottomInstance = Instantiate(bottomPrefab, transform);

            while(currentSegments.Count < middleSegmentCount && unlockedSegments.Any())
            {
                currentSegments.Add(Instantiate(unlockedSegments[Random.Range(0, unlockedSegments.Count)], transform));
            }

            topInstance.transform.position = Vector2.zero;
            topYValue = topInstance.height / 2;
            dropperHeight = topInstance.height;

            foreach(DropperSegment segment in currentSegments)
            {
                segment.transform.position = Vector2.down * (dropperHeight - topYValue + segment.height/2);
                dropperHeight += segment.height;
            }

            bottomInstance.transform.position = Vector2.down * (dropperHeight - topYValue + bottomInstance.height/2);
            dropperHeight += bottomInstance.height;
        }
    }
}
