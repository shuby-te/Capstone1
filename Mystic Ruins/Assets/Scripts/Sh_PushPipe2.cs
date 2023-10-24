using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sh_PushPipe2 : MonoBehaviour
{
    //public GameObject player;
    //public GameObject pipeCols;
    public int num;
    public float rotateSpeed;

    Rigidbody rb;
    bool isTouch;
    bool isPush;
    int x, z;
    

    void Start()
    {
        isTouch = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.F))
        {
            
        }

        Quaternion newRotation = Quaternion.Euler(new Vector3(transform.transform.rotation.x,
                transform.transform.rotation.y + 90, transform.transform.rotation.z));

        rb.rotation = Quaternion.Lerp(rb.rotation, newRotation, rotateSpeed * Time.deltaTime);
        Debug.Log("aa");
    }

    IEnumerator Push(bool ClockWise)
    {
        float n = 0;
        float dir = (ClockWise ? 0.5f : -0.5f);
        while (n < 90)
        {
            yield return new WaitForSeconds(0.001f);

            transform.Rotate(0, dir, 0);
            yield return null;
            n += dir;
        }
        //pipeCols.transform.rotation = Quaternion.Euler(0, 0, 0);
        isPush = false;        
    }

    
}
