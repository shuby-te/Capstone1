using System.Collections;
using UnityEngine;

public class SwordTypeA : MonoBehaviour
{
    public GameObject range;
    public Transform player; 
    public float speed = 5f; 
    public bool wait = false;
    Vector3 movement;

    BoxCollider bc;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider>();
        transform.LookAt(player.position);
        range.transform.LookAt(player.position);
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
        movement = transform.up * speed * 5;
        StartCoroutine(ChasePlayer());
    }

    IEnumerator ChasePlayer()
    {
        while (true)
        {
            movement = transform.up * Time.deltaTime * speed * 20;
            transform.localPosition += movement;
            yield return new WaitForEndOfFrame();
            if (wait)
                yield return StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        yield return new WaitForSeconds(2f);
        transform.LookAt(player.position);
        range.transform.LookAt(player.position);
        range.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        range.transform.localScale = new Vector3(0.1f, 0.5f, 0.3f);
        range.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
        yield return StartCoroutine(range.GetComponent<Range>().RangeScaleUp());
        anim.SetFloat("Multiplier", 0);
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
        bc.enabled = true;
        transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            wait = true;
            anim.SetFloat("Multiplier", 1);
            bc.enabled = false;

        }
    }
}
