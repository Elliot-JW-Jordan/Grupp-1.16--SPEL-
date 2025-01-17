using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveOnDestroy : MonoBehaviour
{
   
    public string testStart;
    public int waitDontLoadNextSceneYet = 5; // Fördröjning i sekunder
    public playerHealth playerhealth;

    private bool isSceneLoading = false; // För att säkerställa att scenen bara laddas en gång

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
            
            
            isSceneLoading = true; // Förhindrar att Coroutine startas flera gånger
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(waitDontLoadNextSceneYet); // Vänta i angivet antal sekunder
        SceneManager.LoadScene("testStart"); // Ladda scenen
    }
}
