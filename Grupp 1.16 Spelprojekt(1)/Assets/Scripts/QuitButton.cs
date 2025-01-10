using UnityEngine;

public class QuitButtonController : MonoBehaviour
{
    // This function will be called when the Quit button is clicked
    public void OnQuitButtonClicked()
    {
        // If we are running the game in the editor, stop the play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Otherwise, quit the application
        Application.Quit();
#endif
        Debug.Log("Game is quitting...");
    }
}
