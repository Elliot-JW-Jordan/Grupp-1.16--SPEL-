using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrapScriptForPrefab : MonoBehaviour
{
    public CameraSShake camera; //reffererar till CameraSShake scriptet
    public bool playerIsTrapped = false; //indikerar om spelaren har fastnat i en fälla eller inte 
    PlayerPhysicsWalking thePlayersMovemenWhileTrapped;
    private int requiredPressesOFtHEButtonSPACE = 30; // hur många gånger spelaren måsta spamma MELLANSLAG för att fly ifrån fällan.
    private int traptimmerBeforeInstantDeath = 8; // tid som spelaren får på sig att rymma ifrån fällan i sekunder
    private int currentPresses = 0;//hur många gågner spelaren har tryckt
    private SpriteRenderer playerspriteRenderer;
   
    
    [SerializeField] private Color trapped = Color.red; // färg när spelaren är fångad
    [SerializeField] private Color defualtColor = Color.white;
    [SerializeField]
    private float trapCooldown = 5f; //Tiden i sekunder som det tar innan fällan kan om aktiveras
    private bool trapIsActive = true; // bool för tillståndet av fällan, activerade och de actriverad

  
    [SerializeField]  private Slider progressOFEscape; // visar infomationen i fomr uta ui
    [SerializeField] private ParticleSystem smokeEffect; //Rök 


    private Renderer trapRenderer;
    void Start()
    {
        trapRenderer = GetComponent<Renderer>();
        trapRenderer.enabled = false; //gömmer fällan i början

        // Så att fällan börjar i korrekt förg
        GetComponent<SpriteRenderer>().color = defualtColor;


        if (progressOFEscape == null)
        {
            GameObject Trapslider1 = GameObject.FindWithTag("Trapslider");
            if (Trapslider1 != null)
            {
                progressOFEscape = Trapslider1.GetComponent<Slider>();
            } else
            {
                Debug.LogWarning("The slider object Trapslider is not found inn the scene");

            }

        }
        if (progressOFEscape !=null)
        {


            progressOFEscape.gameObject.SetActive(false);
            progressOFEscape.value = 0;
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // kollar ifall det är ett objekt med spelar "Player" tag som kolliderar med fällan.
        if (collision.gameObject.CompareTag("Player") && !playerIsTrapped)
        {

            Debug.Log("The player collided with the trap!! Calling TrapScript...");
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



        // bar 
        if(progressOFEscape != null)
        {
            progressOFEscape.gameObject.SetActive(true); // gör den synlig
            progressOFEscape.value = 0;

        }

        StartCoroutine(Traptimer());

    }

   private IEnumerator Traptimer()
    {
        // väntar för timern att ta slut
        yield return new WaitForSeconds(traptimmerBeforeInstantDeath);


         if (playerIsTrapped)
        {

            Debug.Log("Death awaits");
            playerHealth playerHP = thePlayersMovemenWhileTrapped.GetComponent<playerHealth>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(3); // Skadar spelaren när spelarn är fångad i fällan för länge
            }
            ReleasePlayer(thePlayersMovemenWhileTrapped.gameObject);
            //döds placeholder här 
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerIsTrapped)
        {
            Debug.Log("Player escaped the trap");
            camera.BeginShake(0.2f, 3f); // Activerar skakningen
            ReleasePlayer(collision.gameObject);
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

        StartCoroutine(TimerReset());



    }

    private IEnumerator TimerReset() 
    {
        Debug.Log("The cooldown of the trap has started");
        yield return new WaitForSeconds(trapCooldown);

        trapIsActive = true;
        Debug.Log("The trap can be triggered again");

        ResetTrap(); // återställer fällan
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

        Debug.Log("tHE TRAP has been reset");
        // återställer ptillstånd
       // StartCoroutine(TimerReset());

    }



    // Update is called once per frame
    void Update()
    {
        //ifall splaren är fast i fällan
        if (playerIsTrapped)
        {
            TakeCareOFInput(); 
           



        }





        
    }

  private void  TakeCareOFInput()
    {
        // mellanslag
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
            Debug.Log($"Player has pressed space : {currentPresses} times");

            if (progressOFEscape != null)
            {

                progressOFEscape.value = currentPresses;

            }
            camera.BeginShake(0.1f, 0.2f); //vid klicknind utav mellanslag

            if (currentPresses >= requiredPressesOFtHEButtonSPACE)
            {
                Debug.Log("The player has escaped the trap");
                camera.BeginShake(0.3f, 5f);// När spelaren rymmer
                ReleasePlayer(thePlayersMovemenWhileTrapped.gameObject);
                currentPresses = 0; //återställer
            }
        }

    }
}
