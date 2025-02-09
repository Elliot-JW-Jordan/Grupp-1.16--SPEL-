using System.Collections;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private GameObject mainCamera;
    private bool isPlayerInTrigger = false;
    private Collider2D playerCollider;
    private Collider2D roomCollider;
    private float detachDelay = 0.1f; // Small delay to prevent flickering
    private float parentDelay = 0.1f; // 0.5 second delay before setting the camera's parent

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        roomCollider = GetComponent<Collider2D>();

        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera not found. Ensure it is tagged 'MainCamera'.");
        }
        else
        {
            Debug.Log("Main Camera successfully found: " + mainCamera.name);
        }

        if (roomCollider == null)
        {
            Debug.LogWarning("No Collider2D found on this object! The camera trigger may not work.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && mainCamera != null)
        {
            isPlayerInTrigger = true;
            playerCollider = other;

            mainCamera.transform.position = transform.position + new Vector3(0, 0, -10);

            // Start the coroutine to set the parent after a delay
            StartCoroutine(SetCameraParentWithDelay());

            Debug.Log("Started coroutine to set camera parent with delay.");
        }
    }

    private IEnumerator SetCameraParentWithDelay()
    {
        yield return new WaitForSeconds(parentDelay);

        if (isPlayerInTrigger && mainCamera != null)  // Ensure player is still in the trigger
        {
            mainCamera.transform.SetParent(transform);
            Debug.Log("Camera parent set to: " + mainCamera.transform.parent);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && mainCamera != null)
        {
            isPlayerInTrigger = false;
            playerCollider = null;

            DetachCamera();
        }
    }

    private void DetachCamera()
    {

        if (mainCamera != null && mainCamera.transform.parent != null)
        {
            if (mainCamera.transform.parent.gameObject.activeInHierarchy) // SER TILL SÅ ATT PARENTEN ÄR AKTIVERAD
            {
                Debug.Log("Detaching camera...");
                mainCamera.transform.SetParent(null);
            } else
            {
                Debug.Log("Can not detach camera, the parent is being deactevated");
            }
           
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && playerCollider != null && roomCollider != null)
        {
            if (!playerCollider.bounds.Intersects(roomCollider.bounds))
            {
                isPlayerInTrigger = false;
                playerCollider = null;

                if (mainCamera != null)
                {
                    Debug.Log("Camera is being detached in Update!");
                    Invoke(nameof(DetachCamera), detachDelay);  // Small delay to prevent flickering
                }
            }
        }
    }
}
