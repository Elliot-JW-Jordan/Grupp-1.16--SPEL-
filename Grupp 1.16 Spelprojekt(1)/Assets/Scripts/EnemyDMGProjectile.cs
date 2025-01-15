using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDMGProjectile : MonoBehaviour
{
    public GameObject EnemyProjectile; 
    public GameObject player; 
    public float projectileSpeed = 10f;
    public float fireRate = 1f;

    private float nextFireTime = 0f;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
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

        Vector3 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        GameObject projectile = Instantiate(EnemyProjectile, transform.position, rotation);

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
