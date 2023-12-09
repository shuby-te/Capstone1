using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject timer;
    public GameObject bombPos;
    public ParticleSystem[] particle = new ParticleSystem[3];
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.eulerAngles=new Vector3(0,270,0);
        timer.transform.eulerAngles = new Vector3(180, 0, 0);
        gameObject.SetActive(true);
        gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            particle[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = bombPos.transform.position;
    }

    IEnumerator Timer()
    {
        int i = 0;
        float k = timer.transform.eulerAngles.y;
        while (i<360)
        {
            i += 2;
            timer.transform.eulerAngles = new Vector3(180,0,k++);
            k++;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(Timer());
    }
    private void OnDisable()
    {
        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].Play();
        }
    }

}
    