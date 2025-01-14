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
     public void AddItemToInventory(ItemSystem invAddedItem) // L�gger till en f�rem�ls lista//kanske bya till Sting namn, 
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
      if(Input.GetKey("i") && activatedMenu)  // m� beh�va �ndra till
       {
           Time.timeScale = 1; // �terst�ller tiden
           InventoryMenuUI.SetActive(false);
           activatedMenu = false;
        }  else if (Input.GetKey("i") && activatedMenu) //Kommih�g att s�tta inventoryButton till i i settings//
       {
            Time.timeScale = 0; //Ger m�jlighet att pausa spelet //M� ta bort ifall det st�ller till med problem.

           InventoryMenuUI.SetActive(true);
           activatedMenu = true;
       }


    }

}
