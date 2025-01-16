using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private void Start()
    {
        // Hämta musens position i världsrummet
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Beräkna riktningen från objektet till musen
        Vector3 direction = mousePosition - transform.position;

        // Eftersom vi jobbar i 2D, sätt Z-komponenten till 0
        direction.z = 0;

        // Beräkna vinkeln i grader för rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Sätt rotationen för objektet (endast Z-axeln roteras)
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Förstör kulan vid kollision
    }
}
