using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
    
{
    public float maxHealth = 10f;  // Max health of the player (set to 10)
    public float health;           // Current health of the player
    public Image healthBarGreen;   // The green health bar image

    void Start()
    {
        health = maxHealth;  // Set health to max at the start
        UpdateHealthBar();   // Update health bar immediately on start
    }

    void Update()
    {
        // Example: this could decrease health over time or when damage is applied
        // For demonstration, we'll just reduce health by 1 every second
        // health -= 1 * Time.deltaTime; // Uncomment to simulate continuous damage

        // Update health bar whenever health changes
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // Calculate the percentage of health remaining
        float healthPercentage = health / maxHealth;

        // Update the fill amount of the green part (assuming it's an Image with Fill method)
        healthBarGreen.fillAmount = healthPercentage;

        // Alternatively, if you're scaling the green part manually, you can use:
        // Vector3 localScale = healthBarGreen.transform.localScale;
        // localScale.x = healthPercentage;
        // healthBarGreen.transform.localScale = localScale;
    }

    // Call this method to simulate taking damage
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
            health = 0;  // Prevent health from going below 0

        UpdateHealthBar();  // Update the health bar after taking damage
    }

    // Example method to heal (could be triggered elsewhere in the game)
    public void Heal(float amount)
    {
        Debug.Log($"HealthManagers method Heal added {amount} floats of healing to health");

        health += amount;
        if (health > maxHealth)
            health = maxHealth;  // Prevent health from exceeding max

        UpdateHealthBar();  // Update the health bar after healing
    }
}
