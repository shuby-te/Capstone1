using System.Collections;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public GameObject bomb;
    public GameObject boss;
    public GameObject bigRock;
    public GameObject orbRot;
    public GameObject[] miniRock = new GameObject[8];
    public GameObject[] dropGear = new GameObject[6];
    public GameObject[] dropBomb = new GameObject[5];
    public GameObject[] dropRock = new GameObject[15];
    public GameObject[] fireBallL = new GameObject[4];
    public GameObject[] fireBallR = new GameObject[4];
    public GameObject[] bossBarrier = new GameObject[3];
    public GameObject[] orb = new GameObject[3];
    public GameObject[] throwPipe = new GameObject[5];
    public ParticleSystem[] explosionParticle;
    public GameObject cutSceanManager;
    public GameObject hpManager;
    int rockNum = 0;
    int bombNum = 0;
    int count = 5;

    bool turn = true;

    CutSceanManager csm;
    BossPhase1 bm;
    Sh_HpManager hm;
    // Start is called before the first frame update
    void Start()
    {
        hm = hpManager.GetComponent<Sh_HpManager>();
        csm = cutSceanManager.GetComponent<CutSceanManager>();
        bm = boss.GetComponent<BossPhase1>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void BigRockActive()
    {
        bigRock.SetActive(true);
    }
    public void BigRockInactive()
    {
        for (int i = 0; i < 8; i++)
        {
            miniRock[i].SetActive(true);
            miniRock[i].transform.position=bigRock.transform.position;
        }
        bigRock.GetComponent<Rock>().Disable();
    }
    public void DropGear(int i)
    {
        dropGear[i%6].SetActive(true);
    }
    public void BombActive()
    {
        bomb.SetActive(true);
    }
    public void DropRockActive(int count, float r)
    {
        float x, z;
        int i = 0;
        while (i < count)
        {
            x = Random.Range(-0.12f, 0.12f);
            z = Random.Range(-0.12f, 0.12f);
            if (x * x + z * z < r * r)
            {
                i++;
                dropRock[rockNum].transform.localPosition = new Vector3(x, 0, z);
                dropRock[rockNum++].SetActive(true);
            }
            if (rockNum == 14)
                rockNum = 0;
        }
    }
    public void SpawnFire(int i)
    {
        if (i % 2 == 0)
            fireBallL[i / 2].SetActive(true);
        else
            fireBallR[i / 2].SetActive(true);
    }
    public void DropBombActive()
    {
        float x, z, r = 0.12f;
        while (true)
        {
            x = Random.Range(-0.12f, 0.12f);
            z = Random.Range(-0.12f, 0.12f);
            if (x * x + z * z < r * r)
            {
                dropBomb[bombNum].SetActive(true);
                dropBomb[bombNum++].transform.localPosition = new Vector3(x, 0.2f, z);
                if(bombNum==4)
                {
                    bombNum = 0;
                }
                break;
            }
        }
    }
    public void DropBombInactive()
    {
        for(int i= 0; i < 4; i++)
            dropBomb[i].SetActive(false);
    }

    public IEnumerator EnableBarrier()
    {
        for (int i = 0; i < bossBarrier.Length; i++)
        {
            bossBarrier[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public IEnumerator EnableOrb()
    {
        orbRot.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        for (int i = orb.Length - 1; i + 1 > 0; i--)
        {
            orb[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }   
    }

    public IEnumerator TurnHead()
    {
        Debug.Log("turnturn");
        turn = true;
        throwPipe[--bm.remainAttack].SetActive(true);
        StartCoroutine(Timer(5));
        while (turn)
        {
            StartCoroutine(bm.TurnHead());
            yield return new WaitForEndOfFrame();
        }
        throwPipe[bm.remainAttack].transform.parent = null;
        boss.GetComponent<Animator>().SetFloat("AttackSpeed", 1);
    }

    public void ThrowPipe()
    {
        if (count-- != 0)
        {
            throwPipe[bm.remainAttack].GetComponent<ThrowPipe>().isThrow = true;
            throwPipe[bm.remainAttack].GetComponent<ThrowPipe>().orbNum = boss.GetComponent<BossPhase1>().barrierNum;
        }
        else
            CheckBarrier();
    }

    public void CheckBarrier()
    {
        for(int i=0;i< throwPipe.Length;i++)
        {
            Destroy(throwPipe[i]);
        }
        bool remain = false;
        if (bossBarrier != null)
            for (int i = 0; i < bossBarrier.Length; i++)
            {
                if (bossBarrier[i] != null)
                    remain = true;
            }

        if(remain)
        {
            for (int i = 0; i < bossBarrier.Length; i++)
            {
                if (bossBarrier[i] != null) ;
                    //StartCoroutine(BarrierGrow(i));
            }
            int c = 15;
            DropRockActive(15, 0.12f);
            for (int i = 0; i < 12; i++)
                SpawnFire(i);
            for (int i = 0; i < bossBarrier.Length; i++)
                if (bossBarrier[i] != null)
                    Destroy(bossBarrier[i]);
            for (int i = 0; i < explosionParticle.Length; i++)
                explosionParticle[i].Play();
            StartCoroutine(Grow());
        }
        else
        {
            StartCoroutine(csm.CutSceanStart(2));
        }
    }

    IEnumerator BarrierGrow(int i)
    {
        while (bossBarrier[i].transform.localScale.x<3) 
        {
            if (bossBarrier[i] != null)
                bossBarrier[i].transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            else
                yield break;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Grow()
    {
        StartCoroutine(Die());
        while (explosionParticle[0].transform.localScale.x < 5)
        {
            explosionParticle[0].transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            explosionParticle[1].transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            explosionParticle[2].transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            explosionParticle[0].transform.localPosition += Vector3.down * 0.1f;
            explosionParticle[1].transform.localPosition += Vector3.down * 0.1f;
            explosionParticle[2].transform.localPosition += Vector3.down * 0.1f;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Timer(int i)
    {
        yield return new WaitForSeconds(i);
        turn = false;
    }
    IEnumerator Die()
    {
        while (hm.playerHp != 0)
        {
            hm.playerHp -= 1;
            hm.hpSlider.value = hm.playerHp;
            yield return new WaitForEndOfFrame();
        }
    }

}
