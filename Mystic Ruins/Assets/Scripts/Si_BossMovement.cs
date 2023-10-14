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
        anim = GetComponent<Animator>();
        anim.speed += 1.3f;
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
            StartCoroutine(Attack());
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
    IEnumerator Attack()
    {
        attackType = UnityEngine.Random.Range(1, 5);
        if (lastAttackType == attackType)
        {
            isAttack = false;
            yield break;
        }
        lastAttackType = attackType;
        switch (attackType)
        {
            case 1:
                move = true;
                break;
            case 2:
                yield return StartCoroutine(AttackType2());
                StartCoroutine(deley());
                isAttack = false;
                break;
            case 3:
                if (boomActive) { break; }
                yield return StartCoroutine(AttackType3());
                break;
        }
        yield break;
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

    IEnumerator AttackType2()
    {
        anim.SetInteger("isAttack", 3);
        Vector3 gearPos = transform.localPosition;
        gearPos.x += 3;
        gearPos.y += 3;
        Instantiate(gear, gearPos, gear.transform.rotation);
        yield return new WaitForSeconds(3);
        anim.SetInteger("isAttack", 0);
        for (int i = 1; i < 9; i++)
        {
            gear.transform.Rotate(Vector3.up, 45);
            Quaternion gearRot = gear.transform.rotation;
            Instantiate(miniGear, gearPos, gearRot);
        }
        yield break;
    }

    IEnumerator AttackType3()
    {
        anim.SetInteger("isAttack", 3);

        GameObject Bomb = Instantiate(bomb);
        boomActive = true;
        Bomb.transform.parent = player.transform;
        Bomb.transform.localPosition = new Vector3(0, 3, 0);
        Renderer renderer = Bomb.GetComponent<Renderer>();
        Color targetColora = Color.blue;
        Color targetColorb = Color.magenta;
        Color targetColorc = Color.red;

        renderer.material.color = targetColora;

        yield return new WaitForSeconds(1);
        isAttack = false;
        anim.SetInteger("isAttack", 0);

        renderer.material.color = targetColorb;

        yield return new WaitForSeconds(1);
        renderer.material.color = targetColorc;

        yield return new WaitForSeconds(1);
        Destroy(Bomb);
        boomActive = false;

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
