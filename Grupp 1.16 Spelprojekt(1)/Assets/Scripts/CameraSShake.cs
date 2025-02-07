    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSShake : MonoBehaviour
{
    [Header("Camera Shake Values")]
    public float magnitude = 0.3f;
    public float timeofShake = 0.2f;



    private Vector3 originalPos; //Kamernans originala position,  MITTEN AV SK�RMEN
    private Vector3 currentCameraPos;
    private float shakeTimeCounter = 0f;
    private float dampeningSpeed = 1.5f;
    public bool isshaking = false;


    private void Awake()
    {
       // currentCameraPos = transform.localPosition;//Nuvarande position till transform.local
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;//
    }

    // Update is called once per frame
    void Update()
    {
        if(isshaking)
        {
            if (shakeTimeCounter > 0)
            {//d�r shakeshake �r offseten, Kalkulerar offseten
                Vector3 shakeShake = Random.insideUnitSphere * magnitude;
                shakeShake.z = 0; //s� den inte skakar p� Z-leden
                //P�verkar CurrenCamaeraPos med offsetten, Till�mpar kamera med offset.
                transform.localPosition +=  shakeShake;
                transform.position = originalPos + (Vector3)Random.insideUnitCircle * magnitude;
                shakeTimeCounter -= Time.deltaTime * dampeningSpeed;
            }
            else
            {
                timeofShake = 0f;
                //�ter st�ller positionen 
                transform.position = originalPos;
                //Upph�r skakningen
                isshaking = false;
               
            }
        }
    }
    

    public void BeginShake(float duration, float magnitudeS)
    {

        timeofShake = duration;
        magnitude = magnitudeS;
        shakeTimeCounter = timeofShake;
        originalPos = transform.position;
        isshaking = true;

    }
}
