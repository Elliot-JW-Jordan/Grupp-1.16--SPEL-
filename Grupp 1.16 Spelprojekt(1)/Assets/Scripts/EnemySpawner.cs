using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private RoomTemplates Enemys;
    private int rand;
    public bool ShodSpawnEnemy = true;

    public BasicLogicForAllRooms roomLogic;
    private void Start()
    {
        // Get the RoomTemplates component
        Enemys = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        if (Enemys == null)
        {
            Debug.LogError("RoomTemplates component not found on object with tag 'Rooms'!");
        }

        // Get the parent script
        roomLogic = GetComponentInParent<BasicLogicForAllRooms>();
        if (roomLogic == null)
        {
            Debug.LogError("BasicLogicForAllRooms script not found on the parent!");
        }
    }

    private void Update()
    {
        if (ShodSpawnEnemy == true)
        {
            if (ShodSpawnEnemy && roomLogic != null && roomLogic.playerHasEntered)
            {
                // Spawn the enemy after a short delay
                Invoke("SpawnEnemy", 0.1f);
                ShodSpawnEnemy = false;
            }
        }
    }


    void SpawnEnemy()
    {
        if (Enemys == null) return;

        // Randomly select an enemy prefab
        rand = Random.Range(0, Enemys.Enemys.Length);
        Instantiate(Enemys.Enemys[rand], transform.position, Enemys.Enemys[rand].transform.rotation);
    }

} 
    
