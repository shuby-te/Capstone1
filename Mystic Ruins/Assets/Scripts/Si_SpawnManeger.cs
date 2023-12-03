using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Si_SpawnManeger : MonoBehaviour
{
    public GameObject gearSpawnPoint;
    public GameObject gearSpawnRotate;
    public GameObject spreadGearSpawnPoint;
    public GameObject bombSpawnPoint;
    public GameObject bossRoomCenter;
    public GameObject dropGear;
    public GameObject bigGear;
    public GameObject miniGear;
    public GameObject bomb;
    public GameObject rock;
    public GameObject fire;
    public GameObject firePos1;
    public GameObject firePos2;
    public GameObject dropbomb;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attack2()
    {
        StartCoroutine(SpreadGear());
    }
    public void attack3()
    {
        StartCoroutine(Bomb());
        Debug.Log("1");
    }
    public void attack4()
    {
        StartCoroutine(DropGear());
    }
    public void attack5(int count, float r)
    {
        StartCoroutine(SpawnRock(count, r));
    }
    public void attack9()
    {
        StartCoroutine(SpawnFire());
    }
    public void GearPiece()
    {
        Vector3 pos = gearSpawnPoint.transform.position;

        for (int i = 0; i < 8; i++)
        {
            bigGear.transform.Rotate(Vector3.forward, 45);
            Quaternion gearRot = bigGear.transform.rotation;
            Instantiate(miniGear, pos, gearRot);
        }
    }
    public void DropBomb()
    {
        StartCoroutine(SpawnBomb());
    }
    IEnumerator SpreadGear()
    {
        Vector3 pos = gearSpawnPoint.transform.position;
        GameObject a = Instantiate(bigGear, pos, Quaternion.identity);
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pattern2") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                break;
            yield return new WaitForEndOfFrame();
        }
        Destroy(a);
        yield break;
    }

    IEnumerator DropGear()
    {
        GameObject[] gear = new GameObject[9];
        gearSpawnRotate.transform.Rotate(Vector3.up, Random.Range(0,360));
        for (int i = 0; i < 6; i++)
        {
            gearSpawnRotate.transform.Rotate(Vector3.up, 60);
            Vector3 pos = gearSpawnPoint.transform.position;
            pos.y += 4;
            gear[i]=Instantiate(dropGear, pos, dropGear.transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 6; i++)
            gear[i].GetComponent<Si_Obj>().isDrop = true;
        gearSpawnRotate.transform.rotation = new Quaternion(0,0,0,0);
    }

    IEnumerator Bomb()
    {
        Instantiate(bomb);
        yield return null;
    }

    IEnumerator SpawnRock(int count,float r)
    {
        float x, z;
        float rx, ry, rz, rw;
        int i = 0;
        while (i < count)
        {
            x = Random.Range(-0.12f, 0.12f);
            z = Random.Range(-0.12f, 0.12f);

            rx = Random.Range(-1, 1);
            ry = Random.Range(-1, 1);
            rz = Random.Range(-1, 1);
            rw = Random.Range(-1, 1);

            if (x * x + z * z < r * r)
            {
                i++;
                GameObject Rock = Instantiate(rock, bossRoomCenter.transform.localPosition, new Quaternion(rx, ry, rz, rw));
                //GameObject Rock=Instantiate(rock, rockSpawnPoint.transform);
                Rock.transform.parent = bossRoomCenter.transform.parent;
                Rock.transform.localPosition = new Vector3(x, 0.3f, z);
                Rock.transform.localScale = new Vector3(0.007f, 0.007f, 0.007f);
            }   
        }
        yield break;
    }
    IEnumerator SpawnFire()
    {
        float x, z;
        int i = 0;
        Transform tf;

        while (i < 7)
        {
                i++;
                if (i / 2 == 0)
                    tf = firePos1.transform;
                else
                    tf = firePos2.transform;
                Instantiate(fire, tf);

                yield return new WaitForSeconds(0.3f);
        }
        yield break;
    }
    IEnumerator SpawnBomb()
    {
        float x, z, r= 0.15f;
        float rx, ry, rz, rw;
        while (true)
        {
            x = Random.Range(-0.12f, 0.12f);
            z = Random.Range(-0.12f, 0.12f);

            rx = Random.Range(-1, 1);
            ry = Random.Range(-1, 1);
            rz = Random.Range(-1, 1);
            rw = Random.Range(-1, 1);

            if (x * x + z * z < r * r)
            {
                GameObject bb = Instantiate(dropbomb, bossRoomCenter.transform);
                //GameObject Rock=Instantiate(rock, rockSpawnPoint.transform);
                bb.transform.parent = bossRoomCenter.transform.parent;
                bb.transform.localPosition = new Vector3(x, 0.3f, z);
                bb.transform.localScale = new Vector3(0.007f, 0.007f, 0.007f);
                break;
            }
        }
        yield break;
    }
}
