using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Diagnostics;


public class TrapScriptForPrefab : MonoBehaviour
{
    public CameraSShake camera; //reffererar till CameraSShake scriptet
    public bool playerIsTrapped = false; //indikerar om spelaren har fastnat i en fälla eller inte 
    PlayerPhysicsWalking thePlayersMovemenWhileTrapped;
    private int requiredPressesOFtHEButtonSPACE = 30; // hur många gånger spelaren måsta spamma MELLANSLAG för att fly ifrån fällan.
    private int traptimmerBeforeInstantDeath = 8; // tid som spelaren får på sig att rymma ifrån fällan i sekunder
    private int currentPresses = 0;//hur många gågner spelaren har tryckt
    private bool isNearAtrap = false; // kollar ifall spelaren är nära fällan
    private Image theFillOfSlider;
    private SpriteRenderer playerspriteRenderer;
   
    
    [SerializeField] private Color trapped = Color.red; // färg när spelaren är fångad
    [SerializeField] private Color defualtColor = Color.white;
    [SerializeField]

    private float trapCooldown = 5f; //Tiden i sekunder som det tar innan fällan kan om aktiveras
    private bool trapIsActive = true; // bool för tillståndet av fällan, activerade och de actriverad

  
    [SerializeField]  private Slider progressOFEscape; // visar infomationen i fomr uta ui
    [SerializeField] private ParticleSystem smokeEffect; //Rök 
    [SerializeField] private TMP_Text instructionalText; // refferance till en ui text
    [SerializeField] private TMP_Text disarmInstructionText;
    private Renderer trapRenderer;

