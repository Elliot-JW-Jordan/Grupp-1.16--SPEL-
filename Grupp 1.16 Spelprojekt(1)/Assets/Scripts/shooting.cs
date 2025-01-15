using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab f�r kulan
    public Transform firePoint; // Positionen d�r kulan skjuts ut
    public float bulletSpeed = 10f; // Hastigheten f�r kulan
    public float Damage = 1f;

    void Update()
    {
        // Kolla musens position i v�rlden
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // R�kna ut riktningen mot musen
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Rotera spelaren/gun mot musen
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // Skjut n�r v�nster musknapp trycks ned
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(direction);
        }
    }

    void Shoot(Vector3 direction)
    {
        // Skapa kulan vid firePoint och s�tt dess riktning
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
}
