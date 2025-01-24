using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class playerHealth : MonoBehaviour
{
    
    public int health;
    public int maxHealth = 10;
    public int healAmountQ = 0;
    public Image green;
    public int DamageOnPlayer = 2;

    private float totalDefensivevalue = 0f;// fr�n spelarens rustningar
    private float damageRadeuctionperventage = 0f; //procent baserad reduktion

    // Start is called before the first frame update
    void Start()
    {
        
        health = maxHealth;
        UpdateHealthBar();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(DamageOnPlayer);
        }
    }
    public void TakeDamage(int amount)
    {
        //Kalkulerar damage, Procent att redusera med
        float reducedDamage = Mathf.Max(amount * (1f - damageRadeuctionperventage), 1f); //Spleraren tar �tmistonde 1 damage ovasett armour

        int finalDamage = Mathf.RoundToInt(reducedDamage);
        Debug.Log($"Original DMG to payer : {amount}, Reduced DMG : {finalDamage}, Reduction Percentage : {damageRadeuctionperventage * 100}%");

        
            health -= finalDamage;
            if (health < 0)
            {
                Destroy(GameObject);
            }
        UpdateHealthBar();

    }
    // Coroutine f�r att spela animationen och ladda scenen med f�rdr�jning
    IEnumerator PlayAnimationAndLoadScene()
    {
        // Spela animationen
        ripFelix.Play("d�d_felix_rip");

        // V�nta p� att animationen ska spela klart (t.ex. 3 sekunder, eller hur l�ng tid animationen �r)
        yield return new WaitForSeconds(35); // Justera tiden efter din animation

        // Ladda scenen efter animationen
        SceneManager.LoadScene("testStart");
    }

    public void WithArmour()
    {
        float totalDefensiveValue = 0;

        //Nedan s� kalkuleras the totalla v�rdet
        foreach (var armour in ItemUseManagerScript.equippedArmour.Values)
        {
            if (armour != null)
            {
                totalDefensiveValue += armour.defensiveValue;
            }
        }
        Debug.Log($"tTotalDefensive value from armour player is wearing {totalDefensiveValue}");
        //damagereductionPercentage based on total defencevalue
        damageRadeuctionperventage = CalculateDamageReduction(totalDefensiveValue);
        Debug.Log($"tTotalDefensive value from armour player is wearing {totalDefensiveValue}, damage reduction percentage : {damageRadeuctionperventage * 100}%");


    }
    private float CalculateDamageReduction(float defense)
    {
        // ifall spelraren exempelvis har en totalDefensive value p� 100 blir damageReduction procentet 50%, vilket �r det absolut h�gsta
        float reduction = defense / (defense + 100f);
        // S� att man inte kan bli od�dlig s� Clampar jag till 75%
        return Mathf.Clamp(reduction, 0f, 0.75f);
    }




  public void HealPlayer(float healAmount)
    {
        Debug.Log($"ItemUseManagerScript called HealPlayer healAmount : {healAmount}");
        health += Mathf.RoundToInt(healAmount); // Detta konverterar healthAmount till en Integer
        Debug.Log($"Health after healing {healAmount}, health : {health}");
        if (health > maxHealth)
        {
            health = maxHealth; // f�r att se till s� att health aldrig �verstiger MaxHealth

        }
        UpdateHealthBar();

    }



    void UpdateHealthBar()
    {
        float healthPercentage = (float)health / maxHealth;
        green.fillAmount = healthPercentage; // Update the fill amount of the green health bar

    }

    

}
