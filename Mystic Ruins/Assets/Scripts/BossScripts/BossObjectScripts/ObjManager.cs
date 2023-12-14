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

    int rockNum = 0;
    int bombNum = 0;
    int count = 5;

    bool turn = true;
    BossMovement bm;

    // Start is called before the first frame update
    void Start()
    {
        bm = boss.GetComponent<BossMovement>();
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
        float x, z, r = 30f;
        while (true)
        {
            x = Random.Range(-30f, 30f);
            z = Random.Range(-30f, 30f);
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
            dropBomb[i].GetComponent<DropBomb>().Disable();
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

    public IEnumerator ThrowPipe()
    {
        while (true)
        {
            turn = true;
            throwPipe[--bm.remainAttack].SetActive(true);
            StartCoroutine(Timer(5));
            while (turn)
            {
                StartCoroutine(bm.TurnHead());
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1.5f);
            //throwPipe의 bool값하나 만들어서 on
        }
    }

    IEnumerator Timer(int i)
    {
        yield return new WaitForSeconds(i);
        turn = false;
    }

}
