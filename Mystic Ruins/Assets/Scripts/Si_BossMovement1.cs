using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Si_BossMovement1 : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnManager;

    public float speed;
    public float lerpSpeed;
    public bool isBreak = false; //����� üũ
    public bool isAttack = false; //�������� ���������� üũ
    public bool isActive = false; //������ �ൿ������ üū
    public bool boomActive = false; //��ź�� �۵������� üũ
    public bool isFar = true;
    public bool move = false; //
    public bool isStun = false;
    public int attackNum;
    public int lastAttack = 0;
    public int count;

    Si_SpawnManeger SpawnManager;
    Animator anim;

    void Start()
    {
        SpawnManager = spawnManager.GetComponent<Si_SpawnManeger>();
        Physics.gravity = Physics.gravity;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float dir = Vector3.Distance(transform.position, player.transform.position);

        DisCheck();
        if (!isActive && Input.GetKeyDown(KeyCode.Space) && dir < 4)
        {
            anim.SetBool("isActive", true);
            StartCoroutine(deley(3f));
        }
        if (move) {
            if(dir<7)
            {
                move = false;
            }
            anim.SetBool("isWalk", true);
            StartCoroutine(TurnHead());
        }
        if (isActive && !isAttack&&!isBreak &&!isStun)
        {

            if (isFar)
            {
                StartCoroutine(TurnHead());
            }
            else
            {
                StartCoroutine(Attack());               
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            StartCoroutine(TurnHead());
        }
    }
    void DisCheck()
    {
        float dir = Vector3.Distance(transform.position, player.transform.position);
        if (dir < 7)
        {
            isFar = false;
            anim.SetBool("isFar", false);
        }
        else
        {
            isFar = true;
            anim.SetBool("isFar", true);
        }

    }
    IEnumerator Attack() // ���� ���� ������ ����
    {
        if (!isAttack && isActive)
        {
            if (count == 3)
            {
                attackNum = UnityEngine.Random.Range(9, 9);
                isAttack = true;
                yield return StartCoroutine(TurnHead());
                yield return StartCoroutine(TurnHead());
                anim.SetBool("isAttack", true);
                switch (attackNum)
                {
                    case 9:
                        StartCoroutine(ElementAttack1());
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                }
            }
            else
            {
                attackNum = UnityEngine.Random.Range(1, 6);
                if (attackNum != lastAttack)
                {
                    isAttack = true;
                    yield return StartCoroutine(TurnHead());
                    anim.SetBool("isAttack", true);
                    switch (attackNum)
                    {
                        case 1:
                            yield return StartCoroutine(Attack1());
                            break;
                        case 2:
                            yield return StartCoroutine(Attack2());
                            break;
                        case 3:
                            if (!boomActive)
                                yield return StartCoroutine(Attack3());
                            else isAttack = false;
                            break;
                        case 4:
                            yield return StartCoroutine(Attack4());
                            break;
                        case 5:
                            yield return StartCoroutine(Attack5());
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                    }
                    lastAttack = attackNum;
                    count++;
                }
                else
                    isAttack = false;
            }
        }
    }
    IEnumerator deley(float i) //���� ���� ��
    {
        if(!isActive)
        {
            yield return new WaitForSeconds(i);
            isActive = true;
            yield break;
        }
        isBreak = true;
        yield return new WaitForSeconds(i);
        DisCheck();
        anim.SetBool("isBreak", false);
        isBreak = false;
        anim.SetBool("isAttack", false);
        yield break;
    }

    IEnumerator Attack1() //�տ�����
    {
        move = true;
        while (isFar) {
            yield return new WaitForFixedUpdate();
                }
        anim.SetInteger("AttackType", 1);
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern1"))
                break;
            yield return new WaitForEndOfFrame();
        }
        anim.SetInteger("AttackType", 0);
        yield return new WaitForSeconds(1);
        DisCheck();
        isAttack = false;
        yield break;
    }//끝

    IEnumerator Attack2()//��� ��ȯ �� 8����
    {
        anim.SetInteger("AttackType", 2);
        while (true) 
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern2")&&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.3f)
                break;
            yield return new WaitForEndOfFrame();
        }        
        SpawnManager.attack2();
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern2"))
                break;
            yield return new WaitForEndOfFrame();
        }
        anim.SetInteger("AttackType", 0);
        anim.SetBool("isBreak", true);
        yield return StartCoroutine(deley(2f));
        isAttack = false;
        yield break;
    }//0.3 끝


    IEnumerator Attack3()//��ź ���̱�
    {
        anim.SetInteger("AttackType", 3);
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern3") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
                break;
            yield return new WaitForEndOfFrame();
        }
        boomActive = true;
        SpawnManager.attack3(gameObject);
        anim.SetInteger("AttackType", 0);
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern3"))
                break;
            yield return new WaitForEndOfFrame();
        }
        yield return StartCoroutine(TurnHead());
        DisCheck();
        yield return StartCoroutine(Attack1());
        yield break;
    }//0.7

    IEnumerator Attack4()//����ȯ �� ����߸�
    {
        anim.SetInteger("AttackType", 4);
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern4") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
                break;
            yield return new WaitForEndOfFrame();
        }
        SpawnManager.attack4();
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern4"))
                break;
            yield return new WaitForEndOfFrame();
        }
        anim.SetInteger("AttackType", 0);
        anim.SetBool("isBreak", true);
        yield return StartCoroutine(deley(2f));
        isAttack = false;
        yield break;
    }//0.4

    IEnumerator Attack5()//���� �� �� ����߸�
    {
        anim.SetInteger("AttackType", 5);
        {
            while (true)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern5") &&
                    anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f)
                    break;
                yield return new WaitForEndOfFrame();
            }
        }
        SpawnManager.attack5(3, 0.15f);
        {
            while (true)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern5") &&
                    anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f)
                    break;
                yield return new WaitForEndOfFrame();
            }
        }
        SpawnManager.attack5(3, 0.15f);
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern5")&&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.8f)
                break;
            yield return new WaitForEndOfFrame();
        }
        SpawnManager.attack5(10, 0.15f);
        anim.SetBool("isBreak", true);
        anim.SetInteger("AttackType", 0);
        yield return StartCoroutine(deley(1.5f));
        isAttack = false;

        yield break;
    }//0.2 0.45 0.8
    IEnumerator Attack6()
    {
        isAttack = false;
        yield break;
    }
    IEnumerator Attack7()
    {
        isAttack = false;
        yield break;
    }
    IEnumerator Attack8()
    {

        yield break;
    }
    IEnumerator ElementAttack1()
    {
        anim.SetInteger("AttackType", 9);
        yield return new WaitForSeconds(1);

        while (true)
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern9_1"))
            {
                break;
            }
            if(isStun)
            {
                anim.SetBool("isStun", true);
                yield return new WaitForSeconds(0.3f);
                anim.SetBool("isStun", false);
                break;
            }
            yield return new FixedUpdate();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern9_2"))
        {
            yield return new WaitForSeconds(0.5f);
            SpawnManager.attack9();
        }
        while(anim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
        {
            yield return new FixedUpdate();
        }
        anim.SetBool("isBreak", true);
        anim.SetInteger("AttackType", 0);
        StartCoroutine(deley(3));
        isAttack = false;
        count = 0;
     }
    IEnumerator TurnHead()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        
        for (float i = 0; i < 10; i ++)
        {
            Vector3 t_dir = (playerPos - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.1f);
            yield return new FixedUpdate();
        }
    }
}
