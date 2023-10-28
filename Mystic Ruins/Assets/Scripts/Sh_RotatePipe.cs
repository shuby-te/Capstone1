using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sh_RotatePipe : MonoBehaviour
{
    public bool isSelect;
    public float time;

    void Update()
    {   
        if(isSelect)
        {
            time += Time.deltaTime;
            if(time < 0.1f && Input.GetMouseButtonDown(0))
                StartCoroutine(Push());
            else if(time >= 0.1f)
                isSelect = false;
        }
    }

    IEnumerator Push()
    {
        float n = 0;

        while (n < 90)
        {
            yield return new WaitForSeconds(0.001f);

            transform.Rotate(0, 0, 1);
            yield return null;
            n += 1;
        }
        isSelect = false;
    }

}
