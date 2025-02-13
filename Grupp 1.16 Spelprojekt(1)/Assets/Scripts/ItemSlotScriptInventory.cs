using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using System.Linq;
using System;

public class ItemSlotScriptInventory : MonoBehaviour, IPointerClickHandler
{
    DisplayingTextScript displaying;
    [Header("Slot and item data")]
    public int maxNumberOfItems = 4;
    public string itemNAMEInv;
    public int quantityInv;
    public Sprite itemSpriteInv;
    public bool isfull;
    public string descriptionInINV;

    [Header("Placeholder")]
    public Sprite placeholderImage;

    [SerializeField]
    public TMP_Text quantityText;

    [SerializeField]
    public Image itemImageINV;

    [Header("Selection")]
    [SerializeField]
    public GameObject selectedOutline;
    public bool invItemSelected;


    [Header("Description")]
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public ItemSystem itemData; // lagrar det tikiga data som , armour 
    private ManagerOfInventory inventoryM;  //refferrar till Manager of Inventory



    private void Awake()
    {
        // finn alla instancer ut av ManagerOfInventory, b�de aktiva och inactiva., v�ljer den f�rsta.
        ManagerOfInventory[] managerOfInventories =  Resources.FindObjectsOfTypeAll<ManagerOfInventory>();
        inventoryM = managerOfInventories.FirstOrDefault();

        if (inventoryM == null)
        {
            Debug.LogError("There is no ManagerOfInventoy found, make sure there is one the the scene");
        }
    }



    private void Start()
    {
        displaying = FindObjectOfType<DisplayingTextScript>();

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
        itemImageINV.color = Color.white;
        itemDescriptionImage.color = Color.white;
        quantityText.enabled = true;

        if (itemImageINV != null)
        {
            itemImageINV.enabled = true;
            itemDescriptionImage.enabled = true;
        }
        


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
     if (isfull) // ifall slotten inte �r t�md
        {
            if (itemImageINV != null) // ifalld en fins
            {
                itemImageINV.enabled = true; //
            }
            if (itemDescriptionImage != null) //ifall den fins
            {
                itemDescriptionImage.enabled = true;
            }
            
        }
        inventoryM.DeselectionOfItemSlots();
        selectedOutline.SetActive(true);
        invItemSelected = true;
        itemDescriptionText.text = descriptionInINV;
        itemDescriptionNameText.text = itemNAMEInv;
        itemDescriptionImage.sprite = itemSpriteInv;



    }

    public void OnRightClick()
    {
        // hindrar problem
        if (itemImageINV != null)
        {
            itemImageINV.enabled = true;
        }
        if (itemDescriptionImage != null)
        {
            itemDescriptionImage.enabled = true;
        }

        // F�rst kollar jag ifall slotten �r seleckad
        if (!invItemSelected)
        {
            Debug.LogWarning("The Right-Click failed. The itemslot clicked must be selected before using it");
            return;
        }
        //koller ifall datan verkligen finns
        if (string.IsNullOrEmpty(itemNAMEInv))
        {
            Debug.LogWarning("The Right-Click failed. The item DATA is either invalid or empty");
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
        displaying.DisplayMessage($"Used item : {itemtoUSE}", 3f);
        //Item quantaty minskar med 1 efter anv�ndning 
        quantityInv--;
        //Uppdaterar UI
        quantityText.text = quantityInv.ToString();
        inventoryM.HelperUpdate(Array.IndexOf(inventoryM.itemSlot, this));

        //Om quantity n�r noll 0, s� ska f�rem�let f�rsvinna fr�n itemslotten MEN INTE FR�N ALLA  och spelarens Inventory
        if (quantityInv<= 0)
        {

       //old clear

            // BOOLEN kollar iffal det fins itemslorts kvar med samma item
            bool theItemStillExists = false;
            ItemSystem itemSlotToRemove = null; // Variablen som lagrar f�rem�let som m�ste tas bort
            if (inventoryM.itemSlot != null && inventoryM.itemSlot.Length > 0)
            {

                foreach (var slot in inventoryM.itemSlot)
                {
                    if (slot.itemNAMEInv == itemNAMEInv && slot.quantityInv > 0)
                    {
                        theItemStillExists = true;
                        break;
                    }
                    // om slotten har 0 antall, markera att den ska tas bort senare
                    if (slot.itemNAMEInv == itemNAMEInv && slot.quantityInv == 0)
                    {
                        itemSlotToRemove = FindItemByName(itemNAMEInv); // lagrar alla slots som ska tas bort

                    }

                }

            }
              
            if (!theItemStillExists)
            {

                if(itemSlotToRemove != null)
                {
                    //ta bort f�rem�let ifr�n inventorylist
                    inventoryM.RemoveItemFromInventory(itemSlotToRemove);

                }
                
            }
            isfull = false;
          
            ClearSlot();
            inventoryM.RearangeInventory(); // flytta f�rem�l

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
        

            
            
            
            // all null to placeholder
      
        //t�m slotten allts� rutans infromation

        // �ters�ller data

        itemNAMEInv = string.Empty;
        quantityInv = 0;
       descriptionInINV = string.Empty;
        isfull = false;

        
  //visar att rutan �r tom  genom bool
       
        invItemSelected = false;
        quantityText.text = string.Empty;
        quantityText.enabled = false;
        //Anv�nder en PLACEHOLDER SPRITE IST�LLET F�R NULL
        //st�nger barra av ifall helt tom
       
            if (itemImageINV != null)
            {
                itemImageINV.sprite = placeholderImage; // anv�nder en placeholder
            itemImageINV.color = new Color(1f, 1f, 1f, 0f); // g�r placeholdern genomskinlig
                itemImageINV.enabled = true;
            }
           if ( itemDescriptionImage != null)
            {
                itemDescriptionImage.sprite = placeholderImage;
            itemDescriptionImage.color = new Color(1f, 1f, 1f, 0f);
            itemDescriptionImage.enabled = true;
            }
           if(selectedOutline != null)
        {
            selectedOutline.SetActive(false);
        }
          
        //Kallar en metod f�r att ordna om inventory n�r  
      // /inventoryM.RearangeInventory();
        Debug.Log("The slot was cleared successfully");
    }
    private void Update()
   
        {
            if (itemImageINV != null)
            {
                itemImageINV.enabled = true;
            }
            if (itemDescriptionImage != null)
            {
                itemDescriptionImage.enabled = true;
            }

    }
}
        
      





   
