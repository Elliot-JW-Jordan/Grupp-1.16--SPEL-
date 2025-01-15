using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveOnDestroy : MonoBehaviour
{
    public string testStart;

    private void OnDestroy()
    {
        if (!string.IsNullOrEmpty("testStart"))
        {
            SceneManager.LoadScene("testStart");
        }
    }


}
