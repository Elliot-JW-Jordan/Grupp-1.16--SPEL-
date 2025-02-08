using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private GameObject mainCamera;

    private bool isPlayerInTrigger = false;
    private Collider2D playerCollider;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera not found. Ensure it is tagged 'MainCamera'.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && mainCamera != null)
        {
            isPlayerInTrigger = true; 
            playerCollider = other; 

            mainCamera.transform.position = transform.position + new Vector3(0, 0, -10);
            mainCamera.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && mainCamera != null)
        {
            isPlayerInTrigger = false;
            playerCollider = null;

            Invoke(nameof(DetachCamera), 0.1f);

           // mainCamera.transform.SetParent(null);
        }
    }
    private void DetachCamera()
    {
        
        mainCamera.transform.SetParent(null);
    }

    private void Update()
    {
        if (isPlayerInTrigger && playerCollider != null)
        {
            if (!playerCollider.bounds.Intersects(GetComponent<Collider2D>().bounds))
            {
                isPlayerInTrigger = false;
                playerCollider = null;


              
                
                if (mainCamera != null)

                {
                    
                    mainCamera.transform.SetParent(null);
                }



            }
        }
       
    }
}