using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToBossRoom : MonoBehaviour
{
    private bool PlayerIsToutching = false;
    public string bossRoomSceneName = "Boss Battle";

    private void Update()
    {
        if (PlayerIsToutching == true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(bossRoomSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIsToutching=true;
        }
    }
}
