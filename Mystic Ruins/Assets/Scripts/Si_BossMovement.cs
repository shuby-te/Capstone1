using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Si_BossMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnManager;

    public float speed;
    public float lerpSpeed;
    public bool isBreak = false; //����� üũ
    public bool isAttack = false; //�������� ���������� üũ
    public bool isActive = false; //������ �ൿ������ üū
    public bool boomActive = false; //��ź�� �۵������� üũ
    public bool move = false; //
    public int attackNum;
    public int lastAttack = 0;

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
                StartCoroutine(Attack1());
            }
            anim.SetBool("isWalk", true);
            StartCoroutine(TurnHead());
        }
        if(!isAttack&&isActive)
        {
            StartCoroutine(Attack());
        }
    }
    
    IEnumerator Attack() // ���� ���� ������ ����
    {
        if (!isAttack && isActive)
        {
            attackNum = UnityEngine.Random.Range(1, 6);
            if (attackNum != lastAttack)
            {
                isAttack = true;
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
                }
                lastAttack = attackNum;
            }
            else
                isAttack = false;
        }
    }
    IEnumerator deley(int i) //���� ���� ��
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

    IEnumerator Attack1() //�տ�����
    {
        anim.SetInteger("Attack", 1);
        yield return new WaitForSeconds(1.3f);
        anim.SetInteger("Attack", 0);
        anim.SetBool("isBreak", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("isBreak", false);
        isAttack = false;
        yield break;
    }

    IEnumerator Attack2()//��� ��ȯ �� 8����
    {
        anim.SetInteger("Attack", 2);
        yield return new WaitForSeconds(1);
        SpawnManager.attack2();
        yield return new WaitForSeconds(3);
        anim.SetInteger("Attack", 0);
        anim.SetBool("isBreak", true);
        yield return StartCoroutine(deley(2));
        isAttack = false;
        yield break;
    }


    IEnumerator Attack3()//��ź ���̱�
    {
        anim.SetInteger("Attack", 3);
        yield return new WaitForSeconds(1);
        boomActive = true;
        SpawnManager.attack3(gameObject);
        anim.SetInteger("Attack", 0);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(TurnHead());
        yield return StartCoroutine(Attack1());
        yield break;
    }

    IEnumerator Attack4()//����ȯ �� ����߸�
    {
        anim.SetInteger("Attack", 4);
        yield return new WaitForSeconds(1);
        SpawnManager.attack4();
        yield return new WaitForSeconds(0.3f);
        anim.SetInteger("Attack", 0);
        anim.SetBool("isBreak", true);
        yield return StartCoroutine(deley(2));
        isAttack = false;
        yield break;
    }

    IEnumerator Attack5()//���� �� �� ����߸�
    {
        anim.SetInteger("Attack", 5);
        yield return new WaitForSeconds(1);
        SpawnManager.attack5(3, 0.15f);
        yield return new WaitForSeconds(1);
        SpawnManager.attack5(3, 0.15f);
        yield return new WaitForSeconds(2); 
        SpawnManager.attack5(10, 0.15f);
        anim.SetBool("isBreak", true);
        yield return StartCoroutine(deley(2));
        isAttack = false;

        yield break;
    }
    IEnumerator ElementAttack1()
    {

        yield return new WaitForSeconds(1);
    }
    IEnumerator TurnHead()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        
        for (float i = 0; i < 100; i ++)
        {
            Vector3 t_dir = (playerPos - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.03f);
            yield return new FixedUpdate();
        }
    }
}
