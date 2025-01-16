using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using JetBrains.Annotations;

public class ItemSlotScriptInventory : MonoBehaviour, IPointerClickHandler
{
    [Header("Slot and item data")]
    public int maxNumberOfItems = 4;
    public string itemNAMEInv;
    public int quantityInv;
    public Sprite itemSpriteInv;
    public bool isfull;
    public string descriptionInINV;



    [SerializeField]
    public TMP_Text quantityText;

    [SerializeField]
    private Image itemImageINV;

    [Header("Selection")]
    [SerializeField]
    public GameObject selectedOutline;
    public bool invItemSelected;


    [Header("Description")]
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    private ManagerOfInventory inventoryM;  //refferrar till Manager of Inventory


    private void Start()
    {
        inventoryM = GameObject.Find("InventoryCANVAS").GetComponent<ManagerOfInventory>();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string descriptionPlus)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError($"The itemName is empty or null");

        }


        this.itemNAMEInv = itemName;
        this.quantityInv = quantity;
        this.itemSpriteInv = itemSprite;
        this.descriptionInINV = descriptionPlus;


        isfull = true;

        quantityText.text = quantity.ToString(); // ifall det inte funkar, byt tillv ariable och inte parimeter
        itemImageINV.sprite = itemSprite;
        quantityText.enabled = true;


        Debug.Log($"Slot Updated : {itemName} Quantriry : {quantity}");

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();

        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();

        }


    }
    public void OnLeftClick()
    {

        inventoryM.DeselectionOfItemSlots();
        selectedOutline.SetActive(true);
        invItemSelected = true;
        itemDescriptionText.text = descriptionInINV;
        itemDescriptionNameText.text = itemNAMEInv;
        itemDescriptionImage.sprite = itemSpriteInv;



    }

    public void OnRightClick()
    {
        // F�rst kollar jag ifall slotten �r seleckad
        if (!invItemSelected)
        {
            Debug.LogWarning("The Right-Click failed. The itemslot clicked must be selected before using it");
            return;
        }
        //koller ifall datan verkligen finns
        if (string.IsNullOrEmpty(itemNAMEInv))
        {
            Debug.LogError("The Right-Click failed. The item DATA is either invalid or empty");
            return;
        }
        ItemUseManagerScript itemUseManager = FindObjectOfType<ItemUseManagerScript>();
        if (itemUseManager == null)
        {
            Debug.LogError("ItemUseManagerScript not found in this scene");
            return;
        }
        //Hitta det riktiga namnet p� f�rem�let i slotten som clickades
        ItemSystem itemtoUSE = FindItemByName(itemNAMEInv);

        if (itemtoUSE == null)
        {
            Debug.LogError($"Item : {itemNAMEInv} not found in inventory");
            return;
        }

        //Anv�nd det untvalda f�rm�let genom att kalla p� UseItem
        itemUseManager.UseItem(itemtoUSE);
        //Item quantaty minskar med 1 efter anv�ndning 
        quantityInv--;
        //Uppdaterar UI
        quantityText.text = quantityInv.ToString();

        //Om quantity n�r noll 0, s� ska f�rem�let f�rsvinna fr�n itemslotten och spelarens Inventory
        if (quantityInv<= 0)
        {

            //T�m rutan allts� Slot
            ClearSlot();

            //ta bort f�rem�let ifr�n inventorylist
            inventoryM.RemoveItemFromInventory(itemtoUSE);
        }

       

    }

    // en metod f�r att hemta namnet p� f�rem�let fr�n 
    private ItemSystem FindItemByName(string itemNameS)
    {  //Loopar egenom inventorylistan
        foreach (var item in inventoryM.inventoryList)
        {
            if (item.itemName == itemNameS)
            {
                return item;
            }


        }
        return null;
    }
    public void ClearSlot()
    {
        //t�m slotten allts� rutans infromation
        itemNAMEInv = string.Empty;
        quantityInv = 0;
        itemImageINV = null;
        descriptionInINV = string.Empty;

        //visar att rutan �r tom  genom bool
        isfull = false;
        quantityText.text = string.Empty;
        itemImageINV.sprite = null;
        quantityText.enabled = false;

    }
}





   
