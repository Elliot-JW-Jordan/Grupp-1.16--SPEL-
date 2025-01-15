using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

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

    public void AddItem(string itemName, int quantity,Sprite itemSprite, string descriptionPlus)
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


    }
}





   
