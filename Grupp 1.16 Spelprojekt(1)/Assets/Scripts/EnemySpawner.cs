using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private RoomTemplates Enemys;
    private int rand;
    public bool ShodSpawnEnemy = true;

    private void Update()
    {
        if (ShodSpawnEnemy == true)
        {
            Enemys = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            Invoke("SpawnEnemy", 0.1f);
            ShodSpawnEnemy = false;
        }
        
    }


    void SpawnEnemy()
    {
        rand = Random.Range(0, Enemys.Enemys.Length);
        Instantiate(Enemys.Enemys[rand], transform.position, Enemys.Enemys[rand].transform.rotation);
    }

} 
    
