using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerandMaker : MonoBehaviour
{
    [Header("ItemIcons")]
    public Sprite helmetSPRITE;
    public Sprite chestplateSPRITE;
    public Sprite leggingsSPRITE;
    public Sprite bootSprite;

    [Header("Consumable")]
    public Sprite powerUp;
    public Sprite healingP;

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
           icon: healingP,
          itemRarity: Rarity.Common,
          price: 10,
          stats: new Dictionary<string, float> { { "HealingPower", 2f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 8f,
          buffingFactor: 0f);


        Consumable tinyHealingPotion = ItemMaker.Create<Consumable>(
          name: "Tiny Healing Potion",
          description: "Common: Heals 1 HP",
          icon: healingP,
         itemRarity: Rarity.Common,
         price: 5,
         stats: new Dictionary<string, float> { { "HealingPower", 1f } },
         type: ConsumableType.HealingPotion,
         duration: 0f,
         healAmount: 5f,
         buffingFactor: 0f);


        Consumable HealingPotion = ItemMaker.Create<Consumable>(
           name: "Regular Healing Potion",
           description: "Uncommon: Heals 4 HP",
           icon: healingP,
          itemRarity: Rarity.Uncommon,
          price: 30,
          stats: new Dictionary<string, float> { { "HealingPower", 4f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 10f,
          buffingFactor: 0f);

        Consumable GreaterHealingPotion = ItemMaker.Create<Consumable>(
           name: "Greater Healing Potion",
           description: "Epic: Heals 8 HP",
           icon: healingP,
          itemRarity: Rarity.Epic,
          price: 50,
          stats: new Dictionary<string, float> { { "HealingPower", 8f } },
          type: ConsumableType.HealingPotion,
          duration: 0f,
          healAmount: 80f,
          buffingFactor: 0f);

        


        Consumable CheapHealing = ItemMaker.Create<Consumable>(
        name: "Cheap Healing Potion",
        description: "Legendary: Heals 30 HP",
        icon: healingP,
       itemRarity: Rarity.Legendary,
       price: 20,
       stats: new Dictionary<string, float> { { "HealingPower", 30f } },
       type: ConsumableType.HealingPotion,
       duration: 0f,
       healAmount: 30f,
       buffingFactor: 0f);


        Consumable GarbageHealing = ItemMaker.Create<Consumable>(
        name: "Garbage Healing Potion",
        description: "Common: Heals 8 HP",
        icon: healingP,
       itemRarity: Rarity.Common,
       price: 1,
       stats: new Dictionary<string, float> { { "HealingPower", 2f } },
       type: ConsumableType.HealingPotion,
       duration: 0f,
       healAmount: 2f,
       buffingFactor: 0f);


        Consumable GoofyHealing = ItemMaker.Create<Consumable>(
        name: "Goofy Healing Potion",
        description: "Epic: Heals 2 HP",
        icon: healingP,
       itemRarity: Rarity.Uncommon,
       price: 33,
       stats: new Dictionary<string, float> { { "HealingPower", 23f } },
       type: ConsumableType.HealingPotion,
       duration: 0f,
       healAmount: 23f,
       buffingFactor: 0f);


        Consumable DMGBuff = ItemMaker.Create<Consumable>(
        name: "Damange Enhancing Potion",
        description: "Common: Increases Damage With 1.1",
        icon: powerUp,
       itemRarity: Rarity.Common,
       price: 30,
       stats: new Dictionary<string, float> { { "Damage Enhacing", 1.1f } },
       type: ConsumableType.Buff,
       duration: 200f,
       healAmount: 0f,
       buffingFactor: 1.1f);

        Consumable DMGBuffNumber2 = ItemMaker.Create<Consumable>(
       name: "Greater Damange Enhancing Potion",
       description: "Epic: Increases Damage With 1.18",
       icon: powerUp,
      itemRarity: Rarity.Epic,
      price: 50,
      stats: new Dictionary<string, float> { { "Damage Enhacing", 1.18f } },
      type: ConsumableType.Buff,
      duration: 300f,
      healAmount: 0f,
      buffingFactor: 1.18f);

        Consumable DMGBuffNumber3 = ItemMaker.Create<Consumable>(
      name: "Greater Damange Enhancing Potion",
      description: "Legendary: Increases Damage With 1.2",
      icon: powerUp,
     itemRarity: Rarity.Legendary,
     price: 70,
     stats: new Dictionary<string, float> { { "Damage Enhacing", 1.2f } },
     type: ConsumableType.Buff,
     duration: 360f,
     healAmount: 0f,
     buffingFactor: 1.2f);

        Consumable DMGBuffNumber4 = ItemMaker.Create<Consumable>(
   name: "Tiny Damange Enhancing Potion",
   description: "Common: Increases Damage With 1.2",
   icon: powerUp,
  itemRarity: Rarity.Common,
  price: 70,
  stats: new Dictionary<string, float> { { "Damage Enhacing", 1.2f } },
  type: ConsumableType.Buff,
  duration: 30f,
  healAmount: 0f,
  buffingFactor: 1.2f);

        Consumable DMGBuffNumber5 = ItemMaker.Create<Consumable>(
  name: "Burst DMG Enhancing Potion",
  description: "Rare: Increases Damage With 1.9",
  icon: powerUp,
 itemRarity: Rarity.Rare,
 price: 100,
 stats: new Dictionary<string, float> { { "Damage Enhacing", 1.9f } },
 type: ConsumableType.Buff,
 duration: 9f,
 healAmount: 0f,
 buffingFactor: 1.9f);

        Consumable DMGBuffNumber6 = ItemMaker.Create<Consumable>(
  name: "Time-extended Enhancing Potion",
  description: "Uncommon: Increases Damage With 1.4",
  icon: powerUp,
 itemRarity: Rarity.Uncommon ,
 price: 50,
 stats: new Dictionary<string, float> { { "Damage Enhacing", 1.4f } },
 type: ConsumableType.Buff,
 duration: 100f,
 healAmount: 0f,
 buffingFactor: 1.4f);

        Consumable DMGBuffNumber7 = ItemMaker.Create<Consumable>(
  name: " Damange Enhancing Potion",
  description: "Common: Increases Damage With 1.2",
  icon: powerUp,
 itemRarity: Rarity.Common,
 price: 70,
 stats: new Dictionary<string, float> { { "Damage Enhacing", 1.2f } },
 type: ConsumableType.Buff,
 duration: 30f,
 healAmount: 0f,
 buffingFactor: 1.2f);

        Consumable DMGBuffNumber8= ItemMaker.Create<Consumable>(
  name: "DANGER dmg ",
  description: "Legendary: Increases Damage With 2",
  icon: powerUp,
 itemRarity: Rarity.Legendary,
 price: 300,
 stats: new Dictionary<string, float> { { "Damage Enhacing", 2f } },
 type: ConsumableType.Buff,
 duration: 30f,
 healAmount: 0f,
 buffingFactor: 2f);


        Armour LeatherHelmet = ItemMaker.Create<Armour>(
            name: "Weathered Leather Cap",
            description: "Weak and cheap, none the less, it is still protection ",
            icon: helmetSPRITE,
            itemRarity: Rarity.Common,
            price: 70,
            stats: new Dictionary<string, float> { { "Armour Rating", 10f } },
            type: ArmourType.Helmet,
            durability: 80f,
            defensiveValue: 10f,
            weightA: 1.5f
            );


        Armour IronHelmet = ItemMaker.Create<Armour>(
          name: "Old Iron Helmet",
          description: " A soliders standard helmet",
          icon: helmetSPRITE ,
          itemRarity: Rarity.Uncommon,
          price: 100,
          stats: new Dictionary<string, float> { { "Armour Rating", 15f } },
          type: ArmourType.Helmet,
          durability: 80f,
          defensiveValue: 15f,
          weightA: 2f
          );
        Armour SIronHelmet = ItemMaker.Create<Armour>(
        name: "Hard Iron Helmet",
        description: " A really hard helmet",
        icon: helmetSPRITE,
        itemRarity: Rarity.Rare,
        price: 150,
        stats: new Dictionary<string, float> { { "Armour Rating", 20f } },
        type: ArmourType.Helmet,
        durability: 80f,
        defensiveValue: 20f,
        weightA: 2f
        );

        Armour IronChestPlate = ItemMaker.Create<Armour>(
          name: "Old Iron Chestplate",
          description: " A soliders standard chestplate",
          icon: chestplateSPRITE,
          itemRarity: Rarity.Uncommon,
          price: 120,
          stats: new Dictionary<string, float> { { "Armour Rating", 25f } },
          type: ArmourType.Chestplate,
          durability: 80f,
          defensiveValue: 25f,
          weightA: 4f
          );

        Armour SIronChestPlate = ItemMaker.Create<Armour>(
        name: "Hard Iron Chestplate",
        description: " A really hard chestplate",
        icon: chestplateSPRITE,
        itemRarity: Rarity.Rare,
        price: 180,
        stats: new Dictionary<string, float> { { "Armour Rating", 30f } },
        type: ArmourType.Chestplate,
        durability: 90f,
        defensiveValue: 30f,
        weightA: 4f
        );


        Armour IronLeggings = ItemMaker.Create<Armour>(
         name: "Old Iron Leggings",
         description: " A soliders standard Leggings",
         icon: leggingsSPRITE,
         itemRarity: Rarity.Uncommon,
         price: 120,
         stats: new Dictionary<string, float> { { "Armour Rating", 25f } },
         type: ArmourType.Leggings,
         durability: 80f,
         defensiveValue: 25f,
         weightA: 4f
         );

        Armour SIronLeggings = ItemMaker.Create<Armour>(
        name: "Hard Iron Leggings",
        description: " A really hard pair of Leggings",
        icon: leggingsSPRITE,
        itemRarity: Rarity.Rare,
        price: 180,
        stats: new Dictionary<string, float> { { "Armour Rating", 30f } },
        type: ArmourType.Leggings,
        durability: 80f,
        defensiveValue: 30f,
        weightA: 4f
        );

        Armour IronBoots = ItemMaker.Create<Armour>(
        name: "Old Iron Boots",
        description: " A soliders standard Boot",
        icon: bootSprite,
        itemRarity: Rarity.Uncommon,
        price: 110,
        stats: new Dictionary<string, float> { { "Armour Rating", 15f } },
        type: ArmourType.boots,
        durability: 80f,
        defensiveValue: 15f,
        weightA: 2f
        );


        Armour SIronBoots = ItemMaker.Create<Armour>(
       name: "Hard Iron Boots",
       description: " A Hard pair of Boots",
       icon: bootSprite,
       itemRarity: Rarity.Rare,
       price: 160,
       stats: new Dictionary<string, float> { { "Armour Rating", 20f } },
       type: ArmourType.boots,
       durability: 80f,
       defensiveValue: 20f,
       weightA: 2f
       );



        Armour LeatherChestplate = ItemMaker.Create<Armour>(
           name: "Weathered Leather Chestplate",
           description: "Weak and cheap, none the less, it is still protection ",
           icon: chestplateSPRITE,
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
           icon: leggingsSPRITE,
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
           icon: bootSprite,
           itemRarity: Rarity.Common,
           price: 80,
           stats: new Dictionary<string, float> { { "Armour Rating", 15f } },
           type: ArmourType.boots,
           durability: 80f,
           defensiveValue: 15f,
           weightA: 2.5f
           );
        
        listOfitems.Add(ssmallHealingPotion);
        listOfitems.Add(tinyHealingPotion);
        listOfitems.Add(HealingPotion);
        listOfitems.Add(GreaterHealingPotion);
        listOfitems.Add(CheapHealing);
        listOfitems.Add(GoofyHealing);
        listOfitems.Add(GarbageHealing);

        listOfitems.Add(IronHelmet);
        listOfitems.Add(IronChestPlate);
        listOfitems.Add(IronLeggings);
        listOfitems.Add(IronBoots);
        listOfitems.Add(SIronBoots);
        listOfitems.Add(SIronChestPlate);
        listOfitems.Add(SIronHelmet);
        listOfitems.Add(SIronLeggings);
        listOfitems.Add(DMGBuff);
        listOfitems.Add(DMGBuffNumber2);
        listOfitems.Add(DMGBuffNumber3);
        listOfitems.Add(DMGBuffNumber4);
        listOfitems.Add(DMGBuffNumber5);
        listOfitems.Add(DMGBuffNumber6);
        listOfitems.Add(DMGBuffNumber7);
        listOfitems.Add(DMGBuffNumber8);
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
