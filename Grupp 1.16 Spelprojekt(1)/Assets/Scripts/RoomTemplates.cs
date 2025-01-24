using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [Header("Main Rooms")]
    public GameObject[] BottomRooms;
    public GameObject[] TopRooms;
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;

    [Header("OneDoorRooms")]
    public GameObject[] EndBottomRooms;
    public GameObject[] EndTopRooms;
    public GameObject[] EndLeftRooms;
    public GameObject[] EndRightRooms;

    [Header("Extra")]
    public GameObject closedRooms;
    public GameObject DoorU;
    public GameObject DoorD;
    public GameObject DoorR;
    public GameObject DoorL;

    public List<GameObject> rooms;

    public GameObject[] Enemys;

    public float waitTime;
    private bool spawnedBoss = false;
    public GameObject boss;

    private void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            return;
        }

        if (!spawnedBoss)
            SpawnBoss();
    }

    private void SpawnBoss()
    {
        if (rooms.Count == 0) return;

        Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        spawnedBoss = true;
    }

}
