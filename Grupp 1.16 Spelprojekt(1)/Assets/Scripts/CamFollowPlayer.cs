using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    private CameraSShake cameraSShake;
    public GameObject player; 
    public float maxOffset = 6f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void LateUpdate() //
    {
        if (player == null || transform.parent == null || (cameraSShake != null && cameraSShake.isshaking))
            return;
        

        float playerY = player.transform.position.y;

        float parentY = transform.parent.position.y;

        float clampedY = Mathf.Clamp(playerY, parentY - maxOffset, parentY + maxOffset);

        Vector3 cameraPosition = transform.position;
        cameraPosition.y = clampedY; 
        transform.position = cameraPosition;
    }
   
}
