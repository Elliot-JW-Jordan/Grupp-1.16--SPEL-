using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private void Start()
    {
        // H�mta musens position i v�rldsrummet
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Ber�kna riktningen fr�n objektet till musen
        Vector3 direction = mousePosition - transform.position;

        // Eftersom vi jobbar i 2D, s�tt Z-komponenten till 0
        direction.z = 0;

        // Ber�kna vinkeln i grader f�r rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // S�tt rotationen f�r objektet (endast Z-axeln roteras)
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // F�rst�r kulan vid kollision
    }
}
