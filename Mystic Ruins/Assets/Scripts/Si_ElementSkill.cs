using System.Collections;
using UnityEngine;

public class Si_ElementSkill : MonoBehaviour
{
    public int time=0;
    public bool isActive = false;
    public bool isSkil = false;
    public int skillNum; // fire = 1 water = 2 metal = 3 
    public GameObject boss;
    BossPhase1 bm;
    Animator anim;
    Sh_HpManager hm;
    bool run = false;
    // Start is called before the first frame update
    void Start()
    {
        hm = boss.GetComponent<BossPhase1>().hm;
        bm=boss.GetComponent<BossPhase1>();
        anim=boss.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            isActive = true;
            time = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2)&&!isActive&&!isSkil)
        {
            isSkil = true;
            StartCoroutine(on(6));
        }
        if (time > 60 && !bm.isStun)
        {
            StartCoroutine(bm.Stun(3));
            time = -999;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Element1-1") && !run)
        {
            StartCoroutine(WaterAttack());
            run = true;
            Debug.Log("enter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (run)
        {
            Debug.Log("exit");
            run = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        float bossSpeed = anim.GetFloat("AttackSpeed");
        if (other.CompareTag("Boss"))
        {
            if (isActive && skillNum == 1)
            {
                if (bossSpeed == 1)
                {
                    anim.SetFloat("AttackSpeed", 1.25f);
                    bm.bossSpeed = 1.25f;
                }
                else
                {
                    anim.SetFloat("AttackSpeed", 1.5f);
                    boss.GetComponent<BossPhase1>().bossSpeed = 1.5f;
                    StartCoroutine(bm.OverHeat());
                }
                isActive = false;
            }
            else if (isActive && skillNum == 2)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Element1-1")&&!run)
                {
                    StartCoroutine(WaterAttack());
                    run = true;
                    Debug.Log("enter");
                }
            }
        }
        else if (isActive && other.CompareTag("Bomb"))
        {
            if (other.GetComponent<DropBomb>() != null && skillNum == 1)
            {
                other.GetComponent<DropBomb>().isFire = true;
                isActive = false;
            }
        }
    }

    IEnumerator WaterAttack()
    {
        time++;
        Debug.Log("attack");
        yield return new WaitForEndOfFrame();
        run = false;
        Debug.Log("stop");
    }

    IEnumerator on(int x)
    {
        yield return new WaitForSeconds(0.4f);
        isActive = true;
        yield return new WaitForSeconds(x);
        isActive = false;
        time = 0;
        yield return new WaitForSeconds(1.3f);
        isSkil = false;
    }
}
