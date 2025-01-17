using UnityEngine;

public class EnemyAI_MoveTowardsPlayer : MonoBehaviour
{
    public float moveSpeed = 3f; 
    private Transform player;     
    private Rigidbody2D rb;      
    Animator animator;

    private float lastXPosition;
    private float lastYPosition;

    public string predominantDirection = "None";

    void Start()
    {
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();

        lastXPosition = transform.position.x;
        lastYPosition = transform.position.y;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            MoveTowardsPlayer();

            CalculatePredominantDirection();

            if (predominantDirection == "Negative X")
            {
                animator.Play("Eye_Side_Move");

                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
            else if (predominantDirection == "Positive X")
            {
                animator.Play("Eye_Side_Move");

                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
            else if (predominantDirection == "Positive Y")
            {
                animator.Play("Eye_Back_Move");
            }
            else if (predominantDirection == "Negative Y")
            {
                animator.Play("Eye_Fram_Walk");
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    void CalculatePredominantDirection()
    {
        float deltaX = Mathf.Abs(transform.position.x - lastXPosition);
        float deltaY = Mathf.Abs(transform.position.y - lastYPosition);

        if (deltaX > deltaY)
        {
            predominantDirection = transform.position.x > lastXPosition ? "Positive X" : "Negative X";
        }
        else if (deltaY > deltaX)
        {
            predominantDirection = transform.position.y > lastYPosition ? "Positive Y" : "Negative Y";
        }
        else
        {
            predominantDirection = "None";
        }

        lastXPosition = transform.position.x;
        lastYPosition = transform.position.y;

        Debug.Log("Predominant Direction: " + predominantDirection);
    }
}
