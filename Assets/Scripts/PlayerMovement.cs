using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    Rigidbody rb;

    Vector3 moveDir;
    Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float prevYVel = rb.velocity.y;
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDir = mainCam.TransformDirection(moveDir);
        moveDir.Normalize();
        moveDir.y = prevYVel;

        rb.velocity = new Vector3(rb.velocity.x * moveSpeed, moveDir.y, rb.velocity.z * moveSpeed);


    }
}
