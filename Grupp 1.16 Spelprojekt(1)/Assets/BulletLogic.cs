using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float destroyDelay = 1.0f;
    public int Enemytype = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - transform.position;

        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (Enemytype == 1)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            if (animator != null)
            {
                animator.Play("Fireball_Inpackt");
            }
            else
            {
                Debug.LogError("Animator not assigned!");
            }


            float BulletdeathAnimationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, BulletdeathAnimationLength);
        }

        if (Enemytype == 2) 
        {
            Destroy(gameObject);
        }
    }
}
