using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float lerpSpeed;

    Animator anim;
    bool isNotice = false;
    bool isAttack = false;

    float time;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float dir = Vector3.Distance(transform.position, player.transform.position);
        if (!isAttack && dir < 8)
        {

            isAttack = true;
            StartCoroutine(TurnHead());

        }
        if (dir <= 12 && !isAttack)
        {

            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            Vector3 t_dir = (playerPos - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.1f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
    IEnumerator TurnHead()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        for (float i = 0; i < 1; i += 0.1f)
        {
            Vector3 t_dir = (playerPos - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, t_dir, i);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(SwordAttack());
        yield break;
    }

    IEnumerator SwordAttack()
    {
        anim.SetInteger("isAttack", 1);

        yield return new WaitForSeconds(1.3f);

        anim.SetInteger("isAttack", 0);

        yield return new WaitForSeconds(0.3f);

        isAttack = false;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //    Destroy(other.gameObject);
    }
}
