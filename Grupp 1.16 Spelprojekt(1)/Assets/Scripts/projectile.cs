using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class projectile : MonoBehaviour
{
    public GameObject boss1bullet;
    public float BulletSpeed = 3f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            shot();
        }
    }

    void shot()
    {
        GameObject bullet = Instantiate(boss1bullet, transform.position, Quaternion.identity);

        // Get the mouse position in world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Correct the z-axis for 2D space
        mouseWorldPosition.z = 0f;

        // Calculate the direction to the mouse position
        Vector2 direction = ((Vector2)mouseWorldPosition - (Vector2)bullet.transform.position).normalized;

        // Set the bullet's Rigidbody2D velocity to move toward the mouse position
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction * BulletSpeed;
        }
    }
}













