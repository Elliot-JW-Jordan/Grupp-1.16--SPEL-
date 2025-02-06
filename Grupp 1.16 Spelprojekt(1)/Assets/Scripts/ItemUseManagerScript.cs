using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ItemUseManagerScript : MonoBehaviour
{
    private playerHealth playerHealth;
    private Enemyhealth enemyhealth;
    private PlayerPhysicsWalking physicsWalking;
    ItemManagerandMaker itemManagerandMaker;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<playerHealth>();
        enemyhealth = FindObjectOfType<Enemyhealth>();
        physicsWalking = FindObjectOfType<PlayerPhysicsWalking>();
        itemManagerandMaker = FindObjectOfType<ItemManagerandMaker>();

        if(enemyhealth == null) {
            Debug.LogWarning("No EnemyHealth found in scene, making a new one..");
            // skapar enn GameObject och l�gger till Enemyhealth till objectet
            GameObject enemy = new GameObject("Enemy");
            enemyhealth = enemy.AddComponent<Enemyhealth>();
            // l�gger till null referance protection senare
        
        
        }
        




    }
    //Nedan finns det en metod som ska kunna anv�nda vilket f�rm�l som helst ooch appilicera dess egenskaper
    public void UseItem(ItemSystem item)
    {
        if (item is Consumable consumable) //Om f�rem�let �r av Consumable Typen
        {
            ApplyConsumableEffect(consumable);

        } else if (item is Armour  armour) { //Om f�rem�let �r av Armour Typen
            EquipArmour(armour);

        }
    }
    private void ApplyConsumableEffect(Consumable consumable)
    {
        //Ifall f�rem�let har indelningen HealingPotion
        if (consumable.consumableType1 == ConsumableType.HealingPotion)
        {
            Debug.Log($"Call HealPlayer method, {consumable.healAmount}");
            playerHealth.HealPlayer(consumable.healAmount); //m� vara felaktig byter till fungerande senare
        }
        if (consumable.consumableType1 == ConsumableType.Buff)
        {

            ApplyBuff(consumable.buffingFactor, consumable.duration);
        }
    }

    private void ApplyBuff(float buffFactor, float duration)
    {
        if (enemyhealth == null)
        {
            Debug.LogWarning("Enemyhealth is not found. The buff can not be applied");
            return;
        }


        Debug.Log($"Call ApplyDamageBuff method, ApplyBuff : {buffFactor} duration {duration}");

        enemyhealth.ApplyDamageBuff(buffFactor, duration);
    }
    public static Dictionary<string, Armour> equippedArmour = new Dictionary<string, Armour>
    {
        {"Helmet", null },
        {"ChestPlate", null },
        {"Leggings", null },
        {"Boots", null },
    };

    public void EquipArmour(Armour armour)
    {
        string armourType = armour.armourType.ToString();
        Debug.Log($"Equipping {armourType} armor : {armour.itemName} ");

        // Byter ut exsisterande armor med en utav samma sort. 
        if (equippedArmour.ContainsKey(armourType))
        {
            equippedArmour[armourType] = armour;
        } else
        {
            Debug.LogWarning($"Armor type : {armourType} is not possible(supported)");
            return;
        }
        playerHealth.WithArmour();
        physicsWalking.MovementEffectOfArmour();


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
