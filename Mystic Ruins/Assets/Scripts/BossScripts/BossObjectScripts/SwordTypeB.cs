using System.Collections;
using UnityEngine;

public class SwordTypeB : MonoBehaviour
{
    public GameObject player;
    public float delay = 10;
    public float radius = 5f;
    Animator anim;
    Vector3 rot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    IEnumerator Move()
    {
        while (true)
        {
            float randomAngle = Random.Range(0f, 360f);
            float radians = randomAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(radians), 0f, Mathf.Sin(radians)) * radius;
            transform.position = player.transform.position + offset;
            transform.eulerAngles = Vector3.zero;
            transform.LookAt(player.transform.position);
            transform.position += new Vector3(0, 1.5f, 0);
            if (anim != null)
                anim.SetInteger("isAttack", 1);
            yield return new WaitForSeconds(0.1f);
            if (anim != null)
                anim.SetInteger("isAttack", 0);
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        StopCoroutine(Move());
    }

    void ColliderOn()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    void ColliderOff()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}