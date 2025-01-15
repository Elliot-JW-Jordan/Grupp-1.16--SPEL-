using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlotScriptInventory : MonoBehaviour
{

    public string itemNAMEInv;
    public int quantityInv;
    public Sprite itemSpriteInv;
    public bool isfull;


    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImageINV;

    public void AddItem(string itemName, int quantity,Sprite itemSprite)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogError($"The itemName is empty or null");

        }


        this.itemNAMEInv = itemName;
        this.quantityInv = quantity;
        this.itemSpriteInv = itemSprite;
        isfull = true;

        quantityText.text = quantity.ToString(); // ifall det inte funkar, byt tillv ariable och inte parimeter
        itemImageINV.sprite = itemSprite;
        quantityText.enabled = true;


        Debug.Log($"Slot Updated : {itemName} Quantriry : {quantity}");
        
    }


}





   
