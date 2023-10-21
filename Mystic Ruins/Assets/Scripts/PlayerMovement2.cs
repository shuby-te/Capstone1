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
    GameObject partner;

    Vector3 dir = Vector3.zero;
    bool isDash;
    bool isMove;
    float xAxis = 1f, zAxis = -1f;
    float partnerSpeed = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        partner = this.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        //key input
        if (!isDash)
        {
            dir.x = Input.GetAxisRaw("Horizontal");
            dir.z = Input.GetAxisRaw("Vertical");

            dir.Normalize();
        }

        if(Input.anyKey) isMove = true;
        else isMove = false;

        if (Input.GetKey(KeyCode.W)){
            zAxis += Time.deltaTime * partnerSpeed;
            if (zAxis > 1f) zAxis = 1f;
        }
        if (Input.GetKey(KeyCode.S)){
            zAxis -= Time.deltaTime * partnerSpeed;
            if (zAxis < -1f) zAxis = -1f;
        }
        if (Input.GetKey(KeyCode.D)){
            xAxis += Time.deltaTime * partnerSpeed;
            if (xAxis > 1f) xAxis = 1f;
        }
        if (Input.GetKey(KeyCode.A)){
            xAxis -= Time.deltaTime * partnerSpeed;
            if (xAxis < -1f) xAxis = -1f;
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
            //Vector3 point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 point = hit.point;

            Vector3 rotateDir = point - transform.position;
            if (rotateDir != Vector3.zero)
                transform.forward = rotateDir;
        }

        //move
        if(isMove)
            rb.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);

        //partner move
        partner.transform.position = new Vector3(transform.position.x - xAxis * 1.6f,
            transform.position.y + 2.5f, transform.position.z - zAxis * 1.6f);
    }
    
    IEnumerator OffTheDash()
    {
        yield return new WaitForSeconds(0.4f);
        speed /= 2;
        isDash = false;
    }
}
