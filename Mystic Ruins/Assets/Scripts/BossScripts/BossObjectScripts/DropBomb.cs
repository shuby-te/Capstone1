using System.Collections;
using UnityEngine;

public class DropBomb : BossObject
{
    public bool isFire = false;
    bool isStart = false;
    public GameObject timer;
    public ParticleSystem[] particle = new ParticleSystem[1];

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        particle[0].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     if(isFire&&!isStart)
        {
            StartCoroutine(Timer());
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("col");
        if (collision.transform.CompareTag("Boss"))
        {
            Debug.Log("bomb");
            if(isFire)
            {
                StartCoroutine(collision.transform.GetComponent<BossMovement>().Stun(3));
                collision.transform.GetComponent<BossMovement>().Objmanager.DropBombInactive();
            }
            Disable();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Boss"))
        {
            Debug.Log("bomb");
            if (isFire)
            {
                StartCoroutine(other.transform.parent.GetComponent<BossMovement>().Stun(3));
                other.transform.parent.GetComponent<BossMovement>().Objmanager.DropBombInactive();
            }
            Disable();
        }
    }
    IEnumerator Drop()
    {
        particle[0].gameObject.SetActive(true);
        particle[0].Play();
        yield return new WaitForSeconds(0.5f);
        while (gameObject.transform.localPosition.y > -43f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.1f, gameObject.transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
        particle[0].gameObject.SetActive(false);
        for (int i = 1; i < 4; i++)
        {
            particle[i].Play();
        }
        StartCoroutine(Del());
    }

    IEnumerator Timer()
    {
        int i = 0;
        float k = timer.transform.eulerAngles.y;
        while (i < 360)
        {
            i++;
            timer.transform.eulerAngles = new Vector3(-90, k++, 0);
            yield return new WaitForSeconds(1 / 120f);
        }
            Disable();
    }

    IEnumerator Del()
    {
        yield return new WaitForSeconds(15);
        Disable();
    }

    new private void OnEnable()
    {
        StartCoroutine(Drop());
    }
    new private void OnDisable()
    {
        base.OnDisable();
        StopCoroutine(Timer());
        isFire = false;
    }
}
