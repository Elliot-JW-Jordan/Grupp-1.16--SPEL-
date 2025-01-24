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

    private float totalDefensivevalue = 0f;// från spelarens rustningar
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
        float reducedDamage = Mathf.Max(amount * (1f - damageRadeuctionperventage), 1f); //Spleraren tar åtmistonde 1 damage ovasett armour

        int finalDamage = Mathf.RoundToInt(reducedDamage);
        Debug.Log($"Original DMG to payer : {amount}, Reduced DMG : {finalDamage}, Reduction Percentage : {damageRadeuctionperventage * 100}%");

        
            health -= finalDamage;
            if (health < 0)
            {
                Destroy(GameObject);
            }
        UpdateHealthBar();

    }
    // Coroutine för att spela animationen och ladda scenen med fördröjning
    IEnumerator PlayAnimationAndLoadScene()
    {
        // Spela animationen
        ripFelix.Play("död_felix_rip");

        // Vänta på att animationen ska spela klart (t.ex. 3 sekunder, eller hur lång tid animationen är)
        yield return new WaitForSeconds(35); // Justera tiden efter din animation

        // Ladda scenen efter animationen
        SceneManager.LoadScene("testStart");
    }

    public void WithArmour()
    {
        float totalDefensiveValue = 0;

        //Nedan så kalkuleras the totalla värdet
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
        // ifall spelraren exempelvis har en totalDefensive value på 100 blir damageReduction procentet 50%, vilket är det absolut högsta
        float reduction = defense / (defense + 100f);
        // Så att man inte kan bli odödlig så Clampar jag till 75%
        return Mathf.Clamp(reduction, 0f, 0.75f);
    }




  public void HealPlayer(float healAmount)
    {
        Debug.Log($"ItemUseManagerScript called HealPlayer healAmount : {healAmount}");
        health += Mathf.RoundToInt(healAmount); // Detta konverterar healthAmount till en Integer
        Debug.Log($"Health after healing {healAmount}, health : {health}");
        if (health > maxHealth)
        {
            health = maxHealth; // för att se till så att health aldrig överstiger MaxHealth

        }
        UpdateHealthBar();

    }



    void UpdateHealthBar()
    {
        float healthPercentage = (float)health / maxHealth;
        green.fillAmount = healthPercentage; // Update the fill amount of the green health bar

    }

    

}
