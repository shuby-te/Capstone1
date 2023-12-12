using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class OpenWatertank : MonoBehaviour
{
    Animator anim;

    bool isOpen;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("isOpen", true);
            //물 채워지는 기능 구현
        }
    }

    public void CloseValve()
    {
        anim.SetBool("isOpen", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOpen = false;
        }
    }
}
