using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 3f;

    public projectile projectileprefab;
    public Transform LaunchOffset;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //make sure the player only moves when the player gives a input.
        rb.velocity = new Vector2(0, 0);


        //up down movment
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }

        //right left movment
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        if (Input.GetButtonDown("fire1"))
        {
            Instantiate(projectileprefab, LaunchOffset.position, transform.rotation);
        }
    }
}

