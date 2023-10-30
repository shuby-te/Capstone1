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
    public GameObject rockSpawnPoint;
    public GameObject dropGear;
    public GameObject bigGear;
    public GameObject miniGear;
    public GameObject bomb;
    public GameObject rock;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public void attack2()
    {
        StartCoroutine(SpreadGear());
    }
    public void attack3(GameObject obj)
    {
        StartCoroutine(Bomb(obj));
    }
    public void attack4()
    {
        StartCoroutine(DropGear());
    }
    public void attack5(int count, float r)
    {
        StartCoroutine(SpawnRock(count, r));
    }
    IEnumerator SpreadGear()
    {
        Vector3 pos = gearSpawnPoint.transform.position;
        Instantiate(bigGear, pos, Quaternion.identity);
        yield return new WaitForSeconds(3.5f);
        for (int i = 0; i < 8; i++)
        {
            bigGear.transform.Rotate(Vector3.forward, 45);
            Quaternion gearRot = bigGear.transform.rotation;
            Instantiate(miniGear, pos, gearRot);
        }
        yield break;
    }
    IEnumerator DropGear()
    {
        GameObject[] gear = new GameObject[9];
        gearSpawnRotate.transform.Rotate(Vector3.up, Random.Range(0,360));
        for (int i = 0; i < 8; i++)
        {
            gearSpawnRotate.transform.Rotate(Vector3.up, 45);
            Vector3 pos = gearSpawnPoint.transform.position;
            pos.y += 4;
            gear[i]=Instantiate(dropGear, pos, dropGear.transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 8; i++)
            gear[i].GetComponent<Si_Obj>().isDrop = true;
        gearSpawnRotate.transform.rotation = new Quaternion(0,0,0,0);
    }

    IEnumerator Bomb(GameObject obj)
    {
        GameObject Bomb = Instantiate(bomb, bombSpawnPoint.transform);
        obj.GetComponent<Si_BossMovement>().boomActive = false;
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
                GameObject Rock = Instantiate(rock, rockSpawnPoint.transform.localPosition, new Quaternion(rx, ry, rz, rw));
                //GameObject Rock=Instantiate(rock, rockSpawnPoint.transform);
                Rock.transform.parent = rockSpawnPoint.transform.parent;
                Rock.transform.localPosition = new Vector3(x, 0.3f, z);
                Rock.transform.localScale = new Vector3(0.007f, 0.007f, 0.007f);
            }   
        }
        yield break;
    }
}
