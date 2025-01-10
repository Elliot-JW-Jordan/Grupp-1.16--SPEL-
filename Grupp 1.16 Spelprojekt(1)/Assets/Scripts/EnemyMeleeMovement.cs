using UnityEngine;

public class EnemyAI_MoveTowardsPlayer : MonoBehaviour
{
    public float moveSpeed = 3f;  // Speed of enemy movement
    private Transform player;     // Reference to the player's transform
    private Rigidbody2D rb;       // Reference to the Rigidbody2D component

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Move the enemy towards the player
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate direction from enemy to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move the enemy using Rigidbody2D's velocity
        rb.velocity = direction * moveSpeed;
    }
}
