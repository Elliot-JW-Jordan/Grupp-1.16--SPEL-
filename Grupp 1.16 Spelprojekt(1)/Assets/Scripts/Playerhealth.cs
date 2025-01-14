using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Playerhealth : MonoBehaviour


{
    public int health;
    public int maxHealth = 10;
    public Image green;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            Destroy(gameObject);
        }
        UpdateHealthBar();
        

    }
    void UpdateHealthBar()
    {
        float healthPercentage = (float)health / maxHealth;
        green.fillAmount = healthPercentage; // Update the fill amount of the green health bar
    }


}