using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteWater : MonoBehaviour
{
    public float maxTime = 1;
    public float waitTime = 0.1f;
    public GameObject[] objA;
    public string ReperenceName;

    Renderer[] RenA;
    
    void Start()
    {
        RenA = new Renderer[objA.Length];
        for (int i = 0; i < objA.Length; i++)
        {
            RenA[i]= objA[i].GetComponent<Renderer>();
        }        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(func());
        }
    }

    IEnumerator func()
    {
        float time = 0;
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            for (int i = 0;i < objA.Length;i++)
            {
                RenA[i].material.SetFloat(ReperenceName, time);
            }
            time += waitTime;
            if (time > maxTime) { yield break; }
        }
    }
}
