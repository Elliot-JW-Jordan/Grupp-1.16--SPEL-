using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{

    public List<ItemSystem> allitems; // Lista med alla f�rem�l
    public List<ItemSystem> shopitems; // Lista med alla f�rem�l i  aff�ren

    //Procentuella sannolikheter f�r  f�rem�l av olika s�llsynthet att hamna i shoppen
    [Range(0, 1)] public float commonChance = 0.4f;
        [Range(0, 1)] public float uncommonChance = 0.25f;
    [Range(0, 1)] public float rareChance = 0.20f;
    [Range(0, 1)] public float epicChance = 0.1f;
    [Range(0, 1)] public float legendaryChance = 0.05f;


    public int maximumAmountOfItemsInShop = 10;

    // Start is called before the first frame update
    void Start()
    {
        LoadShop();
        SortingOfShopItems();
        UpdateUI();
    }
    public void LoadShop()
    {
        shopitems.Clear();//jag t�mmer listan av f�rem�l i aff�ren
        List<ItemSystem> itemsThatAreAvailable = new List<ItemSystem>(allitems);

        while (shopitems.Count < maximumAmountOfItemsInShop && itemsThatAreAvailable.Count > 0)
        {
            ItemSystem selectionOFItems = GetByRarity(itemsThatAreAvailable);
            if (selectionOFItems != null) 
            {
                shopitems.Add(selectionOFItems);
                itemsThatAreAvailable.Remove(selectionOFItems);

            }




        }
    }
    ItemSystem GetByRarity(List<ItemSystem> itemsThatAreAvailable)
    {
        float rndValue = UnityEngine.Random.value;

        foreach (var item in itemsThatAreAvailable)
        {
            switch (item.itemRarity)
            {
                case Rarity.Common:
                    if(rndValue <= commonChance)
                    {
                        return item;
                    }
                    break;
                case Rarity.Uncommon:
                    if (rndValue <= uncommonChance)
                    {
                        return item;
                    }
                    break;
                case Rarity.Rare:
                    if (rndValue <= rareChance)
                    {
                        return item;
                    }
                    break;
                case Rarity.Epic:
                    if (rndValue <= epicChance)
                    {
                        return item;
                    }
                    break;
                case Rarity.Legendary:
                    if (rndValue <= legendaryChance)
                    {
                        return item;
                    }
                    break;


            }
        }
        return null; //inget f�rem�l passade �llsyntheten

    }

    void SortingOfShopItems()
    {
        shopitems.Sort((a, b) =>
        {

            if (a is Consumable && b is Armour) return -1;
            if (a is Armour && b is Consumable) return 1;
            return 0;
        });
    }

    public void UpdateUI()
    {
        //placeholder f�r UI 
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
