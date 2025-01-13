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
           description: "Common: Heals 25 HP",
           icon: null,
          itemRarity: Rarity.Common,
          price: 10,
          stats: new Dictionary<string, float> { { "HealingPower", 25f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 25f,
          buffingFactor: 0f);


        Consumable HealingPotion = ItemMaker.Create<Consumable>(
           name: "Regular Healing Potion",
           description: "Uncommon: Heals 40 HP",
           icon: null,
          itemRarity: Rarity.Uncommon,
          price: 30,
          stats: new Dictionary<string, float> { { "HealingPower", 40f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 40f,
          buffingFactor: 0f);

        Consumable GreaterHealingPotion = ItemMaker.Create<Consumable>(
           name: "Greater Healing Potion",
           description: "Epic: Heals 80 HP",
           icon: null,
          itemRarity: Rarity.Epic,
          price: 50,
          stats: new Dictionary<string, float> { { "HealingPower", 80f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 80f,
          buffingFactor: 0f);

        Consumable DMGBuff = ItemMaker.Create<Consumable>(
        name: "Damange Enhacning Potion",
        description: "Common: Increases Damage With 1.1",
        icon: null,
       itemRarity: Rarity.Common,
       price: 30,
       stats: new Dictionary<string, float> { { "Damage Enhacing", 0f } },
       type: ConsumableType.HealingPotion,
       duration: 0f,
       healAmount: 0f,
       buffingFactor: 1.1f);


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
           stats: new Dictionary<string, float> { { "Armour Rating", 10f } },
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
           stats: new Dictionary<string, float> { { "Armour Rating", 10f } },
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
           stats: new Dictionary<string, float> { { "Armour Rating", 10f } },
           type: ArmourType.boots,
           durability: 80f,
           defensiveValue: 15f,
           weightA: 2.5f
           );

        listOfitems.Add(ssmallHealingPotion);
        listOfitems.Add(HealingPotion);
        listOfitems.Add(GreaterHealingPotion);
        listOfitems.Add(DMGBuff);
        listOfitems.Add(LeatherHelmet);
        listOfitems.Add(LeatherChestplate);
        listOfitems.Add(LeatherLeggings);
        listOfitems.Add(LeatherBoots);

        foreach (var item in listOfitems)
        {
            item.Use();
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
