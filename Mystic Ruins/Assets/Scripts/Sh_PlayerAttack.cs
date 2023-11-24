using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sh_PlayerAttack : MonoBehaviour
{
    public GameObject hpManager;

    bool isOverlapped;

    void Attack()
    {
        if (isOverlapped)
            hpManager.GetComponent<Sh_HpManager>().AttackToBoss();
    }

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
