using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    //Todo:
    //Lägtill range till den slummässiga nummer generatorn
    public ItemManagerandMaker itemManager; //refferens till ItemManagerandMaker
    public List<ItemSystem> allitems = new List<ItemSystem>(); // Lista med alla föremål
    [SerializeField]
    public List<ItemSystem> shopitems = new List<ItemSystem>(); // Lista med alla föremål i  affären
    public int placeholderCurrency = 1000;

    //Procentuella sannolikheter för  föremål av olika sällsynthet att hamna i shoppen
    [Range(0, 1)] public float commonChance = 0.4f;
    [Range(0, 1)] public float uncommonChance = 0.25f;
    [Range(0, 1)] public float rareChance = 0.20f;
    [Range(0, 1)] public float epicChance = 0.1f;
    [Range(0, 1)] public float legendaryChance = 0.05f;


    public int maximumAmountOfItemsInShop = 10;

    
    private void Awake()
    {
        if (shopitems == null)// för säkerhets skull.
        {
            Debug.LogError("Awake: Initalizing shopitems");
            shopitems = new List<ItemSystem>();
        }
    }


    IEnumerator InitializeShop()
    {
        //vänta tills ItemManager och Listan inte är null eller tomma för att påbörja
        yield return new WaitUntil(() => itemManager != null && itemManager.listOfitems.Count > 0);

        if (itemManager == null || itemManager.listOfitems.Count == 0)// för säkerhets skull.
        {
            Debug.LogError("itemManager is empty and/or missing.");
            
            yield break;

            }
        // överför föremål från itemmanager till alla items
        allitems = new List<ItemSystem>(itemManager.listOfitems);
        //laddar shoppen med föremål
        LoadShop();
        //sorterar shoppen
        SortingOfShopItems();
        //kallar till att updatera ui
        UpdateUI();
    }
      
// Start is called before the first frame update
void Start()
    {
        
       ItemManagerandMaker itemManager = FindObjectOfType<ItemManagerandMaker>();

        if ( itemManager == null)
        {
            Debug.LogError("itemManager is not found in the scene ");
            return;
        }

        StartCoroutine(InitializeShop());
        
    }
    public void LoadShop()
    {
      Debug.Log("Loading shop with (hopefully) random generated items based oppon rarity... ");
     if (allitems == null || allitems.Count == 0)
       {
           Debug.LogWarning("No items available,SHOP UNABLE TO POPULATE ITSELF!!");
            return;
       }
        shopitems.Clear();
        List<ItemSystem> itemsThatAreAvailable = new List<ItemSystem>(itemManager.listOfitems);

        int failedLoopAttempts = 0; //fr att motverka "infinite loops"
        const int maxFailedLoopAttempts = 20;
          
        while (shopitems.Count < maximumAmountOfItemsInShop && itemsThatAreAvailable.Count > 0)
        {

            ItemSystem selectedItem = GetByRarity(itemsThatAreAvailable);
            if (selectedItem != null)
            {
                shopitems.Add(selectedItem); //Jag lägger till det utvalda objectel till shoppen
                itemsThatAreAvailable.Remove(selectedItem); //Tar bort ifrån  mjliga föremål
                failedLoopAttempts = 0; // återställer
            } else
            {
                failedLoopAttempts++;
                if(failedLoopAttempts >= maxFailedLoopAttempts)
                {
                    Debug.LogWarning("Too many failed attepts,Stop item selection");
                    break;
                }
            }


        }
        
        //  shopitems = ShopRandomizer.GenerateShopItems(allitems);

        if (shopitems == null)// fr säkerhets skull.
        {
            
            Debug.LogError("Shopitems is null. Initializing a new list .");
                 shopitems = new List<ItemSystem>(); // I fall det in går sönder, Ändra tilbaka
        }
         
        Debug.Log($"Shop loaded, Amount of Items{shopitems.Count}"); 
    }

    // make own method and call before load
    ItemSystem GetByRarity(List<ItemSystem> itemsThatAreAvailable)
    {

        if (itemsThatAreAvailable == null || itemsThatAreAvailable.Count == 0 )
        {
            Debug.LogWarning("No items available, Unable to evaluattte items for rarity");
            return null;
        }

        //för att blanda innan val "SelectedItem"
        itemsThatAreAvailable = itemsThatAreAvailable.OrderBy(i => UnityEngine.Random.value).ToList();

        float rndValue = UnityEngine.Random.value; // Må vara felaktig //Slummässig nummer generator för affär-generatorn
        ItemSystem selectedItem = null;

        foreach (var item in itemsThatAreAvailable)
        {
            float inclusionItemRate = item.itemRarity switch
            {

                Rarity.Common => commonChance,
                Rarity.Uncommon => uncommonChance,
                Rarity.Rare => rareChance,
                Rarity.Epic => epicChance,
                Rarity.Legendary => legendaryChance,
                _ => 0.5f // standard sannolikhet
            };
            //ifall nummer generatorn är inom "inclusionItemRate", välj den
            if (rndValue <= inclusionItemRate)
            {
                selectedItem = item;
                Debug.Log($"Selected Item :  {item.itemName}, Rarity : {item.itemRarity}");
                return item;
                // avbryter när ett föremål har blivigt valt
            }
        }
         if (selectedItem !=null)
        {
            Debug.Log($"selexctedItem:  {selectedItem.itemName}, Rarityt: {selectedItem.itemRarity}, RandomVAL: {rndValue}");

        } else
        {
            Debug.LogWarning($"No item was chosen and selected. The randimun Value: {rndValue}");
        }
        return selectedItem;

    }

    void SortingOfShopItems()
    {
        shopitems.Sort((a, b) =>
        {

            if (a is Consumable && b is Armour) return -1; //  gör så att Consuable kommer före armour i sorting
            if (a is Armour && b is Consumable) return 1;
            return 0;
        });
    }

    public void UpdateUI()
    {
        ShopUI shopUI = FindObjectOfType<ShopUI>();
        if (shopUI != null)
        {
            shopUI.RefreshShopUI(shopitems);
        }
        //placeholder för UI 
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
