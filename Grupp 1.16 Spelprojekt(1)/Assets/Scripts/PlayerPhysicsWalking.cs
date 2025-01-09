using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsWalking : MonoBehaviour
{
    //Jag börjar med att definera värden av olika slag

    [Header("Values of Walking and Sprinting")]
    public float maxWalkSpeed = 3f;
    public float sprintSpeed = 8f;
    public float accelerationofWalk = 5f;
    public float decelerationofWalk = 10f;
    public float turn = 5f;


    [Header("Stamina Values")]
    public float maxStamnina = 100f;
    public float staminaDeductionRate = 10f;
    public float staminaRegen = 5f;
    public float currentStamina;

    [Header("Physics Values")]
    public float angularDrag = 5f;
    public float linearjarDrag = 5f;

    //Sedan en 2d Rigidbody som ska hantera en input och velocitetn av spelaren.

    private Rigidbody2D rigid2d;

    private Vector2 inputOfMoving;
    private Vector2 CurrentVelocity;


    private bool isRunning;
    private bool canRun;
    // Start is called before the first frame update
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();


        currentStamina = maxStamnina;

        rigid2d.gravityScale = 0f;
        rigid2d.drag = linearjarDrag;
        rigid2d.angularDrag = angularDrag;


    }

    // Update is called once per frame
    void Update()
    {
        inputOfMoving = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        isRunning = Input.GetKey(KeyCode.LeftShift) && canRun;
        HandleStamina();
    }


    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyDynamicTurning();
    }

    void ApplyMovement()
    {
        float targetSpeed = isRunning ? sprintSpeed : maxWalkSpeed;

        //Jag får velositeten
        Vector2 targVelocity = inputOfMoving * targetSpeed;

        // Vector2 targVelocity = inputOfMoving * maxWalkSpeed;

        //Accerlation och minskning när spelare rör sig
        if (inputOfMoving.magnitude > 0)
        {
            CurrentVelocity = Vector2.MoveTowards(CurrentVelocity, targVelocity, accelerationofWalk * Time.fixedDeltaTime);
        }
        else
        {
            CurrentVelocity = Vector2.MoveTowards(CurrentVelocity, Vector2.zero, decelerationofWalk * Time.fixedDeltaTime);
        }

        rigid2d.velocity = CurrentVelocity;

    }
    void ApplyDynamicTurning()
    {
        if (inputOfMoving.magnitude > 0)
        {
            //räknar vinkeln
            float targAngle = Mathf.Atan2(inputOfMoving.y, inputOfMoving.x) * Mathf.Rad2Deg;

            float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, targAngle, turn * Time.fixedDeltaTime);

            rigid2d.rotation = smoothAngle;
        }
    }

    public void HandleStamina()
    {


        if (isRunning)
        {
            currentStamina -= staminaDeductionRate * Time.deltaTime;


            if (currentStamina <= 0)
            {
                currentStamina = 0;
                // Jag stänger av spring funktionen
                canRun = false;
            }
        }
        else
        {
            //När man inte springer
            currentStamina += staminaRegen * Time.deltaTime;
            if (currentStamina >= maxStamnina)
            {
                currentStamina = maxStamnina;
            }
            //När man får springa igen.
            if (currentStamina > 10f)
            {
                canRun = true;
            }
        }
    }
}
