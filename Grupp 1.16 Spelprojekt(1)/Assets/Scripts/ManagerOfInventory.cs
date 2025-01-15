using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ManagerOfInventory : MonoBehaviour
{
    public List<ItemSystem> inventoryList = new List<ItemSystem>();
    public GameObject InventoryMenuUI;
    private bool activatedMenu;
    public int iInventoryListplacement = -1;
    public ItemSlotScriptInventory[] itemSlot;

    // Start is called before the first frame update

    void Start() 
    {     
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

        // hanterar datat i listan invAddedItem);
        HandleItemData(invAddedItem);

        Debug.Log($" trying to add {invAddedItem.itemName} to the Ui itemslots.....");
        AddItem(invAddedItem.itemName, 1, invAddedItem.spriteIcon); //Jag kommer sranru byta ut sprite.icon

          

    }
    private void HandleItemData(ItemSystem item1)
    {

        if (item1 == null)
        {
            Debug.LogError("The item that was passed to HandleItemData is null");
            return;
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

    }
    public void GetItemsOnInventorylist() // place ment  public int iInventoryListplacement = -1;
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Controlls Inventory UI
      if(Input.GetKey("i") && activatedMenu)  // må behöva ändra till
       {
           Time.timeScale = 1; // Återställer tiden
           InventoryMenuUI.SetActive(false);
           activatedMenu = false;
        }  else if (Input.GetKey("i") && activatedMenu) //Kommihåg att sätta inventoryButton till i i settings//
       {
            Time.timeScale = 0; //Ger möjlighet att pausa spelet //Må ta bort ifall det ställer till med problem.

           InventoryMenuUI.SetActive(true);
           activatedMenu = true;
       }


    }
    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log($"AddItem was called : {itemName} , {quantity}");

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogError($"Item slot is either/or empy,Initialized");
            return;
        }



      //  (int i = 0; i < itemSlot.Length; i++) /gammal loop som jag prövar att byta ut.
        foreach (var slot in itemSlot) //Läser efter en tomm slot som sedan ska fyllas in.
        {
            if (!slot.isfull) //Ifall slotten inte är full.
            {
                slot.AddItem(itemName, quantity, itemSprite);
                Debug.Log($"Slot added Item: {itemName}, Quantity : {quantity}");
                return;


            }


           // if (itemSlot[i].isfull == false)  // det gamla
           // {

          //      itemSlot[i].AddItem(itemName, quantity, itemSprite);         //change to fitt my already existing code.
          //      return;
          //  }
        }
        Debug.LogWarning($" There is not empty itemslot for the item : {itemName}");
    }

}
