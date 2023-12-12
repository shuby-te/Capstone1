
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject[] landingPos; //0.player 1.center 2.east 3.west 4.south 5.north
    public GameObject attackRange;
    public GameObject hpManager;
    public GameObject dropRange;
    public GameObject dropParticle;
    MeshCollider mc;
    ParticleSystem ps;
    BossMovement bm;
    int count = 0;

    private void Start()
    {
        mc=dropRange.GetComponent<MeshCollider>();
        ps=dropParticle.GetComponent<ParticleSystem>();
        bm=dropRange.transform.parent.GetComponent<BossMovement>();
        dropParticle.SetActive(false);
    }

    void Disable()
    {
        attackRange.SetActive(false);
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

    void Stun()
    {
        gameObject.GetComponent<Animator>().SetFloat("StunMultiplier", 0);
    }

    void Rock()
    {
        bm.Objmanager.DropRockActive(5, 0.15f);
        if (count == 2)
        {
            bm.Objmanager.DropBombActive();
            count = 0;
        }
        count++;
    }
}