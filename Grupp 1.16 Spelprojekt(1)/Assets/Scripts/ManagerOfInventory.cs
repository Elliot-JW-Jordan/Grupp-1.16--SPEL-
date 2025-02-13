using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering;


public class ManagerOfInventory : MonoBehaviour
{
    DisplayingTextScript displaying;
    public List<ItemSystem> inventoryList = new List<ItemSystem>();
    public GameObject InventoryMenuUI;
    private bool activatedMenu = false;
    public int iInventoryListplacement = -1;
    //public int maxStack = maxNumberOfItems;
    public int maxStack = 4;
    public ItemSlotScriptInventory[] itemSlot;

    // Start is called before the first frame update
    public Sprite placeholder2;
    void Start()
    {
        displaying = FindObjectOfType<DisplayingTextScript>();

        //För att säkerställa att Inventory UI börjar som avstängd
        InventoryMenuUI.SetActive(false); //Skymmer Inventory
        activatedMenu = false; // sätter menyns tillstond till inactivt
        Time.timeScale = 1; // Så att spelet brjar i en normal standard hastighet.



    }
    public void AddItemToInventory(ItemSystem invAddedItem) // Lägger till en föremåls lista//kanske bya till Sting namn, 
    {

        if (invAddedItem == null)
        {
            Debug.LogError("Attempted to add ann item that is null to the player inventory.");
            return;
        }

        inventoryList.Add(invAddedItem);

        Debug.Log($"Added {invAddedItem.itemName} to players inventory list. The InventoryList now has the size {inventoryList.Count}");
        displaying.DisplayMessage($"{invAddedItem.itemName} added to inventory", 2f);
        // hanterar datat i listan invAddedItem);
        // HandleItemData(invAddedItem);

        //Jag lägger till en utökad besrkivning av föremålet med // HandleItemData(invAddedItem);
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
            if (activatedMenu) // Ifall UI är aktiverad när I trycks så ska UI stängas ned
            {

                Time.timeScale = 1; // Återställer tiden, Spelet är ite längre pausat
                InventoryMenuUI.SetActive(false); //stänger ned UI
                activatedMenu = false; //uPDATERAR BOOLEN för att indikera att UI nu är stängd
            }
            else
            {
                Time.timeScale = 0; // stoppar tiden, Pausar spelet
                InventoryMenuUI.SetActive(true); //Visr  OCH öppmnar inventory UI
                activatedMenu = true; // Nu indikerar boolen att UI är ppen, VIlket den är

            }
            //Kommihåg att sätta inventoryButton till i i settings//


       
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
            //  (int i = 0; i < itemSlot.Length; i++) /gammal loop som jag prövar att byta ut.
            foreach (var slot in itemSlot) //Läser efter en tomm slot som sedan ska fyllas in.
            {
                if (!slot.isfull) //Ifall slotten inte är full.
                {
                    int toAdd = Mathf.Min(quantity, maxStack);
                    slot.AddItem(itemName, toAdd, itemSprite, descriptionPlus);
                    quantity -= toAdd;
                    addeedToSlot = true;
                    Debug.Log($"Slot added Item: {itemName}, Quantity : {quantity}");
                    break;
                }
                else if (slot.itemNAMEInv == itemName && slot.quantityInv < maxStack)    //kollar ifall slotten inehåller föremål av samma namn
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
    public void DeselectionOfItemSlots()//Denna metod stänger av den vita OUT-LINEN som  klickade itemslots activerar. Sätter invItemSelected till false
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedOutline.SetActive(false);
            itemSlot[i].invItemSelected = false;

        }
    }
    public void RemoveItemFromInventory(ItemSystem item)
    {
        //Ta bort föremållet ifrån listan
        inventoryList.Remove(item);
        Debug.Log($"Removed {item.itemName} from the inentory list");
        displaying.DisplayMessage($"{item.itemName} Removed form inventory", 3f);

        //tar ochså bort föremmålet från UI inventory slots
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
        Debug.Log("Rearanging");
        for (int i = 0; i < itemSlot.Length - 1; i++) // Koden ska stanna innan den sista 'slot' in 'Iteminventory'
        {
            if (!itemSlot[i].isfull) //finner tomma 'slot'
            {
                for (int j = i +1; j < itemSlot.Length; j++){     // LETAR EFTER nästa FYLLda 'Slot'

                    if (itemSlot[j].isfull)
                    {
                        // Flyttar data från J till I,
                        MoveItemsToEmptySlot(itemSlot[i], itemSlot[j]);
                        itemSlot[j].ClearSlot(); // säkerställer att slottn töms korrekt
                        // Updatera UI 
                        HelperUpdate(i);
                        HelperUpdate(j);
                        itemSlot[j].itemImageINV.enabled = false; //kanske fungerar
                        itemSlot[j].itemDescriptionImage.enabled = false; //kanske fungerar

                        // säkerställer så att J förblir avstängd
                        if (i != j)
                        {
                            if (itemSlot[i].itemImageINV != null)
                            {
                                itemSlot[i].itemImageINV.enabled = true;
                            }
                            if (itemSlot[i].itemDescriptionImage != null)
                            {
                                itemSlot[i].itemDescriptionImage.enabled = true;
                            }
                            if (itemSlot[i].quantityText != null)
                            {
                                itemSlot[i].quantityText.enabled = true;
                            }
                        }

                      //  SwapItems(itemSlot[i], itemSlot[j]); // switch tp i,
                        break; // förflytta till nästa tomma 'slot' 
                   

                    }
            }

        }
    }
}

