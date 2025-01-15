using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [Header("UI references")]
    public Transform shopContent; // "Content" inom scrolable field
    public GameObject itemUIPrefab; // Prefaben för föremålet
    public TMP_Text currencyText;
    public Sprite placeHolderSprite;
    public GameObject shopUIpanel;

   
    [Header("System Referances")]
    public ShopSystem shopSystem;
    public ItemManagerandMaker itemManagerandMaker;
    //public InventorySystem playerInventory;
    public ItemSystem item1;
   

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
     //   item1 = FindObjectOfType<ItemSystem>();
        playerCurency = shopSystem.placeholderCurrency;
        // playerInventory FindObjectOfType<InventorySystem>
        



        UpdateCurrencyUI();
      RefreshShopUI(shopSystem.shopitems);

        //Afär UI kommer altid starta dold
        shopUIpanel.SetActive(false);

        
    }
    public void RefreshShopUI(List<ItemSystem> shopItems)
    {
       // foreach(var item in shopItems)
     //   {
       //     GameObject itemUI = Instantiate(itemUIPrefab, shopContent);
       //     TextMeshProUGUI nameText = itemUI.transform.Find("")
      //  }
        Debug.Log(" RefreshingShopUI, Refreshing shop UI...");
        //Jag tömmer afären på alla föremål
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject); //Frstör alla gamla föremål //Kolla och se ifall detta behöver ändras efter som store manager är gameobject

        }

        Debug.Log("shopContent cleared");
        //lägger til nya föremål
        foreach (ItemSystem item in shopItems)
        {
            Debug.Log($"Processing item: {item.itemName}");
            //Skapar flera nya UI prefabs för föremålen
            GameObject itemUI = Instantiate(itemUIPrefab, shopContent);
            TextMeshProUGUI nameText = itemUI.transform.Find("ItemNameTextUI")?.GetComponent<TextMeshProUGUI>();
            Debug.Log($"Instantioated item prefab: {itemUI.name}");



            //Hämtar alla UI komponenter
            //
            
            Image itemIconUI = itemUI.transform.Find("itemIconUI").GetComponent<Image>();//Hämtar föremåls bilden
            TextMeshProUGUI titleTextUI = itemUI.transform.Find("ItemNameTextUI").GetComponent<TextMeshProUGUI>(); // Hämtar föremåls Titeln
            TextMeshProUGUI descriptionTextUI = itemUI.transform.Find("ItemDescriptionUI").GetComponent<TextMeshProUGUI>(); // Hämtar föremåls förklarningen
            Button itemBUYbutton = itemUI.transform.Find("ItemBuyButtonUI").GetComponent<Button>(); //Hämtar föremålets Köp-knapp
            TextMeshProUGUI Price = itemUI.transform.Find("Price").GetComponent<TextMeshProUGUI>(); //Hämtar föremåls Pris titteln 
            TextMeshProUGUI itemPriceTextUI = itemUI.transform.Find("ItemPriceUI").GetComponent<TextMeshProUGUI>(); //Hämtar föremåls
            TextMeshProUGUI itemTypeTextUI = itemUI.transform.Find("ItemTypeUI").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI Stats = itemUI.transform.Find("Stats").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI Statkinds = itemUI.transform.Find("Statkinds").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemDurationTextUI = itemUI.transform.Find("ItemDurationUI").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemHealAmountTextUI = itemUI.transform.Find("ItemHealAmountUI").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemBuffingFactorTextUI = itemUI.transform.Find("ItemBuffingFactorUI").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemDurabilityTextUI = itemUI.transform.Find("ItemDurabilityUI").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemDefensiveValueTextUI = itemUI.transform.Find("ItemDefensiveValueUI").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemWeightATextUI = itemUI.transform.Find("ItemWeightAUI").GetComponent<TextMeshProUGUI>();




            // Fyller i med infromation om föremålen
            //handle null values.sprite
            //add to string where needed
            itemIconUI.sprite = item.spriteIcon != null ? item.spriteIcon : placeHolderSprite;
            
            if (titleTextUI != null)//Detta är ett testnings debug
            {
                titleTextUI.text = item.itemName; // namnger föremålet
             
            } else
            {
                Debug.LogError("ItemnameTextUI not found in the item prefab");
                continue;
            }
           // titleTextUI.text = item.itemName;
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




          //if not work switch  item to item1
            // planerar knappens onClick listner
            itemBUYbutton.onClick.AddListener(() => BuyItem(item));
        }
       
       
    }
    // Start is called before the first frame update
   
    public void ActivateUIforShop()
    {
        shopUIpanel.SetActive(!shopUIpanel.activeSelf);
    }


    public void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = $"Currency : {playerCurency}";
        }

    }

    public void DeactivateShopUI()
    {
        shopUIpanel.SetActive(false);
    }
    // Update is called once per frame

    public void BuyItem(ItemSystem item1) // bytte alla item1 till item
    {
        if (item1 == null)
        {
            Debug.LogError("Player tried to buy a null item.");
            return;
        }
        //Se till så att spelaren har tillräkligt med pengar 
        if (playerCurency >= item1.price)
        {
            //Subtrakthera priset från spelarens totalla pengar värde
            playerCurency -= item1.price;
            //Föremålet läggs till till spelarens "inventory"
            var inventoryManager = FindAnyObjectByType<ManagerOfInventory>();
            // FindObjectOfType<ManagerOfInventory>().AddItemToInventory(item1); //tog bort efeter som
            if (inventoryManager == null)
            {

                Debug.LogError("ManagerOfInvetory is not found in this scene");
                return;
            }
            if (inventoryManager != null)
            {
                Debug.Log("Debug item to inventory");
                inventoryManager.AddItemToInventory(item1);
               // Debug.Log("Debug added item to inventory");
            }
           // inventoryManager.AddItemToInventory(item1);
            //borde kanske byta til . AddItem istället.
             //Nu ska metoden för att updatera UI kallas
            UpdateCurrencyUI();
            Debug.Log($"Player payed for {item1.itemName} for {item1.price}. Players remainig currency : {playerCurency}");
           // return; // ta bort ifall spell inte funkar
        } else
        {
            Debug.LogWarning($"The player does NOT have enough currency to buy {item1.itemName}. Required currency for purchase {item1.price}, Available : {playerCurency}");

        }
    }
    void Update()
    {
        //För att stänga ned UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateShopUI();
        }

    }
}
