using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using RobbieWagnerGames.Common;
using TMPro;
using AYellowpaper.SerializedCollections;

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

        [TextArea] public string itemDescription;

        [HideInInspector] public bool canInteract = false;

        private void Awake()
        {
            //button.onClick.AddListener(PurchaseItem);

            costText.text = shopItems[0].cost.ToString();

            CheckColor();

            DISABLED_COLOR = new Color(1,1,1,0.5f);
            icon.sprite = shopItems[0].effects[0].icon;
            ShopMenu.Instance.OnOpenMenu  += CheckColor;
            ShopMenu.Instance.OnUpdateShop += CheckColor;
        }

        private void CheckColor()
        {
            if(currentItemIndex < shopItems.Count) 
            {
                costText.text = shopItems[currentItemIndex].cost.ToString();
                currencyIcon.sprite = ShopMenu.Instance.currencyIcons[shopItems[currentItemIndex].currencyType];

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

        public override void NavigateTo()
        {
            icon.color = Color.white;
            currencyIcon.color = Color.white;
            background.color = Color.white;
            costText.color = 
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
            if(canInteract && currentItemIndex < shopItems.Count)
            {
                bool purchased = shopItems[currentItemIndex].Buy();

                if(purchased)
                {
                    currentItemIndex++;
                    CheckColor();
                    ShopMenu.Instance.UpdateShop();
                    BasicAudioManager.Instance.PlayAudioSource(AudioSourceName.Purchase);
                }
                else
                {
                    BasicAudioManager.Instance.PlayAudioSource(AudioSourceName.UIFail);
                }
            }
        }
    }
}