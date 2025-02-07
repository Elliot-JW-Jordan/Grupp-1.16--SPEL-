using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public string audioFileName = "MainGameMusic"; 
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
               audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>(audioFileName);
        
        if (clip != null)
        {   
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("MP3-filen kunde inte laddas. Kontrollera att den finns i Resources-mappen.");
        }
    }

    
}
