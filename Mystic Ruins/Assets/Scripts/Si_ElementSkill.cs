using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Si_ElementSkill : MonoBehaviour
{
    public int time=0;
    public bool isActive = false;
    public bool isSkil = false;
    public int skillNum; // fire = 1 water = 2 metal = 3 
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            isActive = true;
        }
        if (Input.GetKey(KeyCode.Alpha2)&&!isActive&&!isSkil)
        {
            isSkil = true;
            StartCoroutine(on(6));
        }
        if (time > 1000&& !boss.GetComponent<BossMovement>().isStun)
        {
            boss.GetComponent<BossMovement>().isStun=true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            if (isActive && skillNum == 2) 
            {
                //������ ���⿡ �߰�
                if (boss.GetComponent<BossMovement>().attackNum == 9)
                    time++;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        float bossSpeed = boss.GetComponent<Animator>().GetFloat("AttackSpeed");
        if (other.CompareTag("Boss"))
        {
            if (isActive && skillNum == 1)
            {
                if (bossSpeed == 1)
                {
                    boss.GetComponent<Animator>().SetFloat("AttackSpeed", 1.25f);
                    boss.GetComponent<BossMovement>().bossSpeed = 1.25f;
                }
                else
                {
                    boss.GetComponent<Animator>().SetFloat("AttackSpeed", 1.5f);
                    boss.GetComponent<BossMovement>().bossSpeed = 1.5f;
                    StartCoroutine(boss.GetComponent<BossMovement>().OverHeat());
                }
                isActive = false;
            }
        }
        else if (other.CompareTag("Object"))
        {
            if (other.GetComponent<DropBomb>() != null)
            {
                other.GetComponent<DropBomb>().isFire = true;
            }
        }
    }

    IEnumerator wait(float x)
    {
        yield return new WaitForSeconds(x);
    }
    IEnumerator wake()
    {
        yield return new WaitForSeconds(3);
        boss.GetComponent<BossMovement>().isStun = false;
    }
    IEnumerator on(int x)
    {
        yield return StartCoroutine(wait(0.4f));
        isActive = true;
        yield return new WaitForSeconds(x);
        isActive = false;
        time = 0;
        yield return StartCoroutine(wait(1.3f));
        isSkil = false;
    }
}
