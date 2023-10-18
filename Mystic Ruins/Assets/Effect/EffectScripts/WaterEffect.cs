using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterEffect : MonoBehaviour
{
    public float Maxtime = 1;
    public GameObject objA;
    public GameObject objB;
    private Renderer RenA;
    private Renderer RenB;

    // Start is called before the first frame update
    void Start()
    {
        RenA = objA.GetComponent<Renderer>();
        RenB = objB.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(func());
        }
    }

    IEnumerator func()
    {
        float TimaA = 0;
        while (true)
        {
            Debug.Log(TimaA);
            yield return new WaitForSeconds(0.1f);
            RenA.material.SetFloat("_MainTime", TimaA); //이때 들어가는 이름은 NAME이 아닌 Reference에있는 이름을 사용하여야 한다.
            RenB.material.SetFloat("_MainTime", TimaA);
            TimaA += 0.1f;
            if (TimaA > Maxtime) { yield break; }
        }
    }
}
