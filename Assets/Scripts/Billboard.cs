using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;   
    }

    private void LateUpdate()
    {
        transform.rotation = mainCam.transform.rotation;
    }
}
