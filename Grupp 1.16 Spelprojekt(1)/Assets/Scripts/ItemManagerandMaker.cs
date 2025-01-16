using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerandMaker : MonoBehaviour
{
    public  List<ItemSystem> listOfitems { get; private set; } = new List<ItemSystem>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateItems();
    }
    public void GenerateItems()
    {
        Consumable ssmallHealingPotion = ItemMaker.Create<Consumable>(
           name: "Smaller Healing Potion",
           description: "Common: Heals 2 HP",
           icon: null,
          itemRarity: Rarity.Common,
          price: 10,
          stats: new Dictionary<string, float> { { "HealingPower", 2f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 2f,
          buffingFactor: 0f);


        Consumable HealingPotion = ItemMaker.Create<Consumable>(
           name: "Regular Healing Potion",
           description: "Uncommon: Heals 4 HP",
           icon: null,
          itemRarity: Rarity.Uncommon,
          price: 30,
          stats: new Dictionary<string, float> { { "HealingPower", 4f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 4f,
          buffingFactor: 0f);

        Consumable GreaterHealingPotion = ItemMaker.Create<Consumable>(
           name: "Greater Healing Potion",
           description: "Epic: Heals 8 HP",
           icon: null,
          itemRarity: Rarity.Epic,
          price: 50,
          stats: new Dictionary<string, float> { { "HealingPower", 8f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 80f,
          buffingFactor: 0f);

        Consumable DMGBuff = ItemMaker.Create<Consumable>(
        name: "Damange Enhancing Potion",
        description: "Common: Increases Damage With 1.1",
        icon: null,
       itemRarity: Rarity.Common,
       price: 30,
       stats: new Dictionary<string, float> { { "Damage Enhacing", 1.1f } },
       type: ConsumableType.HealingPotion,
       duration: 200f,
       healAmount: 0f,
       buffingFactor: 1.1f);

        Consumable DMGBuffNumber2 = ItemMaker.Create<Consumable>(
       name: "Greater Damange Enhancing Potion",
       description: "Epic: Increases Damage With 1.18",
       icon: null,
      itemRarity: Rarity.Epic,
      price: 50,
      stats: new Dictionary<string, float> { { "Damage Enhacing", 1.18f } },
      type: ConsumableType.HealingPotion,
      duration: 300f,
      healAmount: 0f,
      buffingFactor: 1.18f);

        Consumable DMGBuffNumber3 = ItemMaker.Create<Consumable>(
      name: "Greater Damange Enhancing Potion",
      description: "Legendary: Increases Damage With 1.2",
      icon: null,
     itemRarity: Rarity.Legendary,
     price: 70,
     stats: new Dictionary<string, float> { { "Damage Enhacing", 1.2f } },
     type: ConsumableType.HealingPotion,
     duration: 360f,
     healAmount: 0f,
     buffingFactor: 1.2f);


        Armour LeatherHelmet = ItemMaker.Create<Armour>(
            name: "Weathered Leather Cap",
            description: "Weak and cheap, none the less, it is still protection ",
            icon: null,
            itemRarity: Rarity.Common,
            price: 70,
            stats: new Dictionary<string, float> { { "Armour Rating", 10f } },
            type: ArmourType.Helmet,
            durability: 80f,
            defensiveValue: 10f,
            weightA: 1.5f
            );

        Armour LeatherChestplate = ItemMaker.Create<Armour>(
           name: "Weathered Leather Chestplate",
           description: "Weak and cheap, none the less, it is still protection ",
           icon: null,
           itemRarity: Rarity.Common,
           price: 90,
           stats: new Dictionary<string, float> { { "Armour Rating", 15f } },
           type: ArmourType.Chestplate,
           durability: 90f,
           defensiveValue: 15f,
           weightA: 4f
           );




        Armour LeatherLeggings = ItemMaker.Create<Armour>(
           name: "Weathered Leather Leggings",
           description: "Weak and cheap, none the less, it is still protection ",
           icon: null,
           itemRarity: Rarity.Common,
           price: 90,
           stats: new Dictionary<string, float> { { "Armour Rating", 15f } },
           type: ArmourType.Leggings,
           durability: 85f,
           defensiveValue: 15f,
           weightA: 3.5f
           );



        Armour LeatherBoots = ItemMaker.Create<Armour>(
           name: "Weathered Leather Boots",
           description: "Weak and cheap, none the less, it is still protection ",
           icon: null,
           itemRarity: Rarity.Common,
           price: 80,
           stats: new Dictionary<string, float> { { "Armour Rating", 15f } },
           type: ArmourType.boots,
           durability: 80f,
           defensiveValue: 15f,
           weightA: 2.5f
           );

        listOfitems.Add(ssmallHealingPotion);
        listOfitems.Add(HealingPotion);
        listOfitems.Add(GreaterHealingPotion);
        listOfitems.Add(DMGBuff);
        listOfitems.Add(DMGBuffNumber2);
        listOfitems.Add(DMGBuffNumber3);
        listOfitems.Add(LeatherHelmet);
        listOfitems.Add(LeatherChestplate);
        listOfitems.Add(LeatherLeggings);
        listOfitems.Add(LeatherBoots);


        foreach (var item in listOfitems)
        {
            Debug.Log($"Geennerated Item : {item.itemName}, Rarity : {item.itemRarity}, Price: {item.price}");
            item.Use();
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
