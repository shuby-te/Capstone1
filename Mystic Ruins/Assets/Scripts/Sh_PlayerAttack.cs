using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sh_PlayerAttack : MonoBehaviour
{
    public GameObject hpManager;

    BoxCollider col;
    bool isOverlapped;

    private void Start()
    {
        col = this.gameObject.GetComponent<BoxCollider>();
        col.enabled = false;
    }

    public void Attack()
    {
        col.enabled = true;
        
        if (isOverlapped)
            hpManager.GetComponent<Sh_HpManager>().AttackToBoss();
    }

    public void EndAttack()
    {
        col.enabled = false;
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
