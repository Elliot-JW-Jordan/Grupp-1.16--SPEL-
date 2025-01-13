using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SettingsButtonController : MonoBehaviour
{
    // This function will be called when the Start button is clicked
    public void OnStartButtonClicked()
    {
        // Load the "Game" scene. Make sure the scene name matches the scene you're switching to
        SceneManager.LoadScene("Settings");
    }
}
