using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfInventory : MonoBehaviour
{
    public List<ItemSystem> inventoryList = new List<ItemSystem>();
    public GameObject InventoryMenuUI;
    private bool activatedMenu;
    public int iInventoryListplacement = -1;
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
        Debug.Log($"Added {invAddedItem.itemName} to players inventory list");
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

}
