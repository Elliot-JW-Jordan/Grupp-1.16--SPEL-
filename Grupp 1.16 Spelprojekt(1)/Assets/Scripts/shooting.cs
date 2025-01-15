using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab för kulan
    public Transform firePoint; // Positionen där kulan skjuts ut
    public float bulletSpeed = 10f; // Hastigheten för kulan
    public float Damage = 1f;

    void Update()
    {
        // Kolla musens position i världen
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Räkna ut riktningen mot musen
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Rotera spelaren/gun mot musen
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // Skjut när vänster musknapp trycks ned
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(direction);
        }
    }

    void Shoot(Vector3 direction)
    {
        // Skapa kulan vid firePoint och sätt dess riktning
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
}
