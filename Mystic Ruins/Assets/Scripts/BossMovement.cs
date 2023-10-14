using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;

    Animator anim;
    bool isNotice;
    bool isAttack;

    float time;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float dir = Vector3.Distance(transform.position, player.transform.position);

        if (dir <= 5)
        {
            Debug.Log("이게 맞네");

            isAttack = true;
            //StartCoroutine("SwordAttack");

            anim.SetInteger("isAttack", 1);

            time += Time.deltaTime;

            if (time >= 0.9f)
            {
                Debug.Log("asef");
                anim.SetInteger("isAttack", 0);
                isAttack = false;
                time = 0f;
            }            
        }
        else if(dir <= 12 && !isAttack)
        {
            Debug.Log("이게 되네");
            isNotice = true;

            transform.position += transform.forward * speed * Time.deltaTime;

            Vector3 t_dir = (player.transform.position - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, t_dir, 10f);
        }
    }

    /*IEnumerator SwordAttack()
    {
        anim.SetInteger("isAttack", 1);

        yield return new WaitForSeconds(1.3f);

        anim.SetInteger("isAttack", 0);

        yield return new WaitForSeconds(0.3f);
        
        isAttack = false;
        yield break;
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(other.gameObject);
    }*/
}
