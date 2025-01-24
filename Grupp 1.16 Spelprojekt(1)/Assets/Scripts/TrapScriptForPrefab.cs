using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScriptForPrefab : MonoBehaviour
{

    public bool playerIsTrapped = false; //indikerar om spelaren har fastnat i en fälla eller inte 
    PlayerPhysicsWalking thePlayersMovemenWhileTrapped;


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
            ActivateTrap();

        }
    }

     public void ActivateTrap()
    {
        playerIsTrapped = true; // Fällan har nu fångat spelaren


    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        playerIsTrapped = false; //stänger av det fångade tillståndet

    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
