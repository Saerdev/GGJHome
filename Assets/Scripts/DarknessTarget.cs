using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessTarget : MonoBehaviour
{
    public Transform target;
    public float lerpTime;

    public float currentLerpTime;

    Vector3 startPos, endPos;

    private void Start()
    {
        startPos = transform.position;
        endPos = target.position;
        currentLerpTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
            currentLerpTime = lerpTime;

        float perc = currentLerpTime / lerpTime;

        if (target != null)
        {
            transform.position = Vector3.Lerp(startPos, target.position, perc);
        }   
    }
}
