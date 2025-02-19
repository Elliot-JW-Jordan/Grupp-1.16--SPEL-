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
    public int appliedincrease = 0;

    public bool EnemyTyp2 = false;
    public GameObject BloodStains;

    public ShopSystem shopSystem1;
    public UnityEvent<int> OnCurrencyDropped;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"Collided with: {other.gameObject.name}");
        if (other.gameObject.CompareTag("bullet"))
        {
            TakeDamage(2);
        }
    }

    public void Initialize(ShopSystem shopSystem)
    {
        this.shopSystem1 = shopSystem;
    }
    public void TakeDamage(int amount)
    {
        //Jag applicerar buffen ifall den �r aktiv
        if(buffActive)
        {
            amount += appliedincrease; //l�gger till skillnaden mellan det obuffade DMG v�rdet och det Buffade DMG v�rdet som h�jning.
        }
        health -= amount;
        if (health < 0)
        {
            animator.Play("Eye_Fram_die");
            float EyedeathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;

            Invoke(nameof(HandleDeathLogic), EyedeathAnimationLength);

            animator.Play("Slime_death");
            DropCurrency();
            float deathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, deathAnimationLength);


        }

    }

    private void HandleDeathLogic()
    {
        if (EnemyTyp2)
        {
            Instantiate(BloodStains, transform.position, Quaternion.identity);
        }

        DropCurrency();

        Destroy(gameObject);
    }

    void DropCurrency()
    {
        //L�gger till pengar 
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddCurrency(currencyDrop);
            Debug.Log($"Dropped {currencyDrop} currency.");

        }
        else
        {
            Debug.LogWarning("CurrencyManager instance is missing.");
        }
        OnCurrencyDropped?.Invoke(currencyDrop);
    }

   public void ApplyDamageBuff(float buffFactor,float durationOfBuff)
    {
        if (buffFactor <= 1.0f)
        {
            Debug.LogWarning($"BuffFactor must be equal to or greater than one, this BuffFactor had the value of  {buffFactor}");
            return;
       }

        if (buffActive) //ifall det redan finns en buff som p�verkar s� skapar metoden inte en till. Allts� den retunerar metoden.
        {
            return;
        }

        this.buffFactor = buffFactor;
            this.buffDuration = durationOfBuff; //Buff l�ngden i sekunder
           
            StartCoroutine(ApplyDamageBuffWithTimer(buffFactor, durationOfBuff));

       
       
    }
    private IEnumerator ApplyDamageBuffWithTimer(float buffFactor, float duration)
    {
        buffActive = true;
        buffTime = duration;
       
        int initialDMG = dmgBerForeApplyBuff; // Lagar det initiala v�rdet
        //Jag kalkulerar v�rdet av Buffen
        float buffedDMG = dmgBerForeApplyBuff * buffFactor;
        int buffedDMGint = Mathf.RoundToInt(buffedDMG);
        appliedincrease = buffedDMGint - dmgBerForeApplyBuff;
        Debug.Log($"A Damage buff has been activated, Applied : {buffedDMGint}, Increase : {appliedincrease}  ");
        // V�ntar p� att timer f�r Buffen ska ta slut;
        yield return new WaitForSeconds(duration);
        ResetBuff(initialDMG);

        // efter att timern har tagit slut, s� blir buffActive till false, En ny buff kan d�  startas!!
    }
    private void ResetBuff(int originalDMG)
    {
        Debug.Log("The previous buff is no more, Resetting the damage");
        dmgBerForeApplyBuff = originalDMG;
        buffActive = false; // buffen �r inte l�ngre aktiv
        buffTime = 0; // Nollst�ll timern

        
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
