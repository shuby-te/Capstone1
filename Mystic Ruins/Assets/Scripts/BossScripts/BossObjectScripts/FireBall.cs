using System;
using System.Collections;
using UnityEngine;

public class FireBall : BossObject
{

    private Vector3 startPos, endPos;
    //���� �ݱ���� �ɸ��� �ð�
    protected float timer;
    protected float timeToFloor;
    public float t;
    public GameObject rock;
    public GameObject fireRange;
    public ParticleSystem[] particle = new ParticleSystem[4];
    bool isActive = true;
    // Start is called before the first frame update
    new void Start()
    {
        particle[0].Stop();
        setParent = true;
        parent = transform.parent;
        particle[0].transform.parent = null;
        pos = Vector3.zero;
        rot = Vector3.zero;
        gameObject.SetActive(true);
        Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.53 && isActive)
        {
            isActive = false;
            rock.SetActive(false);
            for (int i = 1; i < particle.Length; i++)
                particle[i].Play();
            StartCoroutine(disable(t + 1));
        }
    }
    new private void OnEnable()
    {
        //transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        rock.SetActive(true);
        base.OnEnable();
        launch();
        isActive = true;
    }
    new private void OnDisable()
    {
        base.OnDisable();
        transform.GetComponent<BoxCollider>().enabled = true;
        fireRange.gameObject.SetActive(false);
    }
    IEnumerator disable(float i)
    {
        yield return new WaitForSeconds(i);
        Disable();
    }
    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    protected IEnumerator Move()
    {
        timer = 0;
        while (transform.position.y >= 0.5f)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 30, timer/2);
            transform.localPosition = tempPos;
            yield return new WaitForEndOfFrame();
        }
        fireRange.gameObject.SetActive(true);
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.position = new Vector3(transform.position.x,0.5f, transform.position.z);
    }
    public void launch()
    {
        float x=0, z=0;
        while (true)
        {
            for (int i = 0; i < UnityEngine.Random.Range(1, 25); i++)
            {
                x = UnityEngine.Random.Range(-40f, 40f);
                z = UnityEngine.Random.Range(320f, 400f);
            }
            if (x * x + (z - 365) * (z - 365) < 35 * 35)
                break;
        }
        startPos = transform.position;
        endPos = new Vector3(x, 0.5f, z);
        particle[0].gameObject.transform.position = new Vector3(endPos.x, 0.5f, endPos.z);
        particle[0].Play();
        StartCoroutine("Move");
    }
}
