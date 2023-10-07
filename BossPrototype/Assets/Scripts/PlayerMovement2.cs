using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if(!isDash)
        {
            dir.x = Input.GetAxisRaw("Horizontal");
            dir.z = Input.GetAxisRaw("Vertical");

            dir.Normalize();
        }

        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash)
        {
            isDash = true;
            speed *= 2;

            StartCoroutine("OffTheDash");
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
    
    IEnumerator OffTheDash()
    {
        yield return new WaitForSeconds(0.4f);
        speed /= 2;
        isDash = false;
    }
}
