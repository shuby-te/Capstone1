using System.Collections;
using UnityEngine;

public class PlayerAttacked : MonoBehaviour
{
    public Sh_HpManager hm;
    public bool isLie = false;
    bool fireDmg = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossAttackRange" || other.tag == "Object") 
        {
            if (other.tag == "BossAttackRange")
                hm.AttackToPlayer(0);
            else if (other.tag == "Object")
                hm.AttackToPlayer(1);
            if (!isLie)
            {
                transform.GetComponent<PlayerMovement2>().isKnockback = true;
                transform.GetComponent<Animator>().SetBool("isKnockback", true);
            }
            hm.ChangeDamageImmune(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fire" && fireDmg && other.GetComponent<Si_ElementSkill>() == null)
        {
            hm.AttackToPlayer(2);
            StartCoroutine(ChangeFireDamageImmuneState());
        }
    }

    IEnumerator ChangeFireDamageImmuneState()
    {
        fireDmg = false;
        hm.ChangeFireDamageImmune(true);
        yield return new WaitForSeconds(0.5f);
        hm.ChangeFireDamageImmune(false);
        fireDmg = true;
    }
}
