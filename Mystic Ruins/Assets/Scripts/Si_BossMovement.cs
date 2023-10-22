using System.Collections;
using UnityEngine;

public class Si_BossMovement : MonoBehaviour
{
    public GameObject gear;
    public GameObject miniGear;
    public GameObject bomb;
    public GameObject bombSpawn;
    public GameObject player;
    public GameObject spawnManager;

    public float speed;
    public float lerpSpeed;
    public bool isBreak = false; //딜레이 체크
    public bool isAttack = false; //공격패턴 진행중인지 체크
    public bool isActive = false; //보스가 행동중인지 체큰
    public bool boomActive = false; //폭탄이 작동중인지 체크
    public bool move = false; //
    public int attackType;
    public int lastAttackType = 0;

    Si_SpawnManeger SpawnManager;
    Animator anim;

    void Start()
    {
        SpawnManager = spawnManager.GetComponent<Si_SpawnManeger>();
        Physics.gravity = Physics.gravity * 10;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float dir = Vector3.Distance(transform.position, player.transform.position);
        if (Input.GetKeyDown(KeyCode.Space) && dir < 4)
        {
            anim.SetBool("isActive", true);
            StartCoroutine(deley(3));
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
        if(!isAttack&&isActive)
        {
            StartCoroutine(Attack());
        }
    }
    
    IEnumerator Attack() // 공격 패턴 무작위 선택
    {
        if (!isAttack && isActive)
        {
            attackType = UnityEngine.Random.Range(1, 6);
            if (attackType != lastAttackType)
            {
                isAttack = true;
                anim.SetBool("isAttack", true);
                switch (attackType)
                {
                    case 1:
                        yield return StartCoroutine(AttackType1());
                        break;
                    case 2:
                        yield return StartCoroutine(AttackType2());
                        break;
                    case 3:
                        if (!boomActive)
                            yield return StartCoroutine(AttackType3());
                        else isAttack = false;
                        break;
                    case 4:
                        yield return StartCoroutine(AttackType4());
                        break;
                    case 5:
                        yield return StartCoroutine(AttackType5());
                        break;
                }
                lastAttackType = attackType;
            }
            else
                isAttack = false;
        }
    }
    IEnumerator deley(int i) //패턴 이후 텀
    {
        if(!isActive)
        {
            yield return new WaitForSeconds(i);
            isActive = true;
            yield break;
        }
        anim.SetBool("isWalk", false);
        isBreak = true;
        yield return new WaitForSeconds(i);
        anim.SetBool("isBreak", false);
        isBreak = false;
        anim.SetBool("isAttack", false);
        yield break;
    }

    IEnumerator AttackType1() //앞에떄림
    {
        anim.SetInteger("AttackType", 1);
        yield return new WaitForSeconds(1.3f);
        anim.SetInteger("AttackType", 0);
        anim.SetBool("isBreak", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("isBreak", false);
        isAttack = false;
        yield break;
    }

    IEnumerator AttackType2()//기어 소환 후 8방향
    {
        anim.SetInteger("AttackType", 2);
        yield return new WaitForSeconds(1);
        SpawnManager.attack2();
        yield return new WaitForSeconds(3);
        anim.SetInteger("AttackType", 0);
        anim.SetBool("isBreak", true);
        yield return StartCoroutine(deley(2));
        isAttack = false;
        yield break;
    }


    IEnumerator AttackType3()//폭탄 붙이기
    {
        anim.SetInteger("AttackType", 3);
        yield return new WaitForSeconds(1);
        boomActive = true;
        SpawnManager.attack3(gameObject);
        anim.SetInteger("AttackType", 0);
        yield return new WaitForSeconds(1);
        TurnHead();
        yield return StartCoroutine(AttackType1());
        yield break;
    }

    IEnumerator AttackType4()//기어소환 후 떨어뜨림
    {
        anim.SetInteger("AttackType", 4);
        yield return new WaitForSeconds(1);
        SpawnManager.attack4();
        yield return new WaitForSeconds(0.3f);
        anim.SetInteger("AttackType", 0);
        anim.SetBool("isBreak", true);
        yield return StartCoroutine(deley(2));
        isAttack = false;
        yield break;
    }

    IEnumerator AttackType5()//전진 후 돌 떨어뜨림
    {
        anim.SetInteger("AttackType", 5);
        yield return new WaitForSeconds(1);
        SpawnManager.attack5(3, 0.15f);
        yield return new WaitForSeconds(1);
        SpawnManager.attack5(3, 0.15f);
        yield return new WaitForSeconds(2); 
        SpawnManager.attack5(10, 0.15f);
        yield return StartCoroutine(deley(2));
        isAttack = false;



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
