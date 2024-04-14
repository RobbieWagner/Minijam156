using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RobbieWagnerGames.Plinko
{
    public class DropperManager : MonoBehaviour
    {
        public List<DropperSegment> unlockedSegments;
        private List<DropperSegment> currentSegments;
        private int middleSegmentCount = 1;
        public int MiddleSegmentCount
        {
            get { return middleSegmentCount; }
            set
            {
                if(middleSegmentCount == value)
                    return;

                middleSegmentCount = value;
                if(DropperBall.Instance.currentDropState != DropState.FALLING && DropperBall.Instance.currentDropState != DropState.FLOATING)
                {
                    ResetDropper();
                }
            }
        }
        [SerializeField] private DropperSegment topPrefab;
        [SerializeField] private DropperSegment bottomPrefab;
        [SerializeField] private DropperSegment topInstance;
        [SerializeField] private DropperSegment bottomInstance;
        [SerializeField] public PhysicsMaterial2D bounceMat;
        [HideInInspector] public int specialRowChance = 5;
        public float initialBounciness = .75f;

        [HideInInspector] public float dropperHeight = 0;
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
            currentSegments = new List<DropperSegment>();
            bounceMat.bounciness = initialBounciness;

            SortDropperSegments();
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

            List<DropperSegment> specialRows = unlockedSegments.Where(segment => segment.special).ToList();
            List<DropperSegment> basicRows = unlockedSegments.Where(segment => !segment.special).ToList();

            while(currentSegments.Count < middleSegmentCount && unlockedSegments.Any())
            {
                if(specialRows.Any() && Random.Range(0, 100) < specialRowChance)
                    currentSegments.Add(Instantiate(specialRows[Random.Range(0, specialRows.Count)], transform));
                else
                    currentSegments.Add(Instantiate(basicRows[Random.Range(0, basicRows.Count)], transform));
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

        public void SortDropperSegments()
        {
            unlockedSegments = unlockedSegments.OrderBy(segment => segment.special).ToList();
        }
    }
}
