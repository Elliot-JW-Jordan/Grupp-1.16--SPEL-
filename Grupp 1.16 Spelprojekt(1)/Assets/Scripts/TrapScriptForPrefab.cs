using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrapScriptForPrefab : MonoBehaviour
{
    public CameraSShake camera; //reffererar till CameraSShake scriptet
    public bool playerIsTrapped = false; //indikerar om spelaren har fastnat i en f�lla eller inte 
    PlayerPhysicsWalking thePlayersMovemenWhileTrapped;
    private int requiredPressesOFtHEButtonSPACE = 30; // hur m�nga g�nger spelaren m�sta spamma MELLANSLAG f�r att fly ifr�n f�llan.
    private int traptimmerBeforeInstantDeath = 8; // tid som spelaren f�r p� sig att rymma ifr�n f�llan i sekunder
    private int currentPresses = 0;//hur m�nga g�gner spelaren har tryckt
    private SpriteRenderer playerspriteRenderer;
   
    
    [SerializeField] private Color trapped = Color.red; // f�rg n�r spelaren �r f�ngad
    [SerializeField] private Color defualtColor = Color.white;
    [SerializeField]
    private float trapCooldown = 5f; //Tiden i sekunder som det tar innan f�llan kan om aktiveras
    private bool trapIsActive = true; // bool f�r tillst�ndet av f�llan, activerade och de actriverad

  
    [SerializeField]  private Slider progressOFEscape; // visar infomationen i fomr uta ui
    [SerializeField] private ParticleSystem smokeEffect; //R�k 


    private Renderer trapRenderer;
    void Start()
    {
        trapRenderer = GetComponent<Renderer>();
        trapRenderer.enabled = false; //g�mmer f�llan i b�rjan

        // S� att f�llan b�rjar i korrekt f�rg
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

        // kollar ifall det �r ett objekt med spelar "Player" tag som kolliderar med f�llan.
        if (collision.gameObject.CompareTag("Player") && !playerIsTrapped)
        {

            Debug.Log("The player collided with the trap!! Calling TrapScript...");
            //kallar f�llans metod
            ActivateTrap(collision.gameObject);

        }
    }

     public void ActivateTrap(GameObject player)
    {
        if (playerIsTrapped || !trapIsActive)
        {
            return; //S� spelaren inte kan bli f�ngad g�ng p� g�ng 
        }

        playerIsTrapped = true; // F�llan har nu f�ngat spelaren
        trapIsActive = false; // f�llans tillst�nd


        thePlayersMovemenWhileTrapped = player.GetComponent<PlayerPhysicsWalking>();
        if (thePlayersMovemenWhileTrapped != null)
        {
            //st�nger av spelarens f�rm�ga att r�rasig n�r splearen sittier i f�llan
            thePlayersMovemenWhileTrapped.enabled = false;
        }

        //byter f�rg
        playerspriteRenderer = player.GetComponent<SpriteRenderer>();
        if (playerspriteRenderer !=null)
        {
            playerspriteRenderer.color = trapped;
        }
        
        // nedan aktiverar r�ken runt f�llan
        if (smokeEffect != null)
        {
            smokeEffect.transform.position = transform.position; // ser till att r�k effect pwanwr p� r�tt st�lle
            smokeEffect.Play(); // n�r spelaren aktiverar f�lla s� b�rjar r�k effekten spela
        }
        if ( trapRenderer != null)
        {
            trapRenderer.enabled = true;
        }



        // bar 
        if(progressOFEscape != null)
        {
            progressOFEscape.gameObject.SetActive(true); // g�r den synlig
            progressOFEscape.value = 0;

        }

        StartCoroutine(Traptimer());

    }

   private IEnumerator Traptimer()
    {
        // v�ntar f�r timern att ta slut
        yield return new WaitForSeconds(traptimmerBeforeInstantDeath);


         if (playerIsTrapped)
        {

            Debug.Log("Death awaits");
            playerHealth playerHP = thePlayersMovemenWhileTrapped.GetComponent<playerHealth>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(3); // Skadar spelaren n�r spelarn �r f�ngad i f�llan f�r l�nge
            }
            ReleasePlayer(thePlayersMovemenWhileTrapped.gameObject);
            //d�ds placeholder h�r 
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

        playerIsTrapped = false; //st�nger av det f�ngade tillst�ndet

        //s�tt p� spelarens r�relse igen
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
            progressOFEscape.gameObject.SetActive(false); // g�r den synlig 
           

        }

        StartCoroutine(TimerReset());



    }

    private IEnumerator TimerReset() 
    {
        Debug.Log("The cooldown of the trap has started");
        yield return new WaitForSeconds(trapCooldown);

        trapIsActive = true;
        Debug.Log("The trap can be triggered again");

        ResetTrap(); // �terst�ller f�llan
    }


    private void ResetTrap()
    {
        playerIsTrapped = false;
        trapIsActive = true;

        // �terst�ller f�rgen
        if (playerspriteRenderer != null)
        {
            playerspriteRenderer.color = Color.white;

        }

        Debug.Log("tHE TRAP has been reset");
        // �terst�ller ptillst�nd
       // StartCoroutine(TimerReset());

    }



    // Update is called once per frame
    void Update()
    {
        //ifall splaren �r fast i f�llan
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
                camera.BeginShake(0.3f, 5f);// N�r spelaren rymmer
                ReleasePlayer(thePlayersMovemenWhileTrapped.gameObject);
                currentPresses = 0; //�terst�ller
            }
        }

    }
}
