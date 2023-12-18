using System.Collections;
using UnityEngine;

public class DropBomb : BossObject
{
    public bool isFire = false;
    bool isStart = false;
    public GameObject timer;
    public GameObject explosionRange;
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
        if (collision.transform.CompareTag("Boss"))
        {
            if(isFire)
            {
                StartCoroutine(collision.transform.GetComponent<BossPhase1>().Stun(3));
                collision.transform.GetComponent<BossPhase1>().om.DropBombInactive();
            }
            Disable();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            if (isFire)
            {
                StartCoroutine(other.transform.parent.GetComponent<BossPhase1>().Stun(3));
                other.transform.parent.GetComponent<BossPhase1>().om.DropBombInactive();
            }
            Disable();
        }
    }
    IEnumerator Drop()
    {
        particle[0].gameObject.SetActive(true);
        particle[0].Play();
        yield return new WaitForSeconds(0.5f);
        while (gameObject.transform.localPosition.y > -43.333f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.1f, gameObject.transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, -43.333f, gameObject.transform.localPosition.z);
        particle[0].gameObject.SetActive(false);

        for (int i = 1; i < 4; i++)
        {
            particle[i].Play();
        }
        StartCoroutine(Del());
    }

    IEnumerator Timer()
    {
        StopCoroutine(Del());
        isStart = true;
        particle[4].gameObject.SetActive(true);
        particle[4].Play();
        int i = 0;
        float k = timer.transform.eulerAngles.y;
        while (i < 360)
        {
            i++;
            timer.transform.eulerAngles = new Vector3(-90, k++, 0);
            yield return new WaitForSeconds(1 / 60f);
        }
        isFire = false;
        isStart = false;

        explosionRange.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        explosionRange.gameObject.SetActive(false);

        Disable();
        yield break;
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
        particle[5].Play();
        base.OnDisable();
        particle[4].Stop();
        particle[4].gameObject.SetActive(false);

        isFire = false;
        //StopCoroutine(Timer());
    }
}
