using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDash = true;
            Vector3 dashPower = transform.forward * dash * -Mathf.Log(1 / 10f);
            rb.AddForce(dashPower, ForceMode.VelocityChange);
            isDash = false;
        }
    }

    private void FixedUpdate()
    {
        //rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 point = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            Vector3 rotateDir = point - transform.position;
            if (rotateDir != Vector3.zero)
                transform.forward = rotateDir;
        }

        //move
        rb.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);
    }
}
