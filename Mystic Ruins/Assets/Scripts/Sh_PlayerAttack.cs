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
    }

    public void EndAttack()
    {
        col.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
            hpManager.GetComponent<Sh_HpManager>().AttackToBoss();
    }

    private void OnDisable()
    {
        isOverlapped = false;
    }
}
