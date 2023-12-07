using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RestrictFromPulley : MonoBehaviour
{
    public GameObject pulley;

    MovePulley pulleyScript;
    BoxCollider boxCollider;

    bool isDetect;

    void Start()
    {
        pulleyScript = pulley.GetComponent<MovePulley>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (isDetect)
        {
            boxCollider.isTrigger = false;
            pulleyScript.isDetect = true;
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDetect = true;
        }
    }
}
