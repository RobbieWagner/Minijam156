using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace RobbieWagnerGames.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] public Canvas screenCoverCanvas;
        [SerializeField] public Image screenCoverImage;

        public static UIManager Instance {get; private set;}

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

            screenCoverCanvas.enabled = false;
        }

        public IEnumerator FadeInScreenCover()
        {
            screenCoverImage.color = Color.clear;
            screenCoverCanvas.enabled = true;
            yield return screenCoverImage.DOColor(Color.black, 1).SetEase(Ease.Linear).WaitForCompletion();
        }

        public IEnumerator FadeOutScreenCover()
        {
            screenCoverImage.color = Color.black;
            yield return screenCoverImage.DOColor(Color.clear, 1).SetEase(Ease.Linear).WaitForCompletion();
            screenCoverCanvas.enabled = false;
        }
    }
}