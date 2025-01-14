using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;

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
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            Destroy(gameObject);
        }

    }

}
