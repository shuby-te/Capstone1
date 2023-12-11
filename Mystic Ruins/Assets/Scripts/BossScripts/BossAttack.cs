using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject attackRange;
    public GameObject hpManager;
    public GameObject dropRange;
    float damage;
    int count = 0;
    bool isOverlapped;

    void Attack()
    {
        if (isOverlapped)
        {
            hpManager.GetComponent<Sh_HpManager>().AttackToPlayer();

            player.GetComponent<PlayerAnim>().KnockBacked();
        }
    }   
    
    void Disable()
    {
        attackRange.SetActive(false);
    }
    public void Disable1()
    {
        dropRange.GetComponent<MeshCollider>().enabled = false;
        dropRange.SetActive(false);
    }
    void on()
    {
        gameObject.transform.position=player.transform.position;
        dropRange.SetActive(true);
    }
    void p1()
    {
        attackRange.SetActive(true);
        damage = 5; //�̷��� ���� ������ ������ �ɵ� ?
        attackRange.transform.localPosition = new Vector3(0, 0, 3);
        attackRange.transform.localScale = new Vector3(5, 1, 5);
    }

    void p5()
    {
        attackRange.SetActive(true);
        attackRange.transform.localPosition = new Vector3(0, 0, 1);
        attackRange.transform.localScale = new Vector3(3, 1, 3);
    }

    void p6_1()
    {
        attackRange.SetActive(true);
        attackRange.transform.rotation = Quaternion.Euler(0, 120, 0);
        attackRange.transform.localPosition = new Vector3(1, 0, -3);
        attackRange.transform.localScale = new Vector3(3, 1, 6);
    }

    void p6_2()
    {
        attackRange.SetActive(true);
        attackRange.transform.rotation = Quaternion.Euler(0, -120, 0);
        attackRange.transform.localPosition = new Vector3(2, 0, 3);
        attackRange.transform.localScale = new Vector3(3, 1, 6);
    }

    void p7()
    {
        attackRange.SetActive(true);
        attackRange.transform.localPosition = new Vector3(0, 0, 0);
        attackRange.transform.localScale = new Vector3(12, 2, 12);
    }

    void p10()
    {
        attackRange.SetActive(true);
        attackRange.transform.localPosition = new Vector3(0, 4, 3);
        attackRange.transform.localScale = new Vector3(5, 8, 1);
    }
    void sp1()
    {
        dropRange.GetComponent<MeshCollider>().enabled = true;
    }

    void Stun()
    {
        gameObject.GetComponent<Animator>().SetFloat("StunMultiplier", 0);
    }

    void Rock()
    {
        gameObject.GetComponent<BossMovement>().Objmanager.DropRockActive(5, 0.15f);
        if (count==2)
        {
            gameObject.GetComponent<BossMovement>().Objmanager.DropBombActive();
            count = 0; 
        }
        count++;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isOverlapped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isOverlapped = false;
    }
}
