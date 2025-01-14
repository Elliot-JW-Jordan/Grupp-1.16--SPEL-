using UnityEngine;
using UnityEngine.Events;

public class Enemyhealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public int currencyDrop = 20;
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
        health -= amount;
        if (health < 0)
        {

         DropCurrency(); //kallar så att spelaren får pengar
            Destroy(gameObject);
            
        }

    }

     void DropCurrency()
    {
        OnCurrencyDropped?.Invoke(currencyDrop);
    }


}
