using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform target;
    public float xSens, ySens, verticalLookMin = -80, verticalLookMax = 80;

    Vector3 moveDir;
    public Vector2 newCamRot, oldCamRot;

    Vector3 curVelPos = Vector3.zero;
    Vector2 curVelRot = Vector2.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        transform.position = target.position;

        oldCamRot = newCamRot;
        newCamRot.y += Input.GetAxis("Mouse X") * xSens;
        newCamRot.x -= Input.GetAxis("Mouse Y") * ySens;
        newCamRot.x = Mathf.Clamp(newCamRot.x,  verticalLookMin, verticalLookMax);
        newCamRot = Vector2.SmoothDamp(oldCamRot, newCamRot, ref curVelRot, 0.7f * Time.deltaTime);
        transform.eulerAngles = newCamRot;
    }
}
