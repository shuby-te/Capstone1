using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPhase2 : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;

    Animator anim;
    BossPhase2 bp;

    private void Start()
    {
        anim = GetComponent<Animator>();
        bp = GetComponent<BossPhase2>();
    }


    void EndAttack(float t)
    {
        anim.SetInteger("AttackType", 0);
        anim.SetBool("isWalk", false);
        StartCoroutine(bp.AttackDelay(t));
    }
}
