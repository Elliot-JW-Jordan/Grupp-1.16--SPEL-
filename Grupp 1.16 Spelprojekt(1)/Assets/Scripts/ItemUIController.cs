using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIController : MonoBehaviour
    
{
    public GameObject inventoryUI;
    private bool activatedMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        //För att säkerställa att Inventory UI börjar som avstängd
        if(inventoryUI == null)
        {
            Debug.LogError("InventoryUI is not assigned in the inspoektor");
        } else
        {
            inventoryUI.SetActive(false); //Skymmer Inventory
        }

        
        activatedMenu = false; // sätter menyns tillstond till inactivt
        Time.timeScale = 1; // Så att spelet brjar i en normal standard hastighet.

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(activatedMenu)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }
     private void OpenInventory()
    {
        Time.timeScale = 0; // Pausar
        inventoryUI.SetActive(true);
        activatedMenu = true;
    }
    private void CloseInventory()
    {
        Time.timeScale = 1; //Återställa tid,
        inventoryUI.SetActive(false);
        activatedMenu = false;
    }
}
