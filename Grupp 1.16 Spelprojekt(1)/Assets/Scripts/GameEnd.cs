using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveOnDestroy : MonoBehaviour
{
    // Name of the scene to load after the object is destroyed
    public string testStart;

    // Update method to check for object destruction
    private void OnDestroy()
    {
        // Check if the scene name is not empty
        if (!string.IsNullOrEmpty("testStart"))
        {
            // Load the specified scene
            SceneManager.LoadScene("testStart");
        }
    }


}
