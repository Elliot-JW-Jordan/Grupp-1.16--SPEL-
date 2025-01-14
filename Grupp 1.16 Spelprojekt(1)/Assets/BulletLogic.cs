using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D othere)
    {
        if (othere.gameObject.CompareTag("Player") || othere.gameObject.CompareTag("Ignore Raycast"))
        {
           Destroy(gameObject); 
        }
        
    }
}
