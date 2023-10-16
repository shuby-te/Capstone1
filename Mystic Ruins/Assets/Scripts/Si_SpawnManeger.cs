using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Si_SpawnManeger : MonoBehaviour
{
    public GameObject dropGearSpawnPoint;
    public GameObject dropGearSpawnRotate;
    public GameObject spreadGearSpawnPoint;
    public GameObject bombSpawnPoint;
    public GameObject dropGear;
    public GameObject bigGear;
    public GameObject miniGear;
    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropGear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpreadGear()
    {
        Instantiate(bigGear, spreadGearSpawnPoint.transform);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 8; i++)
        {
            bigGear.transform.Rotate(Vector3.forward, 45);
            Quaternion gearRot = bigGear.transform.rotation;
            Instantiate(miniGear, spreadGearSpawnPoint.transform.position, gearRot);
        }
        yield break;
    }
    IEnumerator DropGear()
    {
        dropGearSpawnRotate.transform.Rotate(Vector3.up, Random.Range(0,360));
        for (int i = 0; i < 8; i++)
        {
            dropGearSpawnRotate.transform.Rotate(Vector3.up, 45);
            Vector3 pos = dropGearSpawnPoint.transform.position;
            pos.y += 5;
            Instantiate(dropGear, pos, dropGear.transform.rotation);
            yield return new WaitForSeconds(0.05f);
        }
        dropGearSpawnRotate.transform.rotation = new Quaternion(0,0,0,0);
        yield return new WaitForSeconds(2);
        StartCoroutine(DropGear());
    }
}
