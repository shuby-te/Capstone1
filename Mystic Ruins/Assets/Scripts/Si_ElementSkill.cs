using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Si_ElementSkill : MonoBehaviour
{
    public int time=0;
    public bool isActive = false;
    public bool isSkil = false;

    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2)&&!isActive&&!isSkil)
        {
            isSkil = true;
            StartCoroutine(on(6));
        }
        if (time > 1000&& !boss.GetComponent<Si_BossMovement>().isStun)
        {
            boss.GetComponent<Si_BossMovement>().isStun=true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            if (isActive && gameObject.CompareTag("Water"))
            {
                //������ ���⿡ �߰�
                if (boss.GetComponent<Si_BossMovement>().attackNum == 9)
                    time++;
            }
        }
    }

    private void OnParticleTrigger()
    {
        float bossSpeed = boss.GetComponent<Animator>().GetFloat("AttackSpeed");
        if (bossSpeed == 1)
        {
            boss.GetComponent<Animator>().SetFloat("AttackSpeed", 1.25f);
            boss.GetComponent<Si_BossMovement>().bossSpeed = 1.25f;
        }
        else
        {
            boss.GetComponent<Animator>().SetFloat("AttackSpeed", 1.5f);
            boss.GetComponent<Si_BossMovement>().bossSpeed = 1.5f;
            StartCoroutine(boss.GetComponent<Si_BossMovement>().OverHeat());
        }
    }
    IEnumerator wait(float x)
    {
        yield return new WaitForSeconds(x);
    }
    IEnumerator wake()
    {
        yield return new WaitForSeconds(3);
        boss.GetComponent<Si_BossMovement>().isStun = false;
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
