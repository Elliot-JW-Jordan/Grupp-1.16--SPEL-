using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLogicForAllRooms : MonoBehaviour
{
    public bool playerHasEntered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerHasEntered)
        {
            playerHasEntered = true;
            print("Player has entered the room");
        }
    }
}