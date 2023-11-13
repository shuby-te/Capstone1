using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sh_PlayerAttack : MonoBehaviour
{
    public GameObject hpManager;

    public bool isOverlapped;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Boss"))
            isOverlapped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
            isOverlapped = false;
    }
}
