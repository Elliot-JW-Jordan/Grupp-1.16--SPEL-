using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerOfInventory : MonoBehaviour
{
    public GameObject InventoryMenuUI;
    private bool activatedMenu; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("InventoryButton") && activatedMenu)
        {
            InventoryMenuUI.SetActive(false);
            activatedMenu = false;
        }

       else if (Input.GetButtonDown("InventoryButton") && !activatedMenu)
        {
            InventoryMenuUI.SetActive(true);
            activatedMenu = true;
        }


    }
}
