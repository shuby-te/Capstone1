using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TurnWaterValves : MonoBehaviour
{
    public bool turnOn;

    Animator anim;

    bool isValve;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isValve)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("isOpen", true);
                if (turnOn) turnOn = false;
                else turnOn = true;
            }
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
