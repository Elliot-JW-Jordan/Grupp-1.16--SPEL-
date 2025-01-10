using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorSpawnScript : MonoBehaviour
{
    private int waitTimeOptemizing = 1;
    public int DoorDirection = 0;

    private RoomTemplates Doors;

    private void Start()
    {
        Doors = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("DoorSpawn", 0.1f);
    }

    void DoorSpawn()
    {
        Vector2 spawnPosition = (Vector2)transform.position;
        Quaternion spawnRotation = Quaternion.identity;

        if (DoorDirection == 1)
        {
            Instantiate(Doors.DoorU, spawnPosition, spawnRotation, transform.parent);
            Destroy(gameObject, waitTimeOptemizing);
        }

        if (DoorDirection == 2)
        {
            Instantiate(Doors.DoorD, spawnPosition, spawnRotation, transform.parent);
            Destroy(gameObject, waitTimeOptemizing);
        }

        if (DoorDirection == 3)
        {
            Instantiate(Doors.DoorR, spawnPosition, spawnRotation, transform.parent);
            Destroy(gameObject, waitTimeOptemizing);
        }

        if (DoorDirection == 4)
        {
            Instantiate(Doors.DoorL, spawnPosition, spawnRotation, transform.parent);
            Destroy(gameObject, waitTimeOptemizing);
        }

    }

}
