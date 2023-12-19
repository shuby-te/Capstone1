using System.Collections;
using UnityEngine;

public class BossPhase1 : MonoBehaviour
{
    public GameObject player;
    public GameObject objManager;
    public GameObject uiManager;
    public GameObject hpManager;
 

    public ObjManager om;
    public int num;
    public int attackNum;
    public int barrierNum = 3;
    public int remainAttack = 5;

    public float hp;
    public float bossSpeed = 1f;
    public float speed = 1;
    public GameObject wall1;
    public GameObject wall2;
    public bool isBlocking = false;
    public bool isStun = false;

    int lastAttack = 0;
    [SerializeField]
    int count;
    public int isSpecial = 0;

    public bool isBreak = false;
    public bool isAttack = false;
    public bool isActive = false;
    bool isDash = true;
    bool overheating = false;
    bool isFar = true;
    bool move = false;
    bool boomActive = false;
    bool isturnhead = false;
    public bool sp = false;
    public bool qwe = false;
    public  int lastSp = 0;
    bool ui = false;
    UIManager um;
    Animator anim;
    public Sh_HpManager hm;
    public GameObject key;
    public GameObject key2;
    public GameObject aiSword;
    void Start()
    {
        om = objManager.GetComponent<ObjManager>();
        um = uiManager.GetComponent<UIManager>();
        anim = GetComponent<Animator>();
        hm = hpManager.GetComponent<Sh_HpManager>();
    }

