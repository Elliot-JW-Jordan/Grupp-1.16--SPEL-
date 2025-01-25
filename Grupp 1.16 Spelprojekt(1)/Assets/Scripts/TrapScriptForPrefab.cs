using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScriptForPrefab : MonoBehaviour
{

    public bool playerIsTrapped = false; //indikerar om spelaren har fastnat i en fälla eller inte 
    PlayerPhysicsWalking thePlayersMovemenWhileTrapped;
    private int requiredPressesOFtHEButtonSPACE = 30; // hur många gånger spelaren måsta spamma MELLANSLAG för att fly ifrån fällan.
    private int traptimmerBeforeInstantDeath = 8; // tid som spelaren får på sig att rymma ifrån fällan i sekunder

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // kollar ifall det är ett objekt med spelar "Player" tag som kolliderar med fällan.
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("The player collided with the trap!! Calling TrapScript...");
            //kallar fällans metod
            ActivateTrap(collision.gameObject);

        }
    }

     public void ActivateTrap(GameObject player)
    {
        if (playerIsTrapped)
        {
            return; //Så spelaren inte kan bli fångad gång på gång 
        }

        playerIsTrapped = true; // Fällan har nu fångat spelaren

        thePlayersMovemenWhileTrapped = player.GetComponent<PlayerPhysicsWalking>();
        if (thePlayersMovemenWhileTrapped != null)
        {
            //stänger av spelarens förmåga att rörasig när splearen sittier i fällan
            thePlayersMovemenWhileTrapped.enabled = false;
        }
        StartCoroutine(Traptimer());

    }

   private IEnumerator Traptimer()
    {
        // väntar för timern att ta slut
        yield return new WaitForSeconds(traptimmerBeforeInstantDeath);


         if (playerIsTrapped)
        {
            Debug.Log("The player failed to escape. Death awaits");
            //döds placeholder här 
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

        playerIsTrapped = false; //stänger av det fångade tillståndet

        //sätt på spelarens rörelse igen
        if (thePlayersMovemenWhileTrapped != null)
        {
            thePlayersMovemenWhileTrapped.enabled = true;
        }

    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
