using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDMGProjectile : MonoBehaviour
{
    public GameObject EnemyProjectile; // The projectile prefab
    public GameObject player; // Reference to the player's transform
    public float projectileSpeed = 10f; // Speed of the projectile
    public float fireRate = 1f; // Time between shots (in seconds)

    private float nextFireTime = 0f; // Time when the next shot can be fired

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Check if it's time to shoot
        if (Time.time >= nextFireTime)
        {
            shot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void shot()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference not set!");
            return;
        }

        // Calculate the direction to the player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Rotate the bullet spawn point towards the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Instantiate the projectile with the rotation facing the player
        GameObject projectile = Instantiate(EnemyProjectile, transform.position, rotation);

        // Assign velocity to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("Projectile prefab missing Rigidbody2D component!");
        }
    }
}
