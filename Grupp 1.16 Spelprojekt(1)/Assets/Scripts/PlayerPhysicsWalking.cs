    using JetBrains.Annotations;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class PlayerPhysicsWalking : MonoBehaviour
    {
        Animator animator;
        private int FelixDirection = 0;

        //Jag b�rjar med att definera v�rden av olika slag

        [Header("Values of Walking and Sprinting")]
        public float maxWalkSpeed = 4f; //Spelarens max hastighet
        public float sprintSpeed = 8f;  //Hastigheten som splearen springer i
        public float accelerationofWalk = 5f; // Accerleration
        public float decelerationofWalk = 10f; // decelaration
        public float turn = 20f;  //hastigheten av "v�ndningen"


        [Header("Stamina Values")]
        public float maxStamnina = 100f;  //
        public float staminaDeductionRate = 10f;
        public float staminaRegen = 10f;
        public float currentStamina;
        public float fatigueTreshhold = 8f;
        public float staminaFatiguerate = 0.5f;
        public float exhuastionPenalty = 0.5f;
        private bool isfatigued = false;
        private bool isExhausted = false;
        private float fatigueTime = 0f;



        [Header("Dodge Values")]
        public float dodgeSpeed = 15f;
        public float dogeTimer = 0.35f;
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

        [Header("Dynamic Camera effects")]
        public float zoomspeed = 0.15f;
        public float fovReduction; // the cameras minum FOV size
        private float defaultFOV; // regular fov
        private float minFov;
        public float pulseFrequency = 1.5f;
        public float pulseIntensity = 0.1f;
   


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

            animator = GetComponent<Animator>(); // H�mta Animator-komponenten h�r


            currentStamina = maxStamnina;

            rigid2d.gravityScale = 0f;
            rigid2d.drag = linearjarDrag;
            rigid2d.angularDrag = angularDrag;

            //Koden nedan calculerar minum v�rdet av camerans "FOV"
            defaultFOV = camera.fieldOfView;
            minFov = defaultFOV - fovReduction;

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
             //s� att jag kommer ih�g att ta bort ifall det inte funkar
            UpdateCameraEffects();
            Animation();
        }


        private void FixedUpdate()
        {
            if (!isDoging)
            {
                ApplyMovement();
                ApplyDynamicTurning();  //s� att jag kommer ih�g att ta bort ifall det inte funkar
            }

        }

        void ApplyMovement()
        {
            float targetSpeed = isRunning ? sprintSpeed : maxWalkSpeed;

            //Jag f�r velositeten
            //Vector2 targVelocity = inputOfMoving * targetSpeed; // byt ut med undre

            Vector2 targVelocity = inputOfMoving * targetSpeed;

            //Accerlation och minskning n�r spelare r�r sig
            if (inputOfMoving.magnitude > 0)
            {
                CurrentVelocity = Vector2.MoveTowards(CurrentVelocity, targVelocity, accelerationofWalk * Time.fixedDeltaTime);
            }
            else
            {
                CurrentVelocity = Vector2.MoveTowards(CurrentVelocity, Vector2.zero, decelerationofWalk * Time.fixedDeltaTime);
            }

          //  CurrentVelocity = Vector2.MoveTowards(CurrentVelocity, Vector2.zero, decelerationofWalk * Time.fixedDeltaTime);
            // velocity = CurrentVelocity; fix senare


            // jag l�gger till den calculerade velociteten till RigidBody2d
            rigid2d.velocity = CurrentVelocity;

        }


    
        void ApplyDynamicTurning()
        {
            if (inputOfMoving.x != 0)
            {
                ForASmoothHorizontalFlip(inputOfMoving.x > 0 ? 1f : -1f, turn);

            }
        }
        //v�nder spelaren horizontelt p� ett lent och fint s�tt
        //targetHorizontalScaleX -1 v�nster och 1 = h�ger 
        //smoothness �r hastigheten som v�ndningen sker i
        void ForASmoothHorizontalFlip(float targetScaleX, float smoothness)
        {
            //"Clampar" skalan av X
            float currentScaleX = transform.localScale.x;
            float newScaleX = Mathf.MoveTowards(currentScaleX, targetScaleX, Time.deltaTime * smoothness);

            newScaleX = InOutEasing(currentScaleX, newScaleX, smoothness);
            //jag till�mpar skala av x fast h�ller kvar vid de andra 
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);

        }

        float InOutEasing(float start, float end, float speed)
        {
            //noramlisering av tiden
            float floatT = Mathf.Clamp01((Time.time * speed) % 1f);
            return Mathf.Lerp(start, end, floatT * floatT * (3f - 2f * floatT)); //kubic funktion InOutEase en mer dynamisk r�relse
        }
           

        
    

        IEnumerator Dodge()
        {

            isDoging = true;
            canDodge = false;


            if (cameraSShake != null)
            {
                cameraSShake.BeginShake(shakeLe, shakeMagnitude);
            }

            //S� man alltid rullar �t h�ger ifall spelaren inte f�rdas i en definerad riktning


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

                //"FatigueTime" under springande
                fatigueTime += Time.deltaTime;


                if (fatigueTime >= fatigueTreshhold && !isfatigued)
                {
                    isfatigued = true;
                }

                if (currentStamina <= 0)
                {
                    currentStamina = 0;
                    // Jag st�nger av spring funktionen
                    canRun = false;
                    isExhausted = true;
                    maxWalkSpeed *= exhuastionPenalty;
                    sprintSpeed *= exhuastionPenalty;

                    //En mechanism f�r att sakta �ka "stamina"
                    if (currentStamina <= 0 && !isfatigued)
                    {
                        StartCoroutine(SecondWind());

                    }
                }

                  IEnumerator SecondWind()
                {
                    //En l�ngsam �kning under tidens g�ng
                    float recoveryTime = 2f;
                    float recoveryAmount =  maxStamnina * 0.3f; // fix later!! morning ahh time
                    float startRecoveryTime = Time.time;

                    while (Time.time < startRecoveryTime + recoveryTime)
                    {
                        currentStamina += (recoveryAmount / recoveryTime) * Time.deltaTime;
                        yield return null;
                    }
                    currentStamina = Mathf.Min(currentStamina, maxStamnina);
                    isExhausted = false;

                }


            }
            else
            {
                //N�r man inte springer
                float regenRate = isfatigued ? staminaFatiguerate : staminaRegen;
                //oj o oj stamina regen
                currentStamina += regenRate * Time.deltaTime;
                if (currentStamina >= maxStamnina)
                {
                    currentStamina = maxStamnina;
                }
                //N�r man f�r springa igen.
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
                fatigueTime = 0f; //�terst�ller timern
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
        void UpdateCameraEffects()
        {
            //FOV blir dynamiskt p�verkat av spelarns hastighet
            float currentSpeed = rigid2d.velocity.magnitude;
            float targetFOV = Mathf.Lerp(minFov, defaultFOV, currentSpeed / sprintSpeed);
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, zoomspeed * Time.deltaTime);
            //En pulserande kamera n�r spelaren �r tr�tt och s�nt.
            if (isfatigued || isExhausted)
            {
                float pulseOffset = Mathf.Sin(Time.time * pulseFrequency) * pulseIntensity;
                // f�r att f�rs�kra att den pulserande cameran kommer ig�gn p� ett bra s�tt 

                Vector3 currentPosition = camera.transform.localPosition;
                camera.transform.localPosition = Vector3.Lerp(currentPosition, new Vector3(currentPosition.x, currentPosition.y + pulseOffset, currentPosition.z), 0.1f);// Kolla s� att vector3 �ven st�mmer f�r 2d
            }
        
        }
        void Animation()
        {
       
        
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                FelixDirection = 1;
                animator.Play("Felix_walk_baksidan");
            }
            else if(!Input.GetKey(KeyCode.W) && FelixDirection == 1)
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
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W  ))
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

