using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject playerBullet;
    public float BulletSpeed = 3f; // skottens hastighet
    public float shotCooldown = 0.5f; // Cooldown i sekunder
    private float lastShotTime = 0f;

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= lastShotTime + shotCooldown)
        {
            shot();
            lastShotTime = Time.time; // Uppdatera tiden för senaste skottet
        }
    }

    void shot()
    {
        GameObject bullet = Instantiate(playerBullet, transform.position, Quaternion.identity);

        // Hämta muspositionen i världskoordinater
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Korrigera z-axeln för 2D-utrymme
        mouseWorldPosition.z = 0f;

        // Beräkna riktningen till muspositionen
        Vector2 direction = ((Vector2)mouseWorldPosition - (Vector2)bullet.transform.position).normalized;

        // Ställ in bullet's Rigidbody2D hastighet för att röra sig mot muspositionen
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction * BulletSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }


    }
}
