using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int OppeningDirection = 0;
    // 1 for up
    // 2 for right 
    // 3 for down
    // 4 for left

    private void Update()
    {
        if(OppeningDirection == 1)
        {
            //spawn a room with at least a door facing down
        }else if (OppeningDirection == 2)
        {
            //spawn a room with at least a door facing left
        }else if (OppeningDirection == 3)
        {
            //spawn a room with at least a door facing up
        }else if (OppeningDirection == 4)
        {
            //spawn a room with at least a door facing right
        }

    }
}
