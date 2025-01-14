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
    public GameObject itemUIPrefab; // Prefaben f�r f�rem�let
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

        //Af�r UI kommer altid starta dold
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
        //Jag t�mmer af�ren p� alla f�rem�l
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject); //Frst�r alla gamla f�rem�l //Kolla och se ifall detta beh�ver �ndras efter som store manager �r gameobject

        }

        Debug.Log("shopContent cleared");
        //l�gger til nya f�rem�l
        foreach (ItemSystem item in shopItems)
        {
            Debug.Log($"Processing item: {item.itemName}");
            //Skapar flera nya UI f�r f�rem�len
            GameObject itemUI = Instantiate(itemUIPrefab, shopContent);
            TextMeshProUGUI nameText = itemUI.transform.Find("ItemNameTextUI")?.GetComponent<TextMeshProUGUI>();
            Debug.Log($"Instantioated item prefab: {itemUI.name}");



            //H�mtar alla UI komponenter
            //
            
            Image itemIconUI = itemUI.transform.Find("itemIconUI").GetComponent<Image>();//H�mtar f�rem�ls bilden
            Text titleTextUI = itemUI.transform.Find("ItemNameTextUI").GetComponent<Text>(); // H�mtar f�rem�ls Titeln
            Text descriptionTextUI = itemUI.transform.Find("ItemDescriptionUI").GetComponent<Text>(); // H�mtar f�rem�ls f�rklarningen
            Button itemBUYbutton = itemUI.transform.Find("ItemBuyButtonUI").GetComponent<Button>(); //H�mtar f�rem�lets K�p-knapp
            Text Price = itemUI.transform.Find("Price").GetComponent<Text>(); //H�mtar f�rem�ls Pris titteln 
            Text itemPriceTextUI = itemUI.transform.Find("ItemPriceUI").GetComponent<Text>(); //H�mtar f�rem�ls
            Text itemTypeTextUI = itemUI.transform.Find("ItemTypeUI").GetComponent<Text>();
            Text Stats = itemUI.transform.Find("Stats").GetComponent<Text>();
            Text Statkinds = itemUI.transform.Find("Statkinds").GetComponent<Text>();
            Text itemDurationTextUI = itemUI.transform.Find("ItemDurationUI").GetComponent<Text>();
            Text itemHealAmountTextUI = itemUI.transform.Find("ItemHealAmountUI").GetComponent<Text>();
            Text itemBuffingFactorTextUI = itemUI.transform.Find("ItemBuffingFactorUI").GetComponent<Text>();
            Text itemDurabilityTextUI = itemUI.transform.Find("ItemDurabilityUI").GetComponent<Text>();
            Text itemDefensiveValueTextUI = itemUI.transform.Find("ItemDefensiveValueUI").GetComponent<Text>();
            Text itemWeightATextUI = itemUI.transform.Find("ItemWeightAUI").GetComponent<Text>();




            // Fyller i med infromation om f�rem�len
            //handle null values.sprite
            //add to string where needed
            itemIconUI.sprite = item.spriteIcon ?? placeHolderSprite;
            
            if (titleTextUI != null)//Detta �r ett testnings debug
            {
                titleTextUI.text = item.itemName; // namnger f�rem�let
                
            } else
            {
                Debug.LogError("ItemnameTextUI not found in the item prefab");
                continue;
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
    void Update()
    {
        //F�r att st�nga ned UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateShopUI();
        }

    }
}
