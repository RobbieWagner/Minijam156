using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobbieWagnerGames.Common;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.UI;
using System;

namespace RobbieWagnerGames.Plinko
{
    public class ShopMenu : Menu
    {

        [HideInInspector] public bool shopDisplayed = false;
        private List<PurchaseButton> shopItems;
        [SerializeField] private List<PurchaseButton> initialShopItems;
        private ShopControls shopControls;
        [SerializeField] private GridLayoutGroup organizer;
        private List<PurchaseButton[]> purchaseButtons; 
        [SerializeField] private int columnHeight = 3;
        private int CurRow = 0;

        public static ShopMenu Instance {get; private set;}
        protected override void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(gameObject); 
            } 
            else 
            { 
                Instance = this; 
            } 
            purchaseButtons = new List<PurchaseButton[]>();
            base.Awake();
            shopItems = initialShopItems;
            shopControls = new ShopControls();
            shopControls.Shop.NavigateShop.performed += NavigateMenu;
            shopControls.Shop.Select.performed += SelectMenuItem;
            purchaseButtons = new List<PurchaseButton[]>();
        }

        protected override void SelectMenuItem(InputAction.CallbackContext context)
        {
            Debug.Log("select");
            if(purchaseButtons.Any())
                StartCoroutine(purchaseButtons[curButton][CurRow].SelectButton(this));
            InvokeOnSelectMenuItem();
        }

        protected override void NavigateMenu(InputAction.CallbackContext context)
        {
            Vector2 dir = context.ReadValue<Vector2>();
            if(Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
            {
                if(dir.y > 0)
                {
                    CurRow--;
                    CurRow = Math.Clamp(CurRow, 0, purchaseButtons[0].Length - 1);
                    curButton = Math.Clamp(curButton, 0, purchaseButtons.Count - 1);
                    if(purchaseButtons[curButton][CurRow] == null)
                        CurRow++;
                    ConsiderMenuButton(purchaseButtons[curButton][CurRow]);
                }
                else if (dir.y < 0)
                {
                    CurRow++;
                    CurRow = Math.Clamp(CurRow, 0, purchaseButtons[0].Length - 1);
                    curButton = Math.Clamp(curButton, 0, purchaseButtons.Count - 1);
                    if(purchaseButtons[curButton][CurRow] == null)
                        CurRow--;
                    ConsiderMenuButton(purchaseButtons[curButton][CurRow]);
                }
            }
            else
            {
                if(dir.x > 0)
                {
                    curButton++;
                    CurRow = Math.Clamp(CurRow, 0, purchaseButtons[0].Length - 1);
                    curButton = Math.Clamp(curButton, 0, purchaseButtons.Count - 1);
                    if(purchaseButtons[curButton][CurRow] == null)
                        curButton--;
                    ConsiderMenuButton(purchaseButtons[curButton][CurRow]);
                }
                else if (dir.x < 0)
                {
                    curButton--;
                    CurRow = Math.Clamp(CurRow, 0, purchaseButtons[0].Length - 1);
                    curButton = Math.Clamp(curButton, 0, purchaseButtons.Count - 1);
                    if(purchaseButtons[curButton][CurRow] == null)
                        curButton++;
                    ConsiderMenuButton(purchaseButtons[curButton][CurRow]);
                }
            }
            //Debug.Log($"nav {curButton} {CurRow} {purchaseButtons.Count} {purchaseButtons[0].Length}");
        }

        public override void SetupMenu()
        {
            ClearMenu();
            purchaseButtons.Clear();
            
            int lists = shopItems.Count / columnHeight + 1;
            for(int i = 0; i < lists; i++)
                purchaseButtons.Add(new PurchaseButton[columnHeight]);
            int column = 0;
            int row = 0;
            for(int i = 0; i < shopItems.Count; i++)
            {
                PurchaseButton button = shopItems[i];
                purchaseButtons[column][row] = Instantiate(button, organizer.transform);
                row++;
                if(row >= columnHeight)
                {
                    column++;
                    row = 0;
                }
            }
            canvas.enabled = true;
            menuControls.Disable();
            shopControls.Enable();
            isOn = true;
            CallOnOpen();
            ConsiderMenuButton(purchaseButtons[0][0]);
        }

        protected void ConsiderMenuButton(PurchaseButton button)
        {
            foreach(PurchaseButton[] buttonList in purchaseButtons)
                foreach(PurchaseButton listButton in buttonList)
                    listButton?.NavigateAway();
            button.NavigateTo();
        }

        public override void DisableMenu(bool returnToPreviousMenu = true)
        {
            base.DisableMenu(returnToPreviousMenu);
            ClearMenu();
            shopControls.Disable();
        }

        private void ClearMenu()
        {
            foreach(PurchaseButton[] buttonList in purchaseButtons)
                foreach(PurchaseButton button in buttonList)
                    if(button != null) 
                        Destroy(button.gameObject);
            purchaseButtons.Clear();
        }
    }
}