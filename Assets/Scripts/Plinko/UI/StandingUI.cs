using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace RobbieWagnerGames.Plinko
{
    [RequireComponent(typeof(RectTransform))]
    public class StandingUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        public Vector2 onPosition;
        public Vector2 offPosition;

        public void EnableUI()
        {
            canvas.enabled = true;
        }

        public void DisableUI()
        {
            canvas.enabled = false;
        }
    }
}