using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontDesttroy : MonoBehaviour
{


    public static dontDesttroy instance;
    // Start is called before the first frame update
    void Start()
    {

        // ifall ingen annan instans finnd, gör detta till instansen
        if (instance == null)
        {
            instance = this;
            //  den aktiva scenen
            Scene currentScene = SceneManager.GetActiveScene();

            DontDestroyOnLoad(gameObject);


           
        }
        else
        {
          Destroy(gameObject); // IFALL EN INSTANS REDAN FINNS, RADERA DENNA;
        }


    }

    // Update is called once per frame
    void Update()
    {

        Scene currentScene = SceneManager.GetActiveScene(); // den aktive scenen.

        if (currentScene.name == "testStart") // så shopUI inte finns i start
        {
            Destroy(gameObject);
        }
      
    }
}
