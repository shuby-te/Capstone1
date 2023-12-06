using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public bool isFire = false;
    bool isStart = false;
    public GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(isFire&&!isStart)
        {
            StartCoroutine(Timer());
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Boss"))
        {
            if(isFire)
            {
                StartCoroutine(collision.transform.GetComponent<Si_BossMovement>().Stun(3));
            }
            Destroy(gameObject);
        }
    }

    IEnumerator Timer()
    {
        int i = 0;
        float k = timer.transform.eulerAngles.y;
        while (i < 360)
        {
            i++;
            timer.transform.eulerAngles = new Vector3(-90, k++, 0);
            yield return new WaitForSeconds(1 / 120f);
        }
        Destroy(gameObject);
    }
}
