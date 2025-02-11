using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class dontDesttroy : MonoBehaviour
{


    public static  dontDesttroy instance;
    // Start is called before the first frame update
    void Start()
    {

        // ifall ingen annan instans finnd, gör detta till instansen
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject); // IFALL EN INSTANS REDAN FINNS, RADERA DENNA;
        }
        
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
