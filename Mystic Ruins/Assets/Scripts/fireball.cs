using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        launch();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    protected float timer;
    protected float timeToFloor;


    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    protected IEnumerator BulletMove()
    {
        timer = 0;
        while (transform.position.y >= startPos.y)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 25, timer/2);
            transform.position = tempPos;
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
                x = UnityEngine.Random.Range(-25f, 25f);
                z = UnityEngine.Random.Range(-25f, 25f);
            }
            if (x * x + z * z < 50 * 50)
                break;
        }
        startPos = transform.position;
        endPos = new Vector3(x-214.3f, 7, 257-z);
        StartCoroutine("BulletMove");
    }
}
