using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed = 4.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("bullet")) // Anv�nd gameObject f�r att komma �t taggen
        {
            Destroy(gameObject); // F�rst�r detta GameObject
        }
    }

}
