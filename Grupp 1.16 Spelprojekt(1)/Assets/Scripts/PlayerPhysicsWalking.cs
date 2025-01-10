using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsWalking : MonoBehaviour
{
    //Jag börjar med att definera värden av olika slag

    [Header("Values of Walking and Sprinting")]
    public float maxWalkSpeed = 4f; //Spelarens max hastighet
    public float sprintSpeed = 8f;  //Hastigheten som splearen springer i
    public float accelerationofWalk = 5f; // Accerleration
    public float decelerationofWalk = 10f; // decelaration
    public float turn = 5f;  //hastigheten av "vändningen"


    [Header("Stamina Values")]
    public float maxStamnina = 100f;  //
    public float staminaDeductionRate = 10f;
    public float staminaRegen = 5f;
    public float currentStamina;
    public float fatigueTreshhold = 8f;
    public float staminaFatiguerate = 0.5f;
    public float exhuastionPenalty = 0.5f;
    private bool isfatigued = false;
    private bool isExhausted = false;
    private float fatigueTime = 0f;



    [Header("Dodge Values")]
    public float dodgeSpeed = 15f;
    public float dogeTimer = 0.25f;
    public float dodgeDownTime = 1.5f;
    private bool isDoging = false;
    private bool canDodge = true;

    [Header("Physics Values")]
    public float angularDrag = 5f;
    public float linearjarDrag = 10f;


    [Header("Camera Setrtings")]
    public Camera camera;
    public CameraSShake cameraSShake;
    public float shakeMagnitude = 0.3f;
    public float shakeLe = 0.2f;


    [Header("Sprite Scaling")]
    public Transform playerSpr;
    public float minimumSprite = 0.8f;
    public float maxScale = 1.2f;

    //Sedan en 2d Rigidbody som ska hantera en input och velocitetn av spelaren.

    private Rigidbody2D rigid2d;

    private Vector2 inputOfMoving;
    private Vector2 CurrentVelocity;
    private Vector2 lastMoveDirection;


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

       // playerSpr = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        inputOfMoving = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (inputOfMoving != Vector2.zero)
        {
            lastMoveDirection = inputOfMoving;
        }



        isRunning = Input.GetKey(KeyCode.LeftShift) && canRun;

        if (Input.GetKeyDown(KeyCode.Space) && canDodge)
        {
            StartCoroutine(Dodge());
        }
        HandleStamina();
        ChangeSpriteScale();
    }


    private void FixedUpdate()
    {
        if (!isDoging)
        {
            ApplyMovement();
            ApplyDynamicTurning();
        }

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
            Vector2 directionsPredict = inputOfMoving;
            //räknar vinkeln
            float targAngle = Mathf.Atan2(directionsPredict.y, directionsPredict.x) * Mathf.Rad2Deg;
            Quaternion targetRotate = Quaternion.Euler(0, 0, targAngle);

            //float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.z, targAngle, turn * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotate, turn * Time.fixedDeltaTime);
            // rigid2d.rotation = smoothAngle;
        }
    }

    IEnumerator Dodge()
    {

        isDoging = true;
        canDodge = false;


        if (cameraSShake != null)
        {
            cameraSShake.BeginShake(shakeLe, shakeMagnitude);
        }

        //Så man alltid rullar åt höger ifall spelaren inte färdas i en definerad riktning


        Vector2 directionOfDodge = inputOfMoving != Vector2.zero ? inputOfMoving : lastMoveDirection;
        Vector2 velocityOfDodge = directionOfDodge * dodgeSpeed;

        float endOfDodge = Time.time + dogeTimer;

        while (Time.time < endOfDodge)
        {
            rigid2d.velocity = velocityOfDodge;
            yield return null;
        }

        rigid2d.velocity = Vector2.zero;

        isDoging = false;
        yield return new WaitForSeconds(dodgeDownTime);
        canDodge = true;
    }


    public void HandleStamina()
    {


        if (isRunning)
        {
            currentStamina -= staminaDeductionRate * Time.deltaTime;


            if (fatigueTime >= fatigueTreshhold && !isfatigued)
            {
                isfatigued = true;
            }

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                // Jag stänger av spring funktionen
                canRun = false;
                isExhausted = true;
                maxWalkSpeed *= exhuastionPenalty;
                sprintSpeed *= exhuastionPenalty;
            }
        }
        else
        {
            //När man inte springer
            float regenRate = isfatigued ? staminaFatiguerate : staminaRegen;
            //oj o oj stamina regen
            currentStamina += regenRate * Time.deltaTime;
            if (currentStamina >= maxStamnina)
            {
                currentStamina = maxStamnina;
            }
            //När man får springa igen.
            if (currentStamina > 10f)
            {
                canRun = true;
                if (isExhausted)
                {
                    maxWalkSpeed = 4;
                    sprintSpeed = 8f;
                    isExhausted = false;
                }
            }
            fatigueTime = 0f;
        }
    }
    void ChangeSpriteScale()
    {
        float speedF = Mathf.Clamp(rigid2d.velocity.magnitude / sprintSpeed, 0f, 1f);
        float newScale = Mathf.Lerp(minimumSprite, maxScale, 1f - speedF);

        if (playerSpr != null)
        {
            playerSpr.localScale = new Vector3(newScale, newScale, 1f);
        }

    }
}

