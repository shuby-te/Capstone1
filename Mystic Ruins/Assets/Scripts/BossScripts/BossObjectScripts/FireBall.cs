using System;
using System.Collections;
using UnityEngine;

public class FireBall : BossObject
{

    private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    protected float timer;
    protected float timeToFloor;
    public float t;

    public ParticleSystem[] particle = new ParticleSystem[4];
    // Start is called before the first frame update
    new void Start()
    {
        setParent = true;
        parent = transform.parent;
        pos = Vector3.zero;
        rot = Vector3.zero;
        gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0.3) 
        {
            if (gameObject.GetComponent<MeshRenderer>().enabled)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                t = particle[2].main.startLifetime.constantMax;
                for (int i = 0; i < particle.Length; i++)
                {
                    particle[i].Play();
                }
                StartCoroutine(disable(t + 1));
            }
        }
    }
    new private void OnEnable()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        base.OnEnable();
        launch();
    }
    new private void OnDisable()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        base.OnDisable();
    }
    IEnumerator disable(float i)
    {
        yield return new WaitForSeconds(i);
        gameObject.SetActive(false);
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
        while (transform.position.y >= 0.2f)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 30, timer/2);
            transform.localPosition = tempPos;
            yield return new WaitForEndOfFrame();
        }
    }
    public void launch()
    {
        float x=0, z=0;
        while (true)
        {
            for (int i = 0; i < UnityEngine.Random.Range(1, 25); i++)
            {
                x = UnityEngine.Random.Range(-35f, 35f);
                z = UnityEngine.Random.Range(345f, 415f);
            }
            if (x * x + (z - 380) * (z - 380) < 35 * 35)
                break;
        }
        startPos = transform.position;
        endPos = new Vector3(x, 0.2f, z);

        StartCoroutine("Move");
    }
}
