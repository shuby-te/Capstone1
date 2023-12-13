using System.Collections;
using UnityEngine;

public class SwordTypeA : MonoBehaviour
{
    public Transform player; 
    public float speed = 5f; 
    public float stopDuration = 2f;
    bool wait = false;
    Vector3 movement;

    Animator anim;

    private void Start()
    {
        anim= GetComponent<Animator>();
        transform.LookAt(player.position);
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
        movement = Vector3.up * Time.deltaTime;
        StartCoroutine(ChasePlayer());
    }

    IEnumerator ChasePlayer()
    {
        while (true)
        {
            transform.Translate(movement);
            yield return new WaitForFixedUpdate();
            if (wait)
                yield return StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        yield return new WaitForSeconds(stopDuration);
        anim.SetFloat("Multiplier", 0);
        transform.LookAt(player.position);
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
        movement = Vector3.up * Time.deltaTime * speed;
        wait = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            wait = true;
            anim.SetFloat("Multiplier", 1);
        }
    }
}
