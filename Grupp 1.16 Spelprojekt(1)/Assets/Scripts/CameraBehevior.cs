using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private GameObject mainCamera;

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
            mainCamera.transform.position = transform.position + new Vector3(0, 0, -10);
            mainCamera.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && mainCamera != null)
        {
            mainCamera.transform.SetParent(null);
        }
    }
}