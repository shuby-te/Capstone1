using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject boss;
    BossMovement bm;
    public int shieldHp = 3;
    private void Start()
    {
        boss = GameObject.Find("Boss");
        bm=boss.GetComponent<BossMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Object"))
        {
            if (--shieldHp == 0)
                Destroy(gameObject);
            other.gameObject.SetActive(false);
        }
        else if(other.CompareTag("Boss") && bm.attackNum==10 )
        {
            StartCoroutine(bm.Stun(3/bm.speed));
        }
    }
}
