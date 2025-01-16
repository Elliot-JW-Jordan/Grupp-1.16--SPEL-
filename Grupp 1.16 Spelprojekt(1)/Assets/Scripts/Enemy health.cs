using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemyhealth : MonoBehaviour
{
    Animator animator;

    public int health;
    public int maxHealth = 10;
    public int currencyDrop = 20;

    public float buffFactor = 1;
    public float buffDuration = 0;
    public float buffTime = 0;
    public bool buffActive = false;
    public int dmgBerForeApplyBuff = 5;
    private int appliedincrease = 0;



    public ShopSystem shopSystem1;
    public UnityEvent<int> OnCurrencyDropped;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            TakeDamage(5);
        }
    }
    public void Initialize(ShopSystem shopSystem)
    {
        this.shopSystem1 = shopSystem;
    }
    public void TakeDamage(int amount)
    {
        //Jag applicerar buffen ifall den är aktiv
        if(buffActive)
        {
            amount += appliedincrease; //lägger till skillnaden mellan det obuffade DMG värdet och det Buffade DMG värdet som höjning.
        }
        health -= amount;
        if (health < 0)
        {

            animator.Play("Slime_death");

         DropCurrency(); //kallar så att spelaren får pengar
            Destroy(gameObject, 1);
            
            
        }

    }

     void DropCurrency()
    {
        OnCurrencyDropped?.Invoke(currencyDrop);
    }

   public void ApplyDamageBuff(float buffFactor,float durationOfBuff)
    {
        this.buffFactor = buffFactor;
        this.buffDuration = durationOfBuff; //Buff längden i sekunder
         if (buffActive) //ifall det redan finns en buff som påverkar så skapar metoden inte en till. Alltså den retunerar metoden.
        {
            return;
        }
        StartCoroutine(ApplyDamageBuffWithTimer(buffFactor, buffDuration));
          
    }
    private IEnumerator ApplyDamageBuffWithTimer(float buffFactor, float duration)
    {
        buffActive = true;
        buffTime = duration;
        int initialDMG = dmgBerForeApplyBuff; // Lagar det initiala värdet


        //Jag kalkulerar värdet av Buffen

        float buffedDMG = dmgBerForeApplyBuff * buffFactor;
        int buffedDMGint = Mathf.RoundToInt(buffedDMG);
        appliedincrease = buffedDMGint - dmgBerForeApplyBuff;

        Debug.Log($"A Damage buff has been activated, Applied : {buffedDMGint}, Increase : {appliedincrease}  ");
        // Väntar på att timer för Buffen ska ta slut;
        yield return new WaitForSeconds(duration);
        ResetBuff(initialDMG);

        // efter att timern har tagit slut, så blir buffActive till false, En ny buff kan då  startas!!
    }
    private void ResetBuff(int originalDMG)
    {
        Debug.Log("The previous buff is no more, Resetting the damage");
        dmgBerForeApplyBuff = originalDMG;
        buffActive = false; // buffen är inte längre aktiv
        buffTime = 0; // Nollställ timern

        
    }

    private void Update()
    {
        if (buffActive && buffTime > 0)
        {
            buffTime -= Time.deltaTime;
            if (buffTime <= 0)
            {
                ResetBuff(dmgBerForeApplyBuff);
            }
        }
    }
}
