    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSShake : MonoBehaviour
{
    [Header("Camera Shake Values")]
    public float magnitude = 0.3f;
    public float timeofShake = 0.2f;



    private Vector3 originalPos; //Kamernans originala position,  MITTEN AV SKÄRMEN
    private Vector3 currentCameraPos;
    private float shakeTimeCounter = 0f;
    private bool isshaking = false;


    private void Awake()
    {
        currentCameraPos = transform.localPosition;//Nuvarande position till transform.local
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isshaking)
        {
            if (shakeTimeCounter > 0)
            {//där shakeshake är offseten, Kalkulerar offseten
                Vector3 shakeShake = Random.insideUnitSphere * magnitude;
                shakeShake.z = 0; //så den inte skakar på Z-leden
                //Påverkar CurrenCamaeraPos med offsetten, Tillämpar kamera med offset.
                transform.localPosition = currentCameraPos + shakeShake;
                shakeTimeCounter -= Time.deltaTime;
            }
            else
            {
                //Upphör skakningen
                isshaking = false;
                transform.localPosition = currentCameraPos;
            }
        }
    }

    public void BeginShake(float duration, float magnitudeS)
    {

        timeofShake = duration;
        magnitude = magnitudeS;
        shakeTimeCounter = timeofShake;
        isshaking = true;

    }
}
