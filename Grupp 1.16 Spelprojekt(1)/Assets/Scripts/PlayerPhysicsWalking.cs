using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPhysicsWalking : MonoBehaviour//AI "RENAD"
{
    Animator animator;
    private int FelixDirection = 0;

    [Header("Values of Walking and Sprinting")]
    public float maxWalkSpeed = 20f; //Maximala hastigheten som felix kkan gåå ii
    public float sprintSpeed = 30f;
    public float accelerationofWalk = 20f;
    public float decelerationofWalk = 50f;
    public float turn = 40f;

    private bool isFlipping = false; // kollar ifall spelraren flipper eller inte

    [Header("Stamina Values")]
    public float maxStamnina = 100f;
    public float staminaDeductionRate = 10f;
    public float staminaRegen = 15f;
    public float currentStamina;
    public float fatigueTreshhold = 8f;
    public float staminaFatiguerate = 0.5f;
    public float exhuastionPenalty = 0.5f;
    private bool isfatigued = false;
    private bool isExhausted = false;
    private float fatigueTime = 0f;

    [Header("Dodge Values")]
    public float dodgeSpeed = 60f;
    public float dogeTimer = 0.2f;
    public float dodgeDownTime = 1.5f;
    private bool isDoging = false;
    private bool canDodge = true;

    [Header("Physics Values")]
    public float angularDrag = 40f;
    public float linearjarDrag = 30f;

    [Header("Camera Settings")]
    public Camera camera;
    public CameraSShake cameraSShake;
    public float shakeMagnitude = 0.5f;
    public float shakeLe = 0.2f;

    [Header("Dynamic Camera effects")]
    public float zoomspeed = 0.15f;
    public float fovReduction;
    private float defaultFOV;
    private float minFov;
    public float pulseFrequency = 1.5f;
    public float pulseIntensity = 0.1f;

    [Header("Sprite Scaling")]
    public Transform playerSpr;
    public float minimumSprite = 0.8f;
    public float maxScale = 1.2f;

    private Rigidbody2D rigid2d;

    private Vector2 inputOfMoving;
    private Vector2 CurrentVelocity;
    private Vector2 lastMoveDirection;

    private bool isRunning;
    private bool canRun;

    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = maxStamnina;
        rigid2d.gravityScale = 0f;
        rigid2d.drag = linearjarDrag;
        rigid2d.angularDrag = angularDrag;

        defaultFOV = camera.fieldOfView;
        minFov = defaultFOV - fovReduction;
    }

    void Update()
    {
        // jag hämtar rörelse riktningen
       inputOfMoving = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (inputOfMoving != Vector2.zero)
        {
            lastMoveDirection = inputOfMoving;
        }

        isRunning = (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Fire3")) && canRun;

        if ((Input.GetKeyDown(KeyCode.Space) && canDodge))
            {
       StartCoroutine(Dodge());
        }

        HandleStamina();
        ChangeSpriteScale();
        UpdateCameraEffects();
        Animation();
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

        Vector2 targVelocity = inputOfMoving * targetSpeed;

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
        //Börjar bara flippa ifalll spelaren inte gör det och spelaren rörs sig horizontielt
        if (inputOfMoving.x != 0 && !isFlipping) //Om spelaren inte FLIPPAR och input of moving x inte är lika med noll

        {
            float targetScaleX = inputOfMoving.x > 0 ? 1f : -1f; 

            // karantäner börjar bARA OM rörelse riktningen förrändras 
            if (Mathf.Sign(transform.localScale.x) !=Mathf.Sign(targetScaleX))
            {

                // Påbörjer en corountine baserat på inputen
                StartCoroutine(ForASmoothHorizontalFlip(inputOfMoving.x > 0 ? 1f : -1f, turn));

            }

            
        }

    }

    IEnumerator ForASmoothHorizontalFlip(float targetScaleX, float smoothness)
    {
       // if fall flip funtionen inte behövs 
       if (Mathf.Sign(transform.localScale.x) == Mathf.Sign(targetScaleX))
        {
            yield break;
        }



        isFlipping = true; //Signalerar att figuren "flippar"
        //hämtar X skalan av spelaren
        float currentScaleX = transform.localScale.x;
        //sluta inte med skalan innan den når targetScaleX
        while (!Mathf.Approximately(currentScaleX, targetScaleX))
        {// Flytta skalan till målskalam
            currentScaleX = Mathf.MoveTowards(currentScaleX, targetScaleX, Time.deltaTime * smoothness);
            currentScaleX = InOutEasing(currentScaleX, targetScaleX, smoothness);
            //Uppdaterar skala av x
            transform.localScale = new Vector3(currentScaleX, transform.localScale.y, transform.localScale.z);
            yield return null; // Den väntar på nästa frame
        }
        //snappar till målet, Exakt
      
        transform.localScale = new Vector3(currentScaleX, transform.localScale.y, transform.localScale.z); //Snappar spelaren till targetscale.x
        isFlipping = false; //spelaren har lyckats med att flippa
    }

    float InOutEasing(float start, float end, float speed)
    {
        float floatT = Mathf.Clamp01((Time.time * speed) % 1f);
        return Mathf.Lerp(start, end, floatT * floatT * (3f - 2f * floatT));
    }

    IEnumerator Dodge()
    {
        isDoging = true;
        canDodge = false;

        if (cameraSShake != null)
        {
            cameraSShake.BeginShake(shakeLe, shakeMagnitude);
        }

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
            fatigueTime += Time.deltaTime;

            if (fatigueTime >= fatigueTreshhold && !isfatigued)
            {
                isfatigued = true;
            }

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                canRun = false;
                isExhausted = true;
                maxWalkSpeed *= exhuastionPenalty;
                sprintSpeed *= exhuastionPenalty;

                if (currentStamina <= 0 && !isfatigued)
                {
                    StartCoroutine(SecondWind());
                }
            }
        }
        else
        {
            float regenRate = isfatigued ? staminaFatiguerate : staminaRegen;
            currentStamina += regenRate * Time.deltaTime;
            if (currentStamina >= maxStamnina)
            {
                currentStamina = maxStamnina;
            }

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

    IEnumerator SecondWind()
    {
        float recoveryTime = 2f;
        float recoveryAmount = maxStamnina * 0.3f;
        float startRecoveryTime = Time.time;

        while (Time.time < startRecoveryTime + recoveryTime)
        {
            currentStamina += (recoveryAmount / recoveryTime) * Time.deltaTime;
            yield return null;
        }
        currentStamina = Mathf.Min(currentStamina, maxStamnina);
        isExhausted = false;
    }

    void ChangeSpriteScale()
    {
        float speedF = Mathf.Clamp(rigid2d.velocity.magnitude / sprintSpeed, 0f, 1f);
        float newScale = Mathf.Lerp(minimumSprite, maxScale, 1f - speedF);

        if (playerSpr != null)
        {
            // bevarar den förra vändningen 
            float currentSign = Mathf.Sign(playerSpr.localScale.x);

            playerSpr.localScale = new Vector3(newScale * currentSign, newScale, 1f);
        }
    }

    void UpdateCameraEffects()
    {
        float currentSpeed = rigid2d.velocity.magnitude;
        float targetFOV = Mathf.Lerp(minFov, defaultFOV, currentSpeed / sprintSpeed);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, zoomspeed * Time.deltaTime);

        if (isfatigued || isExhausted)
        {
            float pulseOffset = Mathf.Sin(Time.time * pulseFrequency) * pulseIntensity;
            Vector3 currentPosition = camera.transform.localPosition;
            camera.transform.localPosition = Vector3.Lerp(currentPosition, new Vector3(currentPosition.x, currentPosition.y + pulseOffset, currentPosition.z), 0.1f);
        }
    }
    public void MovementEffectOfArmour()
    {

    }




    void Animation()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            FelixDirection = 1;
            animator.Play("Felix_walk_baksidan");
        }
        else if (!Input.GetKey(KeyCode.W) && FelixDirection == 1)
        {
            animator.Play("idle animation baksidan");
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            FelixDirection = 2;
            animator.Play("Felix_walk_framsidan");
        }
        else if (!Input.GetKey(KeyCode.S) && FelixDirection == 2)
        {
            animator.Play("idle_framsidan_Felix 0");
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            FelixDirection = 3;
            animator.Play("Felix_sida_walk");
        }
        else if (!Input.GetKey(KeyCode.A) && FelixDirection == 3)
        {
            animator.Play("Felix_sida_idle");
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {
            FelixDirection = 4;
            animator.Play("Felix_sida_walk");
        }
        else if (!Input.GetKey(KeyCode.D) && FelixDirection == 4)
        {
            animator.Play("Felix_sida_idle");
        }
    }

}
    


