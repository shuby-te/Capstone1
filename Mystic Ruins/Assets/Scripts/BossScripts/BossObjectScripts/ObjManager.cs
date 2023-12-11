using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bigRock;
    public GameObject[] miniRock = new GameObject[8];
    public GameObject[] dropGear = new GameObject[6];
    public GameObject[] dropBomb = new GameObject[5];
    public GameObject[] dropRock = new GameObject[15];
    public GameObject[] fireBallL = new GameObject[4];
    public GameObject[] fireBallR = new GameObject[4];


    int rockNum = 0;
    int bombNum = 0;


    // Start is called before the first frame update
    void Start()
    {

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
        bigRock.GetComponent<Rock>().Disable();
        for (int i = 0; i < 8; i++)
        {
            miniRock[i].SetActive(true);
        }
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

}
