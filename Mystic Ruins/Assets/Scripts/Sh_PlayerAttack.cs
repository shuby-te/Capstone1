using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sh_PlayerAttack : MonoBehaviour
{
    public GameObject attackRange;

    bool isAttack;

    void Start()
    {
        attackRange.SetActive(false);
    }

    void Update()
    {
        attackRange.transform.position = transform.GetChild(0).position + transform.forward * 2f;
        attackRange.transform.rotation = transform.rotation; 

        if(Input.GetMouseButtonDown(0) && !isAttack)
        {
            isAttack = true;
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        attackRange.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackRange.SetActive(false);
        isAttack = false;
    }
}
