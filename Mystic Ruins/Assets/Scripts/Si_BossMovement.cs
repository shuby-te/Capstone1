using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using static UnityEngine.GraphicsBuffer;

public class Si_BossMovement : MonoBehaviour
{
    public GameObject gear;
    public GameObject miniGear;
    public GameObject bomb;
    public GameObject bombSpawn;
    public GameObject player;
    public float speed;
    public float lerpSpeed;

    Animator anim;
    public bool isNotice = false;
    public bool isAttack = false;
    public bool isActive = false;
    public bool boomActive = false;
    public bool move = false;
    public int attackType;

    public int lastAttackType;

    void Start()
    {
        Physics.gravity = Physics.gravity * 10;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float dir = Vector3.Distance(transform.position, player.transform.position);
        if (Input.GetKeyDown(KeyCode.Space) && dir < 4)
        {
            anim.SetBool("isActive", true);
            StartCoroutine(deley());
            isActive = true;
        }
        if (!isAttack && isActive)
        {
            isAttack = true;
        }
        if ((isActive && !isAttack) || move)
        {
            if (move && dir < 4)
            {
                move = false;
                StartCoroutine(AttackType1());
            }
            anim.SetBool("isWalk", true);
            TurnHead();
        }
    }

    IEnumerator deley()
    {
        anim.SetBool("isWalk", false);
        yield return new WaitForSeconds(3);
        yield break;
    }

    IEnumerator AttackType1()
    {
        anim.SetInteger("isAttack", 1);
        yield return new WaitForSeconds(1.3f);

        anim.SetInteger("isAttack", 0);

        yield return new WaitForSeconds(0.3f);
        isAttack = false;
        StartCoroutine(deley());
        yield break;
    }


    IEnumerator AttackType3()
    {
        anim.SetInteger("isAttack", 3);

        GameObject Bomb = Instantiate(bomb, bombSpawn.transform);
        boomActive = true;
        Bomb.transform.parent = bombSpawn.transform;
        yield return new WaitForSeconds(1);
        isAttack = false;
        anim.SetInteger("isAttack", 0);
        yield break;
    }

    IEnumerator AttackType4()
    {
        anim.SetInteger("isAttack", 4);
        Vector3 gearPos = transform.position;
        gearPos.y += 10;
        Instantiate(gear, gearPos, gear.transform.rotation);

        yield break;
    }

    private void TurnHead()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        for (float i = 0; i < 1; i += 0.1f)
        {
            Vector3 t_dir = (playerPos - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, t_dir, i);
        }
    }
}
