using UnityEngine;

public class BossAttackPhase1 : MonoBehaviour
{
    public GameObject[] landingPos; //0.player 1.center 2.east 3.west 4.south 5.north
    public GameObject attackRange;
    public GameObject objectManager;
    public GameObject dropRange;
    public GameObject dropParticle;
    MeshCollider mc;
    ParticleSystem ps;
    BossPhase1 bm;
    ObjManager om;
    int count = 0;

    private void Start()
    {
        mc=dropRange.GetComponent<MeshCollider>();
        ps=dropParticle.GetComponent<ParticleSystem>();
        bm=dropRange.transform.parent.GetComponent<BossPhase1>();
        om = objectManager.GetComponent<ObjManager>();
        dropParticle.SetActive(false);
    }

    void Disable()
    {
        attackRange.SetActive(false);
        mc.enabled = false;
    }
    public void Disable1()
    {
        mc.enabled = false;
        dropParticle.gameObject.SetActive(false);
        ps.Stop();
    }
    void Landing(int i)
    {
        gameObject.transform.position = landingPos[i].transform.position;
        gameObject.transform.rotation = landingPos[i].transform.rotation;
        dropParticle.gameObject.SetActive(true);
        ps.Play();
    }
    void p1()
    {
        attackRange.SetActive(true);
        attackRange.transform.localPosition = new Vector3(0, 0, 3);
        attackRange.transform.localScale = new Vector3(5, 1, 5);
    }

    void p5()
    {
        attackRange.SetActive(true);
        attackRange.transform.localPosition = new Vector3(0, 0, 1);
        attackRange.transform.localScale = new Vector3(2, 0.7f, 2);
    }

    void p6_1()
    {
        attackRange.SetActive(true);
        attackRange.transform.rotation = Quaternion.Euler(0, 120, 0);
        attackRange.transform.localPosition = new Vector3(1, 0, -3);
        attackRange.transform.localScale = new Vector3(3, 1, 6);
    }

    void p6_2()
    {
        attackRange.SetActive(true);
        attackRange.transform.rotation = Quaternion.Euler(0, -120, 0);
        attackRange.transform.localPosition = new Vector3(2, 0, 3);
        attackRange.transform.localScale = new Vector3(3, 1, 6);
    }

    void p7()
    {
        attackRange.SetActive(true);
        attackRange.transform.localPosition = new Vector3(0, 0, 0);
        attackRange.transform.localScale = new Vector3(12, 2, 12);
    }

    void p10()
    {
        attackRange.SetActive(true);
        attackRange.transform.localPosition = new Vector3(0, 4, 3);
        attackRange.transform.localScale = new Vector3(5, 8, 1);
    }
    void sp1()
    {
        mc.enabled = true;
    }

    void sp2_3()//barrier enable
    {
       StartCoroutine(om.EnableBarrier());
    }

    void sp2_4()//orb enable
    {
        StartCoroutine(om.EnableOrb());
        sp2_4_1();
    }

    void sp2_4_1()
    {
        if (bm.remainAttack != 0)
            StartCoroutine(om.TurnHead());
        else
        {
            GetComponent<Animator>().SetBool("throw", false);
            GetComponent<Animator>().SetFloat("AttackSpeed",1);
        }
    }

    void sp2_5()
    {
        om.CheckBarrier();
    }
    void Pass()
    {
        StartCoroutine(GetComponent<BossPhase1>().Stun(0.1f));
    }
    void Stun()
    {
        GetComponent<Animator>().SetFloat("StunMultiplier", 0);
    }

    void Rock()
    {
        om.DropRockActive(7, 0.12f);
        if (count == 2)
        {
            om.DropBombActive();
            count = 0;
        }
        count++;
    }

    void ThrowPipe()
    {
        om.ThrowPipe();
    }

    void SetAnimSpeed(float speed)
    {
        if (speed == -1) speed = 0;
        GetComponent<Animator>().SetFloat("AttackSpeed", speed);
        Debug.Log(speed);
    }
}