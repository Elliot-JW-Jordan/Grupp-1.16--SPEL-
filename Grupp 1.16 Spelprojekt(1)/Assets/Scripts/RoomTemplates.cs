using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] BottomRooms;
    public GameObject[] TopRooms;
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;

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

    private int ShopRoomNumber = 0;
    public GameObject Shop;
    private int maxShops;
    private bool spawnedAllShops = false;

    private void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            return;
        }

        if (!spawnedBoss)
            SpawnBoss();

        if (!spawnedAllShops)
            SpawnShops();
    }

    private void SpawnBoss()
    {
        if (rooms.Count == 0) return;

        Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        spawnedBoss = true;
    }

    private void SpawnShops()
    {
        if (rooms.Count < 5) return;

        maxShops = Mathf.Max(1, rooms.Count / 7); // Ensure at least 1 shop.
        HashSet<int> usedIndices = new HashSet<int>();

        for (int i = 0; i < maxShops; i++)
        {
            int shopRoomIndex;
            do
            {
                shopRoomIndex = Random.Range(2, rooms.Count);
            } while (usedIndices.Contains(shopRoomIndex));

            usedIndices.Add(shopRoomIndex);
            Instantiate(Shop, rooms[shopRoomIndex].transform.position, Quaternion.identity);
        }

        spawnedAllShops = true;
    }
}
