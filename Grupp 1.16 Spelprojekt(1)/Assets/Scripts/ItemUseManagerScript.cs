using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ItemUseManagerScript : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Enemyhealth enemyhealth;
    private PlayerPhysicsWalking physicsWalking;
    ItemManagerandMaker itemManagerandMaker;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyhealth = FindObjectOfType<Enemyhealth>();
        physicsWalking = FindObjectOfType<PlayerPhysicsWalking>();
        itemManagerandMaker = FindObjectOfType<ItemManagerandMaker>();

    }
    //Nedan finns det en metod som ska kunna använda vilket förmål som helst ooch appilicera dess egenskaper
    public void UseItem(ItemSystem item)
    {
        if (item is Consumable consumable) //Om föremålet är av Consumable Typen
        {
            ApplyConsumableEffect(consumable);

        } else if (item is Armour  armour) { //Om föremålet är av Armour Typen
            EquipArmour(armour);

        }
    }



   

    private void ApplyConsumableEffect(Consumable consumable)
    {
        //Ifall föremålet har indelningen HealingPotion
        if (consumable.consumableType1 == ConsumableType.HealingPotion)
        {
            playerHealth.Heal(consumable.healAmount); //må vara felaktig byter till fungerande senare
        }
        if (consumable.consumableType1 == ConsumableType.Buff)
        {
            ApplyBuff(consumable.buffingFactor, consumable.duration);
        }
    }

    private void ApplyBuff(float buffFactor, float duration)
    {
        Debug.Log($"An item : /writeNameHere has called method ApplyBuff");
        enemyhealth.ApplyDamageBuff(buffFactor, duration);
    }

    private void EquipArmour(Armour armour)
    {


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
