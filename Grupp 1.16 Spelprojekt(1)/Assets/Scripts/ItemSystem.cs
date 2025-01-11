using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Grund "Item" klass
[Serializable]
public abstract class ItemSystem 
{
    public string itemName;
    public string description;
    public Sprite spriteIcon;
    public int id;
    public Rarity itemRarity;
    public int price;


    public Dictionary<string, float> stats = new Dictionary<string, float>(); //Sk<par dynamisk statestic för föremål
    public event Action<ItemSystem> OnUsageOfItem; // Event som berättar när ett föremål används;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName + " .");
        OnUsageOfItem?.Invoke(this);
    }
   
}

public enum Rarity
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
 
    public float healAmount;
    public float buffingFactor;

    public override void Use()
    {
        base.Use();
        Debug.Log("Consumable effect:"+ "Duration :" + duration + "Heal :" + healAmount + "Buffing factor: " + buffingFactor + " .");
    }
}
public enum ConsumableType
{
    HealingPotion,
    Buff
}
[Serializable]
public class Armour : ItemSystem
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

public enum ArmourType
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