using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class BossPhase2 : MonoBehaviour
{
    public GameObject player;
    public GameObject farTrigger;
    public RuntimeAnimatorController animController;

    public bool isBehave = true;
    public bool isAttack = false;

    [SerializeField]
    int attackNum;
    [SerializeField]
    float speed;
    [SerializeField]
    bool isWalk = true;
    [SerializeField]
    bool turnHead = false;
    [SerializeField]
    bool isFar = true;
    bool isCheck = false;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        anim.runtimeAnimatorController = animController;
        StartCoroutine(TurnHead());
        StartCoroutine(Walk());
        StartCoroutine(ExecuteBossPattern());
        StartCoroutine(AttackDelay(2));
    }

    // Update is called once per frame
    void Update()
    {              
        float dir = Vector3.Distance(transform.position, player.transform.position);
        if (dir > 5 && !isAttack)
        {
            isFar = true;
            anim.SetBool("isWalk", true);
        }
        
        if (isAttack && anim.GetInteger("AttackType") == 0 && !isCheck)
        {
            isCheck = true;
            StartCoroutine(CheckAttack());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isWalk", false);
            isFar = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isWalk", true);
            isFar = true;
        }
    }

    IEnumerator TurnHead()
    {
        bool ing = false;
        while (true)
        {
            if (turnHead)
                if (!ing)
                {
                    ing = true;
                    Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z); ;
                    for (float i = 0; i < 10; i++)
                    {
                        Vector3 t_dir = (playerPos - transform.position).normalized;
                        transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.1f);
                        yield return new WaitForEndOfFrame();
                    }
                    turnHead = false;
                }
            ing = false;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Walk()
    {
        while (true)
        {
            if (isFar && !isAttack)
            {
                turnHead = true;
                transform.position += transform.forward * Time.deltaTime * speed;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ExecuteBossPattern()
    {
        while (true)
        {
            if (!isAttack && !isFar)
            {
                isAttack = true;
                attackNum = Random.Range(1, 4);
                anim.SetInteger("AttackType", attackNum);
                Debug.Log("ATTACK");
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator AttackDelay(float t)
    {
        yield return new WaitForSeconds(t);
        isAttack = false;
    }

    IEnumerator CheckAttack()
    {
        int i = 0;
        while(true)
        {
            i++;
            if(!isAttack)
            {
                isCheck = false;
                yield break;
            }
            if(i==120)
            {
                isAttack = false;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
    