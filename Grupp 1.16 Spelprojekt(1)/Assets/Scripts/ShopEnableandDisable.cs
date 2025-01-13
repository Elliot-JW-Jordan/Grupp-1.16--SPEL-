using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEnableandDisable : MonoBehaviour
{
    public GameObject shopUIpanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }
     public void TurnShopOn()
    {
        shopUIpanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
