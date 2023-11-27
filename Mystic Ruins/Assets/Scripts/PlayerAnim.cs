using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{    
    Animator anim;
    int keyN, mouseN;
    int isAttack;
    float time;
    bool isRoll;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isKnockback", false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)  && !isRoll)
        {
            isRoll = true;
            anim.SetBool("isRoll", true);
            StartCoroutine("OffRolling");
        }      
               
        if (!isRoll && isAttack == 0 && Input.GetMouseButtonDown(0))
        {            
            time = 0;
            anim.SetInteger("isAttack", ++isAttack);
        }
        else if (time < 0.7f && isAttack == 1 && Input.GetMouseButtonDown(0))
        {
            time = 0;
            anim.SetInteger("isAttack", ++isAttack);
        }
        else if (time < 0.7f && isAttack == 2 && Input.GetMouseButtonDown(0))
        {
            anim.SetInteger("isAttack", ++isAttack);
            isAttack = 0;
        }
        else if (time > 0.7f)
        {
            anim.SetInteger("isAttack", 0);
            isAttack = 0;
            time = 0f;
        }

        float yAngle = transform.rotation.y * 180f;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            keyN = 2;
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            keyN = 4;
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            keyN = 6;
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            keyN = 8;
        else if (Input.GetKey(KeyCode.W))
            keyN = 1;
        else if (Input.GetKey(KeyCode.D))
            keyN = 3;
        else if (Input.GetKey(KeyCode.S))
            keyN = 5;
        else if (Input.GetKey(KeyCode.A))
            keyN = 7;
        else
            keyN = 0;

        /*if (-22.5f < yAngle && yAngle <= 22.5f)
            mouseN = 1;
        else if (22.5f < yAngle && yAngle <= 67.5f)
            mouseN = 2;
        else if (67.5f < yAngle && yAngle <= 112.5f)
            mouseN = 3;
        else if (112.5f < yAngle && yAngle <= 157.5f)
            mouseN = 4;
        else if ((157.5f < yAngle && yAngle <= 180f) || (-180f < yAngle && yAngle <= -157.5f))
            mouseN = 5;
        else if (-157.5f < yAngle && yAngle <= -112.5f)
            mouseN = 6;
        else if (-112.5f < yAngle && yAngle <= -67.5f)
            mouseN = 7;
        else if (-67.5f < yAngle && yAngle <= -22.5f)
            mouseN = 8;*/

        if (-22.5f < yAngle && yAngle <= 22.5f)
            mouseN = 1;
        else if (67.5f < yAngle && yAngle <= 112.5f)
            mouseN = 3;
        else if ((157.5f < yAngle && yAngle <= 180f) || (-180f < yAngle && yAngle <= -157.5f))
            mouseN = 5;
        else if (-112.5f < yAngle && yAngle <= -67.5f)
            mouseN = 7;

        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (keyN == 0)
            anim.SetInteger("moveNum", 0);
        else
        {
            int num = mouseN - keyN;
            if (num > 0) num -= 8;
            num = -num + 1;
            anim.SetInteger("moveNum", num);
        }
    }

    IEnumerator OffRolling()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("isRoll", false);
        yield return new WaitForSeconds(2f);
        isRoll = false;
    }

    public void KnockBacked()
    {
        anim.SetBool("isKnockback", true);
    }

    public void StandUp()
    {
        anim.SetBool("isKnockback", false);
    }
}
