using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float dash = 7f;
    public float rotateSpeed = 7f;

    Rigidbody rb;
    Vector3 dir = Vector3.zero;
    bool isDash;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //ket input
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");

        dir.Normalize();

        //dash
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDash = true;
            Vector3 dashPower = transform.forward * dash * -Mathf.Log(1 / 10f);
            rb.AddForce(dashPower, ForceMode.VelocityChange);
            isDash = false;
        }    
    }

    private void FixedUpdate()
    {
        //rotate
        if (dir != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) && Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
                transform.Rotate(0, 1, 0);

            transform.forward = Vector3.Lerp(transform.forward, dir, rotateSpeed * Time.deltaTime);
        }

        //move
        rb.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);
    }
}
