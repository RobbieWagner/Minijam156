using System.Collections;
using DG.Tweening;
using RobbieWagnerGames.Plinko;
using UnityEngine;
using UnityEngine.UI;

namespace RobbieWagnerGames.Plinko
{
    public class GliderUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Slider slider;
        [SerializeField] private Image sliderFG;
        private Coroutine cooldownCo;

        private void Awake()
        {
            DropperBall.Instance.OnGlideTimeChanged += UpdateGlideTimeSlider;
            DropperBall.Instance.OnCooldown += CooldownGlide;
            DropperBall.Instance.OnChangeDropState += ChangeDropState;
        }

        private void ChangeDropState(DropState state)
        {
            switch(state)
            {
                case DropState.FALLING:
                case DropState.FLOATING:
                canvas.enabled = true;
                break;
                default:
                canvas.enabled = false;
                break;
            }
        }

        private void UpdateGlideTimeSlider(float glideTime)
        {
            if (cooldownCo == null)
            {
                slider.maxValue = DropperBall.Instance.maxGlideTime;
                slider.value = glideTime;
            }
        }

        private void CooldownGlide(float cooldownTime)
        {
            if(cooldownCo == null)
                cooldownCo = StartCoroutine(CooldownCo(cooldownTime));
        }

        private IEnumerator CooldownCo(float cooldownTime)
        {
            sliderFG.color = new Color(sliderFG.color.r, sliderFG.color.g, sliderFG.color.b, .2f);
            yield return slider.DOValue(slider.maxValue, cooldownTime).SetEase(Ease.Linear).WaitForCompletion();
            sliderFG.color = new Color(sliderFG.color.r, sliderFG.color.g, sliderFG.color.b, 1f);
            cooldownCo = null;
        }
    }
}