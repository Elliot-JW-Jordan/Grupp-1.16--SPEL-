using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopEnableandDisable : MonoBehaviour
{
    public ShopUI shopUI;






    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (shopUI == null)
        {
            Debug.LogError("ShopUI not found, Please assign in inspector");
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            shopUI.ActivateUIforShop();
            Debug.Log("The Shop has been activated");

        }

    }
}
