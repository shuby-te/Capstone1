using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    GameObject boss;
    GameObject pos;
    public GameObject timer;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        pos = GameObject.Find("BombSpawnPoint");
        //Destroy(gameObject, 3f);
        gameObject.transform.eulerAngles=new Vector3(0,270,0);
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position= pos.transform.position;
    }
    IEnumerator boom()
    {
        yield return new WaitForSeconds(3);
        boss.GetComponent<Si_BossMovement>().boomActive = false;
        Destroy(gameObject);
    }
    IEnumerator Timer()
    {
        int i = 0;
        float k = timer.transform.eulerAngles.y;
        while (i<360)
        {
            i++;
            timer.transform.eulerAngles = new Vector3(180,0,k++);
            yield return new WaitForSeconds(1 / 120f);
        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        boss.GetComponent<Si_BossMovement>().boomActive = false;

    }
}
    