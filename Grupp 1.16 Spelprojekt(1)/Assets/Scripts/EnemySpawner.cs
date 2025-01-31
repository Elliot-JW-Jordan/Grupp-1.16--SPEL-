using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private RoomTemplates Enemys;
    private int rand;
    public bool ShodSpawnEnemy = true;
    private GameObject EnemyContainer;

    public BasicLogicForAllRooms roomLogic;
    private void Start()
    {
        EnemyContainer = GameObject.FindGameObjectWithTag("EnemyContainer");

        // Get the RoomTemplates component
        Enemys = GameObject.FindGameObjectWithTag("RoomSpawner").GetComponent<RoomTemplates>();
        
        // Get the parent script
        roomLogic = GetComponentInParent<BasicLogicForAllRooms>();
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
        Instantiate(Enemys.Enemys[rand], transform.position, Enemys.Enemys[rand].transform.rotation, EnemyContainer.transform);
    }

} 
    
