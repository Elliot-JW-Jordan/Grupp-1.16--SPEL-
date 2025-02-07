using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System.Linq;

public class ManagerOfInventory : MonoBehaviour
{
    public List<ItemSystem> inventoryList = new List<ItemSystem>();
    public GameObject InventoryMenuUI;
    private bool activatedMenu = false;
    public int iInventoryListplacement = -1;
    //public int maxStack = maxNumberOfItems;
    public int maxStack = 4;
    public ItemSlotScriptInventory[] itemSlot;

    // Start is called before the first frame update

    void Start()
    {
        //F�r att s�kerst�lla att Inventory UI b�rjar som avst�ngd
        InventoryMenuUI.SetActive(false); //Skymmer Inventory
        activatedMenu = false; // s�tter menyns tillstond till inactivt
        Time.timeScale = 1; // S� att spelet brjar i en normal standard hastighet.



    }
    public void AddItemToInventory(ItemSystem invAddedItem) // L�gger till en f�rem�ls lista//kanske bya till Sting namn, 
    {

        if (invAddedItem == null)
        {
            Debug.LogError("Attempted to add ann item that is null to the player inventory.");
            return;
        }

        inventoryList.Add(invAddedItem);

        Debug.Log($"Added {invAddedItem.itemName} to players inventory list. The InventoryList now has the size {inventoryList.Count}");

        // hanterar datat i listan invAddedItem);
        // HandleItemData(invAddedItem);

        //Jag l�gger till en ut�kad besrkivning av f�rem�let med // HandleItemData(invAddedItem);
        string descriptionPlus = HandleItemData(invAddedItem);

        Debug.Log($" trying to add {invAddedItem.itemName} to the Ui itemslots.....");
        AddItem(invAddedItem.itemName, 1, invAddedItem.spriteIcon, descriptionPlus); //Jag kommer sranru byta ut sprite.icon



    }
    private string HandleItemData(ItemSystem item1)
    {

        if (item1 == null)
        {
            Debug.LogError("The item that was passed to HandleItemData is null");

        }
        string itemNameINV = item1.itemName;
        string itemDescriptionINV = item1.description;
        string itemPriceINV = item1.price.ToString();
        string statsINV = string.Join(", ", item1.stats.Select(stat => $"{stat.Key}: {stat.Value}"));
        string additionalInfoINV = "";
        if (item1 is Consumable consumable)
        {
            additionalInfoINV = $"Duration : {consumable.duration}, HealAmount : {consumable.healAmount}, BuffingFactor : {consumable.buffingFactor}";
        } else if (item1 is Armour armour)
        {
            additionalInfoINV = $"Durability: {armour.durability}, DefensiveValue: {armour.defensiveValue}, WeightA : {armour.weightA}";
        }

        Debug.Log($"HandleItem has succsessfully processed the item with Name: {item1.itemName} Stats : {statsINV}  Aditional infromation : {additionalInfoINV}");
        string wholeDescription = $"{itemDescriptionINV}/n Stats: {statsINV}/n {additionalInfoINV}.";
        return wholeDescription;
    }


    // Update is called once per frame
    void Update()
    {
        //Controlls Inventory UI


        //Kollar ifall knappen I 3ttrycks ned

        if (Input.GetKeyDown(KeyCode.I))  // Hela koden har flyttats tilL ItemUIController.
        {
            if (activatedMenu) // Ifall UI �r aktiverad n�r I trycks s� ska UI st�ngas ned
            {

                Time.timeScale = 1; // �terst�ller tiden, Spelet �r ite l�ngre pausat
                InventoryMenuUI.SetActive(false); //st�nger ned UI
                activatedMenu = false; //uPDATERAR BOOLEN f�r att indikera att UI nu �r st�ngd
            }
            else
            {
                Time.timeScale = 0; // stoppar tiden, Pausar spelet
                InventoryMenuUI.SetActive(true); //Visr  OCH �ppmnar inventory UI
                activatedMenu = true; // Nu indikerar boolen att UI �r ppen, VIlket den �r

            }
            //Kommih�g att s�tta inventoryButton till i i settings//


       
       }
   
        
        
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string descriptionPlus)
    {
        Debug.Log($"AddItem was called : {itemName} , {quantity}, {descriptionPlus}");

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogError($"Item slot is either/or empy,Initialized");
            return -1;
        }

