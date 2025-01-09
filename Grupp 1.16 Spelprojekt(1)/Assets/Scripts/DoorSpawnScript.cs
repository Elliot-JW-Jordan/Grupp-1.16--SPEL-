using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorSpawnScript : MonoBehaviour
{
    public int DoorDirection = 0;

    public Vector2 verticalOffset = new Vector2(0f, 6.6f); // Offset for DoorDirection 1 (Up) and 2 (Down)
    public Vector2 horizontalOffset = new Vector2(10.8f, 0f); // Offset for DoorDirection 3 (Right) and 4 (Left)

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
            spawnPosition -= verticalOffset;
            Instantiate(Doors.DoorU, spawnPosition, spawnRotation);
        }
        if (DoorDirection == 2)
        {
            spawnPosition += verticalOffset;
            Instantiate(Doors.DoorD, spawnPosition, spawnRotation);
        }
        if (DoorDirection == 3)
        {
            spawnPosition -= horizontalOffset;
            Instantiate(Doors.DoorR, spawnPosition, spawnRotation);
        }
        if (DoorDirection == 4)
        {
            spawnPosition += horizontalOffset;
            Instantiate(Doors.DoorL, spawnPosition, spawnRotation);
        }

    }

}
