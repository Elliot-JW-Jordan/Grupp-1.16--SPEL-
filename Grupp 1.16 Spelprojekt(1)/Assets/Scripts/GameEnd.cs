using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveOnDestroy : MonoBehaviour
{
    public string testStart;

    public playerHealth playerhealth;

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
        if (playerhealth.health <= 0)
        {
            SceneManager.LoadScene("testStart");
        }
    }
}
