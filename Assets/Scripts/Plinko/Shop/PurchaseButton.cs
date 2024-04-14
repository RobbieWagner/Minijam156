using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using RobbieWagnerGames.Common;
using TMPro;

namespace RobbieWagnerGames.Plinko
{
    public class PurchaseButton : MenuButton
    {
        //[SerializeField] private Button button;
        [SerializeField] public List<PurchaseItem> shopItems;
        [SerializeField] private int currentItemIndex = 0;
        [HideInInspector] public Shop shop;

        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private Image icon;
        [SerializeField] private Image currencyIcon;
        [SerializeField] private Image background;
        private Color DISABLED_COLOR;

        [HideInInspector] public bool canInteract = false;

        private void Awake()
        {
            //button.onClick.AddListener(PurchaseItem);

            costText.text = shopItems[0].cost.ToString();

            if(StaticGameStats.GetCurrencyValue(shopItems[0].currencyType) >= shopItems[0].cost)
            {
                costText.color = StaticGameStats.GetCurrencyColor(shopItems[0].currencyType);
            }
            else costText.color = Color.black;

            DISABLED_COLOR = new Color(1,1,1,0.5f);
            icon.sprite = shopItems[0].effects[0].icon;
        }

        public override void NavigateTo()
        {
            icon.color = Color.white;
            currencyIcon.color = Color.white;
            background.color = Color.white;
            costText.color = new Color(costText.color.r, costText.color.g, costText.color.b, 1f);
            canInteract = true;
        }

        public override void NavigateAway()
        {
            icon.color = DISABLED_COLOR;
            currencyIcon.color = DISABLED_COLOR;
            background.color = DISABLED_COLOR;
            costText.color = new Color(costText.color.r, costText.color.g, costText.color.b, .5f);
            canInteract = false;
        }

        public override IEnumerator SelectButton(Menu menu)
        {
            yield return StartCoroutine(base.SelectButton(menu));
            if(canInteract)
            {
                bool purchased = shopItems[currentItemIndex].Buy();

                if(purchased)
                {
                    Debug.Log("purchase");
                    if(currentItemIndex < shopItems.Count - 1) 
                    {
                        currentItemIndex++;
                        costText.text = shopItems[currentItemIndex].cost.ToString();

                        if(StaticGameStats.GetCurrencyValue(shopItems[currentItemIndex].currencyType) >= shopItems[currentItemIndex].cost)
                        {
                            costText.color = StaticGameStats.GetCurrencyColor(shopItems[currentItemIndex].currencyType);
                        }
                        else costText.color = Color.black;
                    }
                    else 
                    {
                        canInteract = false;
                        costText.text = "MAX";
                        costText.color = Color.black;
                    }
                }
            }
        }
    }
}