    public void MoveItemsToEmptySlot(ItemSlotScriptInventory emptyslot, ItemSlotScriptInventory filledslot) // för att förflytta koden mellan den fýlda och tomma
    {
       if (filledslot == null || emptyslot == null)
        {
            Debug.LogWarning("Game attemped to move items between two null slots.");
            return;
        }
        // förflytar föremålen mellan de två rutorna
        //kopierar data
        emptyslot.itemNAMEInv = filledslot.itemNAMEInv;
        emptyslot.quantityInv = filledslot.quantityInv;
        emptyslot.itemSpriteInv = filledslot.itemSpriteInv;
        emptyslot.descriptionInINV = filledslot.descriptionInINV;
        emptyslot.itemData = filledslot.itemData;
        emptyslot.invItemSelected = filledslot.invItemSelected;
        emptyslot.isfull = true;
        // upfatera Ui för den tomma.
       
        emptyslot.quantityText.text = filledslot.quantityText.text;
        emptyslot.itemImageINV.sprite = filledslot.itemImageINV.sprite;
        emptyslot.itemDescriptionText.text = filledslot.itemDescriptionText.text;
        emptyslot.itemDescriptionNameText.text = filledslot.itemDescriptionNameText.text;
        emptyslot.itemDescriptionImage.sprite = filledslot.itemDescriptionImage.sprite;
        emptyslot.itemDescriptionImage.sprite = filledslot.itemImageINV.sprite;
        emptyslot.quantityText.enabled = true;
        emptyslot.itemImageINV.color = Color.white;
        emptyslot.itemDescriptionImage.color = Color.white;
        emptyslot.itemDescriptionImage.enabled = true;

        emptyslot.itemDescriptionImage.color = new Color(1f, 1f, 1f, 1f); // synlig
        emptyslot.selectedOutline.SetActive(filledslot.invItemSelected);
        Debug.Log("Items moved");
        // töm 'filled'slot
        filledslot.ClearSlot();
        Debug.Log("Items moved SUCCESSFULLY FROM, original slot clreared");
    }
  

    public void HelperUpdate(int slotindex)
    {
        ItemSlotScriptInventory slot = itemSlot[slotindex];
        if (slot != null)
        {
            //uppdaterae en slots ui
            slot.quantityText.text = slot.quantityInv.ToString();
            slot.itemImageINV.sprite = slot.itemSpriteInv;
            slot.quantityText.enabled = slot.isfull;
            slot.selectedOutline.SetActive(slot.invItemSelected);

            // berskrivning 
            slot.itemDescriptionText.text = slot.descriptionInINV;
            slot.itemDescriptionNameText.text = slot.itemNAMEInv;
            slot.itemDescriptionImage.sprite = slot.itemSpriteInv;

          


            Debug.Log($"UI updated for slot{slotindex} ({slot.itemNAMEInv}): quantity :{slot.quantityInv}");
        }
        
    }

    }
