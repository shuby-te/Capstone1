using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RestrictFromPulley : MonoBehaviour
{
    public GameObject player;
    public GameObject pulley;

    MovePulley pulleyScript;
    BoxCollider boxCollider;
    PlayerMovement2 pm;

    bool isDetect;

    void Start()
    {
        pulleyScript = pulley.GetComponent<MovePulley>();
        boxCollider = GetComponent<BoxCollider>();
        pm = player.GetComponent<PlayerMovement2>();
    }

    void Update()
    {
        if (pm.setCart && isDetect)
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
