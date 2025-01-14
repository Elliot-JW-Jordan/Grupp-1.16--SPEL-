using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    [SerializeField]
    private string exitPointTag = "DoorExitPoint";

    [SerializeField]
    private GameObject player;

    private bool isPlayerInTrigger = false;

    private Transform exitPoint;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        foreach (Transform child in transform)
        {
            if (child.CompareTag(exitPointTag))
            {
                exitPoint = child;
                break;
            }
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger == true && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        if (player != null && exitPoint != null)
        {
            player.transform.position = exitPoint.position;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInTrigger = false;
        }
    }

}
