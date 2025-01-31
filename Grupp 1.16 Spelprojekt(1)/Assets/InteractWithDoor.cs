using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    [SerializeField]
    private string exitPointTag = "DoorExitPoint";

    [SerializeField]
    private GameObject player;

    private bool isPlayerInTrigger = false;
    private bool DoorActive = false;

    private Transform exitPoint;

    public Vector2 doorDirection;

    private GameObject EnemyContainer;

    public GameObject block;


    private void Start()
    {
        EnemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");

        player = GameObject.FindWithTag("Player");

        foreach (Transform child in transform)
        {
            if (child.CompareTag(exitPointTag))
            {
                exitPoint = child;
                break;
            }
        }

        Invoke("invalidDoor", 0.2f);
    }

    private void Update()
    {
        if (isPlayerInTrigger && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire2")) && DoorActive && EnemyContainer.transform.childCount == 0)
        {
            TeleportPlayer();
        }
        //else if(isPlayerInTrigger == true)
        //{
        //Instantiate(block, transform.position, Quaternion.identity);
        //}
    }
    

    private void invalidDoor()
    {
        RaycastHit2D Hit = Physics2D.Raycast((Vector2)transform.position + doorDirection, doorDirection);
        if (Hit)
        {
            if(Hit.collider.tag == "Door")
            {
                DoorActive = true;
            }

            if (Hit.collider.tag != "Door")
            {
                DoorActive = false;
            }
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