        while(quantity > 0)
        {
            bool addeedToSlot = false;
            //  (int i = 0; i < itemSlot.Length; i++) /gammal loop som jag pr�var att byta ut.
            foreach (var slot in itemSlot) //L�ser efter en tomm slot som sedan ska fyllas in.
            {
                if (!slot.isfull) //Ifall slotten inte �r full.
                {
                    int toAdd = Mathf.Min(quantity, maxStack);
                    slot.AddItem(itemName, toAdd, itemSprite, descriptionPlus);
                    quantity -= toAdd;
                    addeedToSlot = true;
                    Debug.Log($"Slot added Item: {itemName}, Quantity : {quantity}");
                    break;
                }
                else if (slot.itemNAMEInv == itemName && slot.quantityInv < maxStack)    //kollar ifall slotten ineh�ller f�rem�l av samma namn
                {
                    int availableSpace = maxStack - slot.quantityInv;
                    int toadd = Mathf.Min(quantity, availableSpace);
                    slot.quantityInv += toadd;
                    slot.quantityText.text = slot.quantityInv.ToString();
                    quantity -= toadd;
                    Debug.Log($"Added to existing stack : {itemName}, New quantity : {slot.quantityInv},");
                    addeedToSlot = true;
                    break;

                   
                }
                //Debug.LogWarning($" There is not empty itemslot for the item : {itemName}");
             }
           // Debug.LogWarning($" There is not empty itemslot for the item : {itemName}");
           // return -1;
       if (!addeedToSlot)
            {
                Debug.LogWarning($" There is not empty itemslot for the item inventory full, could not add {quantity}: {itemName}");
                break;
            }


        }




        return 0;
      
       
    }
    public void DeselectionOfItemSlots()//Denna metod st�nger av den vita OUT-LINEN som  klickade itemslots activerar. S�tter invItemSelected till false
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedOutline.SetActive(false);
            itemSlot[i].invItemSelected = false;

        }
    }
    public void RemoveItemFromInventory(ItemSystem item)
    {
        //Ta bort f�rem�llet ifr�n listan
        inventoryList.Remove(item);
        Debug.Log($"Removed {item.itemName} from the inentory list");


        //tar ochs� bort f�remm�let fr�n UI inventory slots
        foreach(var slot in itemSlot)
        {
            if(slot.itemNAMEInv == item.itemName)
            {
                slot.ClearSlot();
                break;
            }
        }
    }

    public void RearangeInventory()
    {
        for (int i = 0; i < itemSlot.Length - 1; i++) // Koden ska stanna innan den sista 'slot' in 'Iteminventory'
        {
            if (!itemSlot[i].isfull) //finner tomma 'slot'
            {
                for (int j = i +1; j < itemSlot.Length; j++){     // LETAR EFTER n�sta FYLLda 'Slot'
                 
                    if (itemSlot[j].isfull) 
                    {

                        //�verf�rning av datta fr�n J till I
                        itemSlot[i].itemNAMEInv = itemSlot[j].itemNAMEInv;
                        itemSlot[i].quantityInv = itemSlot[j].quantityInv;
                        itemSlot[i].itemSpriteInv = itemSlot[j].itemSpriteInv;
                        itemSlot[i].descriptionInINV = itemSlot[j].descriptionInINV;
                        itemSlot[i].isfull = true;

                        // Nu uppdaterar jag UI element av det f�rflyttade 
                        itemSlot[i].quantityText.text = itemSlot[j].quantityText.text;
                        
                        itemSlot[i].itemDescriptionImage.sprite = itemSlot[j].itemSpriteInv;
                        itemSlot[i].quantityText.enabled = true;

                        // �verf�r selektion
                        itemSlot[i].invItemSelected = itemSlot[j].invItemSelected;
                        itemSlot[i].selectedOutline.SetActive(itemSlot[j].invItemSelected);
                        


                        //Tommer den origin�lla 'slot':en

                        itemSlot[j].ClearSlot();
                        break; // f�rflytta till n�sta tomma 'slot'


                    }

            }

        }
    }
}
    }
