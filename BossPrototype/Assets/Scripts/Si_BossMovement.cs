using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Si_BossMovement : MonoBehaviour
{
    public GameObject gear;
    public GameObject miniGear;
    public GameObject bomb;

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
        BossAttackType1();
        BossAttackType2();
    }

    void Update()
    {
        float dir = Vector3.Distance(transform.position, player.transform.position);
        if (!isAttack && dir < 8)
        {

            isAttack = true;
            StartCoroutine(TurnHead());

        }
        if (dir >= 8 && !isAttack)
        {
            anim.SetInteger("isAttack", 2);
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            Vector3 t_dir = (playerPos - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.1f);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
    IEnumerator TurnHead()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        anim.SetInteger("isAttack", 2);
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

    void BossAttackType1()
    {
        StartCoroutine(GearAttack());
    }
    IEnumerator GearAttack()
    {
        Vector3 gearPos = transform.localPosition;
        gearPos.x += 3;
        gearPos.y += 3;
        Instantiate(gear, gearPos, gear.transform.rotation);
        yield return new WaitForSeconds(3);

        for (int i = 1; i < 9; i++)
        {
            gear.transform.Rotate(Vector3.up, 45);
            Quaternion gearRot = gear.transform.rotation;
            Instantiate(miniGear, gearPos, gearRot);
        }
    }

    void BossAttackType2()
    {
        StartCoroutine(BombTimer());
    }

    IEnumerator BombTimer()
    {
        GameObject Bomb = Instantiate(bomb);
        Bomb.transform.parent = player.transform;
        Bomb.transform.position = new Vector3(0, 3, 0);
        Renderer renderer = Bomb.GetComponent<Renderer>();
        Color targetColora = Color.blue;
        Color targetColorb = Color.magenta;
        Color targetColorc = Color.red;

        renderer.material.color = targetColora;

        yield return new WaitForSeconds(1);
        renderer.material.color = targetColorb;

        yield return new WaitForSeconds(1);
        renderer.material.color = targetColorc;

        yield return new WaitForSeconds(1);
        Destroy(Bomb);
        yield break;
    }
}