    void FixedUpdate()
    {
        if (!sp)
        {
            if (lastSp == 0)
            {
                if (hm.bossHp / hm.maxBossHp <= 0.75f && hm.bossHp / hm.maxBossHp > 0.5f)
                {
                    sp = true;
                    qwe = true;
                    isSpecial = 1;
                }
            }
            else if (lastSp == 1)
            {
                if (hm.bossHp / hm.maxBossHp <= 0.5f)
                {
                    sp = true;
                    qwe = true;
                    isSpecial = 2;
                }
            }
            else if (lastSp == 2)
            {
                if (hm.bossHp / hm.maxBossHp <= 0.25f)
                {
                    sp = true;
                    qwe = true;
                    isSpecial = 1;
                }
            }
            else if (lastSp == 3)
            {
                if (hm.bossHp / hm.maxBossHp <= 0)
                {
                    sp = true;

                    GetComponent<Animator>().SetFloat("AttackSpeed", 0);
                    GetComponent<Animator>().SetBool("isStun", false);
                    wall1.SetActive(true);
                    wall2.SetActive(false);
                    key.SetActive(true);
                    key.transform.parent = null;
                    anim.SetBool("die", true);
                    aiSword.SetActive(false);
                    lastSp = 4;
                    if (ui)
                    {
                        um.UiSwhitch();
                        ui = false;
                    }
                }
            }
        }
        if (qwe)
        {
            if (isSpecial == 1)
            {
                isAttack = true;
                StartCoroutine(SpecialAttack1());
            }
            else if (isSpecial == 2)
            {
                isAttack = true;
                StartCoroutine(SpecialAttack2());
            }
        }

        float dir = Vector3.Distance(transform.position, player.transform.position);

        DisCheck();
        if (!isActive && Input.GetKeyDown(KeyCode.E) && dir < 4)
        {
            wall1.SetActive(false);
            wall2.SetActive(true);
            key2.SetActive(false);
            anim.SetBool("isActive", true);
            if (!ui)
            {
                um.UiSwhitch();
                ui = true;
            }
            StartCoroutine(deley(3f));
        }
        if (move)
        {
            if (dir < 7)
            {
                move = false;
            }
            anim.SetBool("isWalk", true);
            StartCoroutine(TurnHead());
        }
        if (isActive && !isAttack && !isBreak && !isStun &&!sp)
        {
            if (isFar && !isturnhead)
            {
                if (count == 3)
                    StartCoroutine(Attack());
                else
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
        if (dir < 6)
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
            if (count == 3 && !overheating)
            {
                attackNum = Random.Range(9, 11);
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
                        StartCoroutine(ElementAttack2());
                        break;
                }
                StartCoroutine(TurnHead());
            }
            else
            {
                attackNum = UnityEngine.Random.Range(1, 7);
                if (attackNum != lastAttack)
                {
                    isAttack = true;
                    yield return StartCoroutine(TurnHead());
                    anim.SetBool("isAttack", true);
                    switch (attackNum)
                    {
                        case 1:
                            StartCoroutine(Attack1());
                            break;
                        case 2:
                             StartCoroutine(Attack2());
                            break;
                        case 3:
                            if (!boomActive)
                                StartCoroutine(Attack3());
                            else isAttack = false;
                            break;
                        case 4:
                            StartCoroutine(Attack4());
                            break;
                        case 5:
                            StartCoroutine(Attack5());
                            break;
                        case 6:
                            StartCoroutine(Attack6());
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                    }
                    StartCoroutine(TurnHead());
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
        anim.SetBool("isBreak", true);
        isBreak = true;
        yield return new WaitForSeconds(i / bossSpeed);
        DisCheck();
        anim.SetBool("isBreak", false);
        isBreak = false;
        anim.SetBool("isAttack", false);
        isAttack = false;
        yield break;
    }
    IEnumerator Attack1()
    {
        move = true;
        while (isFar) {
            yield return new WaitForFixedUpdate();
                }
        anim.SetInteger("AttackType", 1);
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern1"))
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        anim.SetInteger("AttackType", 0);
        yield return StartCoroutine(deley(1));
        yield break;
    }
    IEnumerator Attack2()
    {
        anim.SetInteger("AttackType", 2);
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern2") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
                break;
            yield return new WaitForEndOfFrame();
        }
        om.BigRockActive();
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern2") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.93f)
                break;
            yield return new WaitForEndOfFrame();
        }
        om.BigRockInactive();
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern2"))
                break;
            yield return new WaitForEndOfFrame();
        }
        anim.SetInteger("AttackType", 0);
        yield return StartCoroutine(deley(2));
        yield break;
    }
    IEnumerator Attack3()
    {
        anim.SetInteger("AttackType", 3);
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern3") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
                break;
            yield return new WaitForEndOfFrame();
        }
        om.BombActive();
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern3"))
                break;
            yield return new WaitForEndOfFrame();
        }
        anim.SetInteger("AttackType", 0);
        yield return StartCoroutine(TurnHead());
        DisCheck();
        yield return StartCoroutine(Attack1());
        yield break;
    }// 점찍으로 고쳐보기
    IEnumerator Attack4()
    {
        anim.SetInteger("AttackType", 4);
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern4") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
                break;
            yield return new WaitForEndOfFrame();
        }
        for (int i=0;i<6;i++)
        {
            om.DropGear(i);
        }
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern4"))
                break;
            yield return new WaitForEndOfFrame();
        }
        anim.SetInteger("AttackType", 0);
        yield return StartCoroutine(deley(2f));
        yield break;
    }
    IEnumerator Attack5()
    {
        anim.SetInteger("AttackType", 5);
        {
            while (true)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern5") &&
                    anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f)
                    break;
                yield return new WaitForEndOfFrame();
            }
        }
        om.DropRockActive(10, 0.15f);
        {
            while (true)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern5") &&
                    anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.45f)
                    break;
                yield return new WaitForEndOfFrame();
            }
        }
        om.DropRockActive(10, 0.15f);
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern5")&&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime>=0.8f)
                break;
            yield return new WaitForEndOfFrame();
        }
        om.DropRockActive(15, 0.15f);
        anim.SetInteger("AttackType", 0);
        yield return StartCoroutine(deley(1.5f));
        yield break;
    }
    IEnumerator Attack6()
    {
        anim.SetInteger("AttackType", 6);
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern6"))
                break;
            yield return new WaitForEndOfFrame();
        }
        anim.SetInteger("AttackType", 0);
        yield return StartCoroutine(deley(1.5f));
        yield break;
    }
    IEnumerator ElementAttack1()
    {
        anim.SetInteger("AttackType", 9);
        yield return new WaitForSeconds(1);

        while (true)
        {
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Element1-1"))
                break;
            if(isStun)
            {
                anim.SetBool("isStun", true);
                yield return new WaitForSeconds(0.3f);
                anim.SetBool("isStun", false);
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Element1-2") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
            {
                anim.SetInteger("AttackType", 0);
                for (int i = 0; i < 16; i++)
                {
                    om.SpawnFire(i);
                    yield return new WaitForSeconds(0.1f / bossSpeed);
                }
                break;
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
            {
                yield return new WaitForSeconds(3 / bossSpeed);
                anim.SetFloat("StunMultiplier", 1);
                isStun = false;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(deley(3));
        isAttack = false;
        count = 0;
     }
    IEnumerator ElementAttack2()
    {
        yield return new WaitForSeconds(1);

        anim.SetInteger("AttackType", 10);
        yield return new WaitForSeconds(0.5f);

        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Element2-1"))
        {
            StartCoroutine(TurnHead());
            yield return new WaitForEndOfFrame();
        }
        isDash = true;
        int i = 0;
        while (i<30/bossSpeed)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Element2-1")&&isDash)
            {
                    transform.position += Vector3.Normalize(transform.forward) * num / 30 / bossSpeed;
            }
            i++;
            yield return new WaitForEndOfFrame();
        }

        anim.SetBool("isBreak", true);
        anim.SetInteger("AttackType", 0);
        StartCoroutine(deley(3));   
        StartCoroutine(TurnHead());
        DisCheck();
        isAttack = false;
        attackNum = 0;
        count = 0;
    }
    IEnumerator SpecialAttack1()
    {
        qwe = false;
        if (lastSp == 0 || lastSp == 2)
        {
            while (true)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") || anim.GetCurrentAnimatorStateInfo(0).IsName("stop"))
                {
                    StopCoroutine(OverHeat());
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    overheating = false;
                    anim.SetFloat("AttackSpeed", 1);
                    anim.SetInteger("SpacialAttack", 1);
                    yield return new WaitForSeconds(0.5f);
                    anim.SetInteger("SpacialAttack", 0);
                    Debug.Log("aaa");
                }

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Special1-1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Special1-2"))
                {
                    isSpecial = 0;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }

            while (true)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
                    if (lastSp == 0)
                    {
                        lastSp = 1;
                        sp = false;
                        Debug.Log("bbb");
                        break;
                    }
                    else if (lastSp == 2)
                    {
                        lastSp = 3;
                        sp = false;
                        Debug.Log("ccc");

                        break;
                    }

                yield return null;
            }
        }
    }
    IEnumerator SpecialAttack2()
    {
        qwe = false;
        sp = true;
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") || anim.GetCurrentAnimatorStateInfo(0).IsName("stop"))
            {
                StopCoroutine(OverHeat());
                gameObject.GetComponent<BoxCollider>().enabled = false;
                overheating = false;
                anim.SetFloat("AttackSpeed", 1);
                anim.SetInteger("SpacialAttack", 2);
                yield return new WaitForSeconds(0.2f);
                anim.SetInteger("SpacialAttack", 0);
                isSpecial = 0;
                break;

            }
            yield return new WaitForEndOfFrame();
        }
        if(isSpecial==2)
        {
            StartCoroutine(SpecialAttack2());
            yield break;
        }
            while (true)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
                {
                    lastSp = 2;
                    sp = false;
                }
                yield return null;
            }        
    }
    public IEnumerator TurnHead()
    {
        if (!isturnhead)
        {
            isturnhead = true;
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            for (float i = 0; i < 10; i++)
            {
                Vector3 t_dir = (playerPos - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.1f);
                yield return new WaitForFixedUpdate();
            }
            isturnhead = false;
        }
    }
    public IEnumerator Stun(float i)
    {
        isStun = true;
        if (sp)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            sp = false;
        }
        transform.GetComponent<BossAttackPhase1>().Disable1();
        anim.SetFloat("AttackSpeed", 1);
        anim.SetBool("isStun", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isStun", false);
        yield return new WaitForSeconds(i);
        anim.SetFloat("StunMultiplier", 1);
        anim.SetInteger("AttackType", 0);
        overheating = false;
        isAttack = false;
        isStun = false;
        attackNum = 0;
        if (i == 5.5)
            bossSpeed = 1;
        count = 0;
 
    }
    public IEnumerator OverHeat()
    {
        overheating = true;
        yield return new WaitForSeconds(15);
        hm.BossOverHeat();
        StartCoroutine(Stun(5.5f));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
            isDash = false;
    }
}
