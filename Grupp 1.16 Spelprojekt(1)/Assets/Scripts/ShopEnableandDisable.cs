using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEnableandDisable : MonoBehaviour
{
    public ShopUI shopUI;




    private void OnMouseDown()
    {
        //unders�k och se ifall Shop Ui g�r att activera
        if (shopUI != null)
        {
            shopUI.ActivateUIforShop();
            //Kallar till metoden f�r att activera shoppen
            Debug.Log("The Shop has been activated");
        } else
        {
            Debug.Log("The shop is not able to be activated. Chech if assigned correctlt");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
