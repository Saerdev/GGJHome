using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed, xSens, ySens, shiftModifier = 2.5f;

    Vector3 moveDir;
    Vector2 newCamRot, oldCamRot;

    Vector3 curVelPos = Vector3.zero;
    Vector2 curVelRot = Vector2.zero;

    void LateUpdate()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveDir = transform.TransformDirection(moveDir);
        transform.position = Vector3.SmoothDamp(transform.position, transform.position + moveDir * moveSpeed, ref curVelPos, 0.7f);

        if (Input.GetKey(KeyCode.LeftControl))
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed *= shiftModifier;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed /= shiftModifier;

        oldCamRot = newCamRot;
        newCamRot.y += Input.GetAxis("Mouse X") * xSens;
        newCamRot.x -= Input.GetAxis("Mouse Y") * ySens;
        newCamRot = Vector2.SmoothDamp(oldCamRot, newCamRot, ref curVelRot, 0.7f * Time.deltaTime);
        transform.eulerAngles = newCamRot;
    }
}
