using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int OppeningDirection;
    // 1 for bottom
    // 2 for top
    // 3 for left
    // 4 for right

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    public float waitTimeOptemizing = 4f;

    private void Start()
    {
        Destroy(gameObject, waitTimeOptemizing);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (spawned == false )
        {
            if(OppeningDirection == 1)       
            {
                //spawn a room with at least a door facing bottom
                rand = Random.Range(0, templates.BottomRooms.Length);
                Instantiate(templates.BottomRooms[rand], transform.position, templates.BottomRooms[rand].transform.rotation);
            }
            else if (OppeningDirection == 2)       
            {
                //spawn a room with at least a door facing top
                rand = Random.Range(0, templates.TopRooms.Length);
                Instantiate(templates.TopRooms[rand], transform.position, templates.TopRooms[rand].transform.rotation);
            }
            else if (OppeningDirection == 3)       
            {
                //spawn a room with at least a door facing left
                rand = Random.Range(0, templates.LeftRooms.Length);
                Instantiate(templates.LeftRooms[rand], transform.position, templates.LeftRooms[rand].transform.rotation);
            }
            else if (OppeningDirection == 4)       
            {
                //spawn a room with at least a door facing right
                rand = Random.Range(0, templates.RightRooms.Length);
                Instantiate(templates.RightRooms[rand], transform.position, templates.RightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("SpawnPoint"))
        {
            if(Other.GetComponent<SpawnPoint>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

}
