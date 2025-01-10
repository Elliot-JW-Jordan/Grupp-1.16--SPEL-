using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogicForOppening : MonoBehaviour
{
    private BasicLogicForAllRooms parentScript;
    private bool ShodIOppen = false;

    void Start()
    {
        parentScript = transform.parent.GetComponent<BasicLogicForAllRooms>();

        if (parentScript == null)
        {
            Debug.LogError("Parent script not found!");
        }
    }

    void Update()
    {
        //if (parentScript != null && BasicLogicForAllRooms.PlayerHasEnterd == true && ShodIOppen == true)
        {
            // when enemy has been added make it this only happed when emenys in thie room =
            //Destroy(gameObject, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("closedroom"))
        {
            ShodIOppen = true;
        }
    }
}
