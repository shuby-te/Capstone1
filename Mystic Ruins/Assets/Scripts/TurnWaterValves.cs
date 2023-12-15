using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TurnWaterValves : MonoBehaviour
{
    public Camera cameraObj;

    public bool turnOn;

    Animator anim;
    Vector3 cPos = new Vector3(44.79f, 33.1f,118.63f);
    Vector3 cRot = new Vector3(86f, 60.61f, 0);

    bool isValve;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isValve)
        {
            cameraObj.GetComponent<CameraMovement>().enabled = false;
            cameraObj.transform.position = cPos;
            cameraObj.transform.rotation = Quaternion.Euler(cRot);

            if(Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("isOpen", true);
                if (turnOn) turnOn = false;
                else turnOn = true;
            }
        }
        else
        {
            cameraObj.GetComponent<CameraMovement>().enabled = true;
        }
    }

    void CloseValve()
    {
        anim.SetBool("isOpen", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isValve = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isValve = false;
        }
    }
}