    [Header("collider")]
    public CircleCollider2D disarmCollider; 
    void Start()
    {
        trapRenderer = GetComponent<Renderer>();
        trapRenderer.enabled = false; //gömmer fällan i början

        // Så att fällan börjar i korrekt förg
        GetComponent<SpriteRenderer>().color = defualtColor;


        if (progressOFEscape == null)
        {
            GameObject Trapslider1 = GameObject.Find("Trapslider");
            if (Trapslider1 != null)
            {
                progressOFEscape = Trapslider1.GetComponent<Slider>();
                if (progressOFEscape == null)
                {
                    UnityEngine.Debug.LogError("Slider component not found on the trapslidere");

                }

            }  else
            {
                UnityEngine.Debug.LogWarning("The slider object trapslider is not found in the scene");
            }

        }
        if (progressOFEscape != null)
        {
            progressOFEscape.gameObject.SetActive(false); // må vara felaktikt
            progressOFEscape.value = 0;

            theFillOfSlider = progressOFEscape.fillRect.GetComponent<Image>(); // hämtar bilden av 'Fill'
            if (theFillOfSlider == null)
            {
                UnityEngine.Debug.LogError("nO Image component found on the fill object of the sliderh");
            }
        }
        if (instructionalText != null)
        {
            instructionalText.gameObject.SetActive(false);  // gömmer texten 
        } 
        if (disarmInstructionText != null)
        {
            disarmInstructionText.gameObject.SetActive(false);
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // kollar ifall det är ett objekt med spelar "Player" tag som kolliderar med fällan.
        if (collision.gameObject.CompareTag("Player") && !playerIsTrapped)
        {

            UnityEngine.Debug.Log("The player collided with the trap!! Calling TrapScript...");
            //kallar fällans metod
            ActivateTrap(collision.gameObject);

        }
    }

     public void ActivateTrap(GameObject player)
    {
        if (playerIsTrapped || !trapIsActive)
        {
            return; //Så spelaren inte kan bli fångad gång på gång 
        }

        playerIsTrapped = true; // Fällan har nu fångat spelaren
        trapIsActive = false; // fällans tillstånd


        thePlayersMovemenWhileTrapped = player.GetComponent<PlayerPhysicsWalking>();
        if (thePlayersMovemenWhileTrapped != null)
        {
            //stänger av spelarens förmåga att rörasig när splearen sittier i fällan
            thePlayersMovemenWhileTrapped.enabled = false;
        }

        //byter färg
        playerspriteRenderer = player.GetComponent<SpriteRenderer>();
        if (playerspriteRenderer !=null)
        {
            playerspriteRenderer.color = trapped;
        }
        
        // nedan aktiverar röken runt fällan
        if (smokeEffect != null)
        {
            smokeEffect.transform.position = transform.position; // ser till att rök effect pwanwr på rätt ställe
            smokeEffect.Play(); // när spelaren aktiverar fälla så börjar rök effekten spela
        }
        if ( trapRenderer != null)
        {
            trapRenderer.enabled = true;
        }
        StartCoroutine(DangerFlash());



        // bar 
        if(progressOFEscape != null)
        {
            progressOFEscape.gameObject.SetActive(true); // gör den synlig
            progressOFEscape.value = 0;

        }
        if (instructionalText  != null)
        {
            instructionalText.gameObject.SetActive(true); // gör texten synlig igen
            

        }
        StartCoroutine(Traptimer());

    }

   private IEnumerator Traptimer()
    {
        // väntar för timern att ta slut
        yield return new WaitForSeconds(traptimmerBeforeInstantDeath);


         if (playerIsTrapped)
        {

            UnityEngine.Debug.Log("Death awaits");


            if (thePlayersMovemenWhileTrapped != null) // kollar ifall  spelarens referens är riktig
            {
                playerHealth playerHP = thePlayersMovemenWhileTrapped.GetComponent<playerHealth>();


                if (playerHP != null)
                {
                    playerHP.TakeDamage(3); // Skadar spelaren när spelarn är fångad i fällan för länge
                } 
                else
                {
                    UnityEngine.Debug.LogError("PlayerHealth component not found on the playerobject");
                }
                ReleasePlayer(thePlayersMovemenWhileTrapped.gameObject);
            } else
            {
                UnityEngine.Debug.LogError("thePlayersMovementWhileTrapped is null, Make sure the player physic walking is assigned");
            }
         
  
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Undersöker först, och bestämer vilket utav de två det 'r 'Circle  
        if (collision == disarmCollider && collision.CompareTag("Player") && !playerIsTrapped)
        {
            isNearAtrap = true; // visa att spelaren är nära
            if (disarmInstructionText != null && trapIsActive)
            {
                disarmInstructionText.text = "Press 'E' to disarm trap";
                disarmInstructionText.gameObject.SetActive(true);
            }

        }
        
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == disarmCollider)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                isNearAtrap = false; //mspelaren är int elängre nära fällan
                if (disarmInstructionText != null)
                {
                    disarmInstructionText.gameObject.SetActive(false);
                }


            }
          //  if (playerIsTrapped) {

               
                camera.BeginShake(0.2f, 3f); // Activerar skakningen
              //  ReleasePlayer(collision.gameObject);
              //  UnityEngine.Debug.Log("Player escaped the trap");
           // }

           
        }
       

    }
   private void ReleasePlayer(GameObject player)
    {

        playerIsTrapped = false; //stänger av det fångade tillståndet

        //sätt på spelarens rörelse igen
        if (thePlayersMovemenWhileTrapped != null)
        {
            thePlayersMovemenWhileTrapped.enabled = true;
        }

        if (playerspriteRenderer != null)
        {
            playerspriteRenderer.color = Color.white;
        }

        if (progressOFEscape != null)
        {
            progressOFEscape.value = 0;
            progressOFEscape.gameObject.SetActive(false); // gör den synlig 
           

        }

        if (instructionalText != null)
        {
            instructionalText.gameObject.SetActive(false); //stänger av texten efter splearen har rymt 
        }

        currentPresses = 0;

        StartCoroutine(TimerReset());



    }

    private IEnumerator TimerReset() 
    {
        UnityEngine.Debug.Log("The cooldown of the trap has started");
        yield return new WaitForSeconds(trapCooldown);

        trapIsActive = true;
        UnityEngine.Debug.Log("The trap can be triggered again");

        ResetTrap(); // återställer fällan
    }

    private IEnumerator DangerFlash() // 
    {
        float durationOfFLash = 0.1f; // hur snabbt det kommer blinka 
        Color orginColor = playerspriteRenderer.color;
       Color colorOfFlash = Color.yellow;

        while (playerIsTrapped)
        {
            playerspriteRenderer.color = colorOfFlash;
            yield return new WaitForSeconds(durationOfFLash);
            playerspriteRenderer.color = orginColor;
            yield return new WaitForSeconds(durationOfFLash);
        }


    }
    private IEnumerator DisarmTrap()
    {
        trapIsActive = false; //stäng av fällan
        UnityEngine.Debug.Log("Trap has  been disarmed!");

        if (disarmInstructionText != null)
        {
            disarmInstructionText.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(15f); // väntar i 15 sekunder


        trapIsActive = true; // sätt på fällan igen
        UnityEngine.Debug.Log("The Trap has been reactivated");
    }

    private void ResetTrap()
    {
        playerIsTrapped = false;
        trapIsActive = true;

        // återställer färgen
        if (playerspriteRenderer != null)
        {
            playerspriteRenderer.color = Color.white;

        }

        UnityEngine.Debug.Log("tHE TRAP has been reset");
        // återställer ptillstånd
       // StartCoroutine(TimerReset());

    }



    // Update is called once per frame
    void Update()
    {
        //ifall spelaren är nära fällan, inte fångad och håller ned 'E', stänger spealren av fällan
        if (isNearAtrap && !playerIsTrapped && Input.GetKey(KeyCode.E) && trapIsActive)
        {
            StartCoroutine(DisarmTrap());
        }


        //ifall splaren är fast i fällan
        if (playerIsTrapped)
        {
            TakeCareOFInput(); 
        }
        
    }

  private void  TakeCareOFInput()
    {
        float progress = 0f;
        // om mellanslag trycks
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
            UnityEngine.Debug.Log($"Player has pressed space : {currentPresses} times");

            if (progressOFEscape != null)
            {
                // normaliserar värdet
                progress = (float)currentPresses / requiredPressesOFtHEButtonSPACE;
                progressOFEscape.value = progress;
            }
            // ändrar sakta färgen av  'Fill' Dynamiskt ;
            if (theFillOfSlider != null)
            {
                theFillOfSlider.color = Color.Lerp(Color.green, Color.red, progress);
            }
            if (instructionalText != null) // hur texten updaterar sig beroende på hur nära spelaren är att befria sig själv.
            {
                if (progress < 0.3f)
                {
                    instructionalText.text = "Keep pressing 'SPACE'!!";
                } else if (progress < 0.6f)
                {
                    instructionalText.text = "Almost, Press FASTER!!";

                } else if (progress < 1.0f)
                {
                    instructionalText.text = "HURRY UP!!! ESCAPE!!!";
                } else
                {
                    instructionalText.text = "Press 'SPACE' to escape";
                }
            }
            camera.BeginShake(0.1f, 0.2f); //vid klicknind utav mellanslag

            if (currentPresses >= requiredPressesOFtHEButtonSPACE)
            {
                UnityEngine.Debug.Log("The player has escaped the trap");
                camera.BeginShake(0.3f, 5f);// När spelaren rymmer
                ReleasePlayer(thePlayersMovemenWhileTrapped.gameObject);
                currentPresses = 0; //återställer
            }
        }

    }
}
