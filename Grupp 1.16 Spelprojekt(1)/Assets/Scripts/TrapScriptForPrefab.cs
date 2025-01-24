using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScriptForPrefab : MonoBehaviour
{

    public bool playerIsTrapped = false; //indikerar om spelaren har fastnat i en f�lla eller inte 
    PlayerPhysicsWalking thePlayersMovemenWhileTrapped;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // kollar ifall det �r ett objekt med spelar "Player" tag som kolliderar med f�llan.
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("The player collided with the trap!! Calling TrapScript...");
            //kallar f�llans metod
            ActivateTrap();

        }
    }

     public void ActivateTrap()
    {
        playerIsTrapped = true; // F�llan har nu f�ngat spelaren


    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        playerIsTrapped = false; //st�nger av det f�ngade tillst�ndet

    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
