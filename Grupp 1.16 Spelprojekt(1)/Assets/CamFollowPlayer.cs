using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform player; 
    public float maxOffset = 6f;

    void Update()
    {
        if (player == null || transform.parent == null)
            return;

        float playerY = player.position.y;

        float parentY = transform.parent.position.y;

        float clampedY = Mathf.Clamp(playerY, parentY - maxOffset, parentY + maxOffset);

        Vector3 cameraPosition = transform.position;
        cameraPosition.y = clampedY; 
        transform.position = cameraPosition;
    }
}
