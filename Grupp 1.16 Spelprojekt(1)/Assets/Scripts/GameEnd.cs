using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveOnDestroy : MonoBehaviour
{
   
    public string testStart;
    public int waitDontLoadNextSceneYet = 5; // F�rdr�jning i sekunder
    public playerHealth playerhealth;

    private bool isSceneLoading = false; // F�r att s�kerst�lla att scenen bara laddas en g�ng

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerhealth = player.GetComponent<playerHealth>();
            
        }
    }

    private void Update()
    {
        if (playerhealth != null && playerhealth.health <= 0 && !isSceneLoading)
        {
            
            
            isSceneLoading = true; // F�rhindrar att Coroutine startas flera g�nger
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(waitDontLoadNextSceneYet); // V�nta i angivet antal sekunder
        SceneManager.LoadScene("testStart"); // Ladda scenen
    }
}
