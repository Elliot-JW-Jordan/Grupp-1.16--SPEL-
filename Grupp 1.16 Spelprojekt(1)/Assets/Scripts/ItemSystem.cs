using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Grund "Item" klass
[Serializable]
public abstract class ItemSystem  //Skapar en mall för föremålet
{
    public string itemName; //namnet
    public string description; // förklarning
    public Sprite spriteIcon; //  bild på föremålet
    public int id; 
    public Rarity itemRarity; // föremålets utsedda sälsynhet
    public int price; // föremålets pris


    public Dictionary<string, float> stats = new Dictionary<string, float>(); //Sk<par dynamisk statestik för föremål
    public event Action<ItemSystem> OnUsageOfItem; // Event som berättar när ett föremål används;

    public virtual void Use() //en metod för när alla föremål används
    {
        Debug.Log("Using " + itemName + " ."); 
        OnUsageOfItem?.Invoke(this);
    }
   
}

public enum Rarity //skapar olika sällsynhets sorter
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary

}
[Serializable]
public class Consumable : ItemSystem 
{
    public ConsumableType consumableType1;
    public float duration; // Hu rlänge som effecten varar i sekunder
 
    public float healAmount; // Hur mycket HP som återställs
    public float buffingFactor; // faktorn något förbättras med

    public override void Use() // metod för anvädning
    {
        base.Use();
        Debug.Log("Consumable effect:"+ "Duration :" + duration + "Heal :" + healAmount + "Buffing factor: " + buffingFactor + " .");
    }
}
public enum ConsumableType // sorter ConsumalbeType
{
    HealingPotion,
    Buff
}
[Serializable]
public class Armour : ItemSystem // variabler for Rustning
{
    public ArmourType armourType;
    public float durability;
    public float defensiveValue;
    public float weightA; // VIKT FÖR RUSTNING

    public override void Use() 
    {
        base.Use();
        Debug.Log("Armour stats : " + " Durability :" + durability + " Defence : " + defensiveValue + "Weight :" + weightA);

    }
   
   
}

public enum ArmourType //Typ av rustning
{
    Helmet,
    Chestplate,
    Leggings,
    boots
}


public static class ItemMaker
{
    private static int nextID = 1;
    public static E Create<E>(
        string name,
        string description,
        Sprite icon,
        Rarity itemRarity,
        int price,
        Dictionary<string, float> stats = null,
        Enum type = null,
        float duration = 0,
         float healAmount = 0,
          float buffingFactor = 0,
          float durability = 0,
          float defensiveValue = 0,
          float weightA = 0 ) where E : ItemSystem, new()
    {
        E item = new E
        {
            itemName = name,
            description = description,
            spriteIcon = icon,
            itemRarity = itemRarity,
            price = price,
            id = nextID++,
            stats = stats ?? new Dictionary<string, float>()

        };
        //Hanterar consumable typen
        if (item is Consumable consumable && type is ConsumableType consumableType)
        {
            consumable.consumableType1 = consumableType;
            consumable.duration = duration;
            consumable.healAmount = healAmount;
            consumable.buffingFactor = buffingFactor;
        }

        if (item is Armour armour && type is ArmourType armourType)
        {
            armour.armourType = armourType;
            armour.durability = durability;
            armour.defensiveValue = defensiveValue;
            armour.weightA = weightA;
        }
        return item;
    }   





















}