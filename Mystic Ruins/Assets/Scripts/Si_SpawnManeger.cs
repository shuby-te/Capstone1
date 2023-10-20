using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Si_SpawnManeger : MonoBehaviour
{
    public GameObject GearSpawnPoint;
    public GameObject GearSpawnRotate;
    public GameObject spreadGearSpawnPoint;
    public GameObject bombSpawnPoint;
    public GameObject dropGear;
    public GameObject bigGear;
    public GameObject miniGear;
    public GameObject bomb;

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
    public void attack3()
    {
        StartCoroutine(Bomb());
    }
    public void attack4()
    {
        StartCoroutine(DropGear());
    }
    IEnumerator SpreadGear()
    {
        Vector3 pos = GearSpawnPoint.transform.position;
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
        GearSpawnRotate.transform.Rotate(Vector3.up, Random.Range(0,360));
        for (int i = 0; i < 8; i++)
        {
            GearSpawnRotate.transform.Rotate(Vector3.up, 45);
            Vector3 pos = GearSpawnPoint.transform.position;
            pos.y += 5;
            Instantiate(dropGear, pos, dropGear.transform.rotation);
            yield return new WaitForSeconds(0.05f);
        }
        GearSpawnRotate.transform.rotation = new Quaternion(0,0,0,0);
    }
    IEnumerator Bomb()
    {
        GameObject Bomb = Instantiate(bomb, bombSpawnPoint.transform);
        yield return null;
    }

}
