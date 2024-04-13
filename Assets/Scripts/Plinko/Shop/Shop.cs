using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

namespace RobbieWagnerGames.Plinko
{

    public class Shop : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [HideInInspector] public bool shopDisplayed = false;
        private List<PurchaseButton> currentDisplayedShopItems;
        private List<PurchaseButton> shopItems;
        [SerializeField] private List<PurchaseButton> initialShopItems;
        [HideInInspector] public bool hasEnteredShop;

        [SerializeField] private List<VerticalLayoutGroup> shops;

        [SerializeField] private TextAsset firstTimeEnteringDialogue;

        private void Awake()
        {
            shopItems = initialShopItems;
            currentDisplayedShopItems = new List<PurchaseButton>();
            canvas.enabled = false;

            hasEnteredShop = false;
        }

        public void EnterShop()
        {
            foreach(PurchaseButton shopItem in shopItems)
            {
                int index = (int) shopItem.shopItems[0].currencyType;
                PurchaseButton newButton = Instantiate(shopItem.gameObject, shops[index].transform).GetComponent<PurchaseButton>();
                currentDisplayedShopItems.Add(newButton);
                newButton.shop = this;
            }
            shopItems.Clear();

            canvas.enabled = true;
            shopDisplayed = true;

            if(!hasEnteredShop)
            {
                //StartCoroutine(ReadFirstTimeShopDialogue());
            }
        }

        public void AddShopItem(PurchaseButton shopItem)
        {
            shopItems.Add(shopItem);

            if(shopDisplayed)
            {
                int index = (int) shopItem.shopItems[0].currencyType;
                PurchaseButton newButton = Instantiate(shopItem.gameObject, shops[index].transform).GetComponent<PurchaseButton>();
                currentDisplayedShopItems.Add(newButton);
            }
        }

        public void RemoveShopItem(PurchaseButton shopItem)
        {
            if(shopItem != null && shopItems.Contains(shopItem))
            {
                currentDisplayedShopItems.Remove(shopItem);
                shopItems.Remove(shopItem);
                Destroy(shopItem.gameObject);
            }
        }

        public void LeaveShop()
        {
            if(hasEnteredShop)
            {
                // foreach(PurchaseButton item in currentDisplayedShopItems)
                // {
                //     Destroy(item.gameObject);
                // }
                // currentDisplayedShopItems.Clear();

                canvas.enabled = false;
                shopDisplayed = false;
            }
        }

        // private IEnumerator ReadFirstTimeShopDialogue()
        // {
        //     yield return StartCoroutine(DialogueManager.Instance.EnterDialogueModeCo(DialogueConfigurer.ConfigureStory(firstTimeEnteringDialogue)));

        //     foreach(PurchaseButton button in currentDisplayedShopItems)
        //     {
        //         button.canInteract = true;
        //     }

        //     hasEnteredShop = true;
        //     StopCoroutine(ReadFirstTimeShopDialogue());
        // }
    }
}