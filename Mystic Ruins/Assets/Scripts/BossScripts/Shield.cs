using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject boss;
    BossPhase1 bm;
    [SerializeField]
    GameObject effect;
    public int shieldHp = 3;
    private void Start()
    {
        boss = GameObject.Find("Boss");
        bm=boss.GetComponent<BossPhase1>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Object"))
        {
            if (--shieldHp == 0)
                StartCoroutine(Effect());
            other.gameObject.SetActive(false);
        }
        else if(other.CompareTag("BossAttackRange") && bm.attackNum==10 )
        {
            StartCoroutine(bm.Stun(3/bm.speed));
            StartCoroutine(Effect());
        }
        else if (other.CompareTag("BossAttackRange"))
            StartCoroutine(Effect());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Boss"))
        {
            StartCoroutine(Effect());
        }
    }
    IEnumerator Effect()
    {
        effect.gameObject.SetActive(true);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
