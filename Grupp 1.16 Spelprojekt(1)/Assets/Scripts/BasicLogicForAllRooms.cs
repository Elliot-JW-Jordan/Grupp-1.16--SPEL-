using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLogicForAllRooms : MonoBehaviour
{
    private bool PlayerHasEnterd = false;


private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player") && PlayerHasEnterd == false)
        {
            PlayerHasEnterd = true;
            print("player has enterd the room");
        }
    }
}
