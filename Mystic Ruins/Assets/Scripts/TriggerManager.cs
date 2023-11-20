using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public GameObject attackTrigger;
    public float deal;
    // Start is called before the first frame update
    void Start()
    {
        //attackTrigger = GameObject.Find("AttackTrigger");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void disable()
    {
        attackTrigger.SetActive(false);
    }
    void p1()
    {
        attackTrigger.SetActive(true);
        deal = 5; //이렇게 보스 데미지 넣으면 될듯 ?
        attackTrigger.transform.localPosition = new Vector3(0, 0, 3);
        attackTrigger.transform.localScale = new Vector3(5, 1, 5);
    }
    void p5()
    {
        attackTrigger.SetActive(true);
        attackTrigger.transform.localPosition = new Vector3(0, 0, 1);
        attackTrigger.transform.localScale = new Vector3(3, 1, 3);
    }
    void p6_1()
    {
        attackTrigger.SetActive(true);
        attackTrigger.transform.rotation = Quaternion.Euler(0, 120, 0);
        attackTrigger.transform.localPosition = new Vector3(1, 0, -3);
        attackTrigger.transform.localScale = new Vector3(3, 1, 6);
    }
    void p6_2()
    {
        attackTrigger.SetActive(true);
        attackTrigger.transform.rotation = Quaternion.Euler(0, -120, 0);
        attackTrigger.transform.localPosition = new Vector3(2, 0, 3);
        attackTrigger.transform.localScale = new Vector3(3, 1, 6);
    }
    void p7()
    {
        attackTrigger.SetActive(true);
        attackTrigger.transform.localPosition = new Vector3(0, 0, 0);
        attackTrigger.transform.localScale = new Vector3(12, 2, 12);
    }
    void p10()
    {
        attackTrigger.SetActive(true);
        attackTrigger.transform.localPosition = new Vector3(0, 4, 3);
        attackTrigger.transform.localScale = new Vector3(5, 8, 1);
    }
}
