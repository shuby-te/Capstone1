using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_PlayerAttackTest : MonoBehaviour
{
    Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            Anim.SetInteger("isAttack", 1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            Anim.SetInteger("isAttack", 2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Anim.SetInteger("isAttack", 3);
        }


    }
}
