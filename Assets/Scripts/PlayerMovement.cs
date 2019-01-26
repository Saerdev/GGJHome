using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10, stairGravityMultiplier = 5, groundCheckDistance = 0.5f, gravity = 9.8f;
    CharacterController cc;

    Vector3 moveDir;
    Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        mainCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDir = mainCam.TransformDirection(moveDir);
        moveDir.Normalize();
        moveDir *= moveSpeed;
        moveDir.y = IsGrounded() ? moveDir.y - (gravity * stairGravityMultiplier) : moveDir.y - gravity;

        cc.Move(moveDir * Time.deltaTime);
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance))
        {
            return true;
        }
        else
            return false;
    }
}
