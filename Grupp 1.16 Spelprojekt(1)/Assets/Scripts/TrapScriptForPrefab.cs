using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScriptForPrefab : MonoBehaviour
{

    public bool playerIsTrapped = false; //indikerar om spelaren har fastnat i en f�lla eller inte 
    PlayerPhysicsWalking thePlayersMovemenWhileTrapped;
    private int requiredPressesOFtHEButtonSPACE = 30; // hur m�nga g�nger spelaren m�sta spamma MELLANSLAG f�r att fly ifr�n f�llan.
    private int traptimmerBeforeInstantDeath = 8; // tid som spelaren f�r p� sig att rymma ifr�n f�llan i sekunder
    private int currentPresses = 0;//hur m�nga g�gner spelaren har tryckt
    private SpriteRenderer playerspriteRenderer;
    [SerializeField] private Color trapped = Color.red; // f�rg n�r spelaren �r f�ngad
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (playerIsTrapped)
        {
            return; //S� spelaren inte kan bli f�ngad g�ng p� g�ng 
        }

        playerIsTrapped = true; // F�llan har nu f�ngat spelaren

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

        StartCoroutine(Traptimer());

    }

   private IEnumerator Traptimer()
    {
        // v�ntar f�r timern att ta slut
        yield return new WaitForSeconds(traptimmerBeforeInstantDeath);


         if (playerIsTrapped)
        {
            Debug.Log("The player failed to escape. Death awaits");
            //d�ds placeholder h�r 
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerIsTrapped)
        {
            Debug.Log("Player escaped the trap");
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

    }





    // Update is called once per frame
    void Update()
    {
        //ifall splaren �r fast i f�llan
        if (playerIsTrapped)
        {
            // mellanslag
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentPresses++;
                Debug.Log($"Player has pressed space : {currentPresses} times");


                if (currentPresses >= requiredPressesOFtHEButtonSPACE)
                {
                    Debug.Log("The player has escaped the trap");
                    ReleasePlayer(gameObject);
                    currentPresses = 0; //�terst�ller
                }
            }



        }





        
    }
}
