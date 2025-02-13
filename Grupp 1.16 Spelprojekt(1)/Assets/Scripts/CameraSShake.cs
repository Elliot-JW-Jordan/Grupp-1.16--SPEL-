    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSShake : MonoBehaviour
{
    [Header("Camera Shake Values")]
    public float magnitude = 0.5f; // innan 0.3
    public float timeofShake = 0.2f;



    private Vector3 originalLocalPos; //Kamernans originala position,  MITTEN AV SKÄRMEN
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
        originalLocalPos = transform.localPosition;//
    }

    public void BeginShake(float duration, float magnitudeS)
    {
        // Kollar ifall kameran redan skakar eller inte
        if (isshaking)
        {
            // ifall kamaeran redan skakar så ska coden ignorera den nya skakningen.
            return;
        }
        originalLocalPos = transform.localPosition; // positionering
         //parametrar
        timeofShake = duration;
        magnitude = magnitudeS;
        shakeTimeCounter = timeofShake;
        
       isshaking = true;

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
                transform.localPosition = originalLocalPos + shakeShake;  // applicerar shakeshake till orginal positionen
              //  transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * magnitude;
                shakeTimeCounter -= Time.deltaTime * dampeningSpeed;
            }
            else
            {
                timeofShake = 0f;
                //åter ställer positionen 
                transform.localPosition = originalLocalPos;
                //Upphör skakningen
                isshaking = false;
               
            }
        }
    }
    

  

}
