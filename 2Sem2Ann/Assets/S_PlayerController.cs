using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        direction = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        rb.velocity = (direction * speed);
        //transform.Translate(direction.normalized * speed);
        //rb.AddForce(direction.normalized * speed, ForceMode.Force);
        //if(rb =!IsGrounded)
        {

        }
    }
}
