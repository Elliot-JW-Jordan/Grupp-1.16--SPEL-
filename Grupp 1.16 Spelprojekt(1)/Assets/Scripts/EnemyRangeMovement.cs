using UnityEngine;

public class EnemyRandomMovementWithAvoidance : MonoBehaviour
{
    public float moveSpeed = 3f;              // Speed of movement
    public float changeDirectionTime = 2f;    // Time before changing direction
    public Vector2 moveArea = new Vector2(5f, 5f); // The area in which the enemy can move (x = width, y = height)
    public float playerDetectionRange = 1f;   // Distance at which the enemy will move away from the player
    public float moveAwayDistance = 1f;       // How far the enemy moves away from the player
    public float cooldownTime = 3f;           // Time the enemy waits after avoiding the player

    private Vector3 targetPosition;          // The current target position
    private float timer = 0f;                 // Timer to change direction or cooldown
    private bool isMoving = false;            // Flag to track whether the enemy is moving
    private bool isCooldown = false;          // Flag to track cooldown state
    private float cooldownTimer = 0f;         // Timer to track cooldown time
    private Transform player;                 // Reference to the player

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isCooldown)
        {
            if (!isMoving)
            {
                timer += Time.deltaTime;

                // Change direction after the set time
                if (timer >= changeDirectionTime)
                {
                    SetRandomTargetPosition();
                    timer = 0f;
                }
            }

            // Move towards the target position
            MoveTowardsTarget();

            // Check for player proximity and move away if necessary
            CheckPlayerProximity();
        }
        else
        {
            // Cooldown logic
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                isCooldown = false;
                cooldownTimer = 0f;
            }
        }
    }

    void SetRandomTargetPosition()
    {
        // Generate a random position within the specified area
        float randomX = Random.Range(-moveArea.x, moveArea.x);
        float randomY = Random.Range(-moveArea.y, moveArea.y);

        // Set the target position
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
        isMoving = true;
    }

    void MoveTowardsTarget()
    {
        if (targetPosition != null)
        {
            // Calculate direction to move in
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();

            // Move the enemy towards the target position
            transform.position += direction * moveSpeed * Time.deltaTime;

            // If the enemy has reached the target position, stop moving
            if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            {
                isMoving = false;
            }
        }
    }

    void CheckPlayerProximity()
    {
        // Check if the player is within the detection range
        if (Vector3.Distance(transform.position, player.position) < playerDetectionRange)
        {
            // Move away from the player
            MoveAwayFromPlayer();
        }
    }

    void MoveAwayFromPlayer()
    {
        // Move away from the player by a set distance
        Vector3 directionAwayFromPlayer = transform.position - player.position;
        directionAwayFromPlayer.Normalize();

        // Set a target position that is `moveAwayDistance` units away from the player
        targetPosition = transform.position + directionAwayFromPlayer * moveAwayDistance;

        // Set the flag to move the enemy
        isMoving = true;

        // Set a cooldown before checking player proximity again
        isCooldown = true;
    }
}
