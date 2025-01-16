    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSShake : MonoBehaviour
{
    [Header("Camera Shake Values")]
    public float magnitude = 0.3f;
    public float timeofShake = 0.2f;



    private Vector3 originalPos;
    private float shakeTimeCounter = 0f;
    private bool isshaking = false;


    private void Awake()
    {
        originalPos = transform.localPosition;
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
            {//där shakeshake är offseten
                Vector3 shakeShake = Random.insideUnitSphere * magnitude;
                shakeShake.z = 0;
                transform.localPosition = originalPos + shakeShake;
                shakeTimeCounter -= Time.deltaTime;
            }
            else
            {
                isshaking = false;
                transform.localPosition = originalPos;
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
