using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;



public class projectile : MonoBehaviour
{
    private Transform projectileTransform;

    private void Awake()
    {
        projectileTransform = transform.Find("Firepoint");
    }

    private void Update()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 projectileDirection = mousePosition - (transform.position).normalized;
        float angle = Mathf.Atan2(projectileDirection.x, projectileDirection.y) * Mathf.Rad2Deg;
    }
}

    



