using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [Header("UI references")]
    public Transform shopContent; // "Content" inom scrolable field
    public GameObject itemUIPrefab; // Prefaben för föremålet
    public Text currencyText;
    public Sprite placeHolderSprite;
    public GameObject shopUIpanel;

   
    [Header("System Referances")]
    public ShopSystem shopSystem;


    private int playerCurency;

    

    // Add rarity to title name toall of the items in item creator
    //Make it so the shop stay hidden until you press a centrian button
    /// press escape and it exises out of the store
    // shop exit button;
    //add button funktinality
    // add place holdermoneysystem adn displayer
    // add a buying and for stor to inventory transfer system
    // thest the shop
    // add Type and or rarity to defsciption of itm

    void Start()
    {

        shopSystem = FindObjectOfType<ShopSystem>();
        playerCurency = shopSystem.placeholderCurrency;
       // UpdateCurrencyUI();
        RefreshShopUI(shopSystem.shopitems);

        
    }
    public void RefreshShopUI(List<ItemSystem> shopItems)
    {
        //Jag tömmer afären på alla föremål
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject); //Frstör alla gamla föremål //Kolla och se ifall detta behöver ändras efter som store manager är gameobject

        }

        //lägger til nya föremål
        foreach (ItemSystem item in shopItems)
        {
            //Skapar flera nya UI för föremålen
            GameObject itemUI = Instantiate(itemUIPrefab, shopContent);


            //Hämtar alla UI komponenter
            //
            
            Image itemIconUI = itemUI.transform.Find("itemIconUI").GetComponent<Image>();
            Text titleTextUI = itemUI.transform.Find("ItemNameTextUI").GetComponent<Text>();
            Text descriptionTextUI = itemUI.transform.Find("ItemDescriptionUI").GetComponent<Text>();
            Button itemBUYbutton = itemUI.transform.Find("ItemBuyButtonUI").GetComponent<Button>();
            Text Price = itemUI.transform.Find("Price").GetComponent<Text>();
            Text itemPriceTextUI = itemUI.transform.Find("ItemPriceUI").GetComponent<Text>();
           Text itemTypeTextUI = itemUI.transform.Find("ItemTypeUI").GetComponent<Text>();
            Text Stats = itemUI.transform.Find("Stats").GetComponent<Text>();
            Text Statkinds = itemUI.transform.Find("Statkinds").GetComponent<Text>();
            Text itemDurationTextUI = itemUI.transform.Find("ItemDurationUI").GetComponent<Text>();
            Text itemHealAmountTextUI = itemUI.transform.Find("ItemHealAmountUI").GetComponent<Text>();
            Text itemBuffingFactorTextUI = itemUI.transform.Find("ItemBuffingFactorUI").GetComponent<Text>();
            Text itemDurabilityTextUI = itemUI.transform.Find("ItemDurabilityUI").GetComponent<Text>();
            Text itemDefensiveValueTextUI = itemUI.transform.Find("ItemDefensiveValueUI").GetComponent<Text>();
            Text itemWeightATextUI = itemUI.transform.Find("ItemWeightAUI").GetComponent<Text>();




            // Fyller i med infromation om föremålen
            //handle null values.sprite
            //add to string where needed
            itemIconUI.sprite = item.spriteIcon ?? placeHolderSprite;
            
            if (titleTextUI != null)//Detta är ett testnings debug
            {
                titleTextUI.text = item.itemName; // namnger föremålet

            } else
            {
                Debug.LogError("ItemnameTextUI not found in the item prefab");
            }
            titleTextUI.text = item.itemName;
            descriptionTextUI.text = item.description;
            itemPriceTextUI.text = item.price.ToString();
            Price.text = "Price:";
            Stats.text = "Stats:";
            Statkinds.text = "TYPE :\n" +
            "Duration :\n" +
            "Heal amount :\n" +
            "Buffing factor :\n" +
            "Durability :" +
            "Defensive value :\n" +
            "WeightA :";


               


          if (item is Consumable consumable)
            {
                itemTypeTextUI.text = consumable.consumableType1.ToString();
                itemDurationTextUI.text = consumable.duration.ToString();
                itemHealAmountTextUI.text = consumable.healAmount.ToString();
                itemBuffingFactorTextUI.text = consumable.buffingFactor.ToString();

            } else
            {
              //  itemTypeTextUI.text = "N / A";
                itemDurationTextUI.text = "N / A";
                itemHealAmountTextUI.text = "N / A";
                itemBuffingFactorTextUI.text = "N / A";

            }
          if (item is Armour armour)
            {
                itemTypeTextUI.text = armour.armourType.ToString();
                itemDurabilityTextUI.text = armour.durability.ToString();
                itemDefensiveValueTextUI.text = armour.defensiveValue.ToString();
                itemWeightATextUI.text = armour.weightA.ToString();
                     

            } else
            {
             //   itemTypeTextUI.text = armour.armourType.ToString();
                itemDurabilityTextUI.text = "N / A";
                itemDefensiveValueTextUI.text = "N / A";
                itemWeightATextUI.text = "N / A";

            }
         


                 



        }

    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
