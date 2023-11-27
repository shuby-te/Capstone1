using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBox : MonoBehaviour
{
    bool isInteract;

    void Update()
    {
        if(isInteract)
        {
            StartCoroutine("Burning");
        }
    }

    IEnumerator Burning()
    {
        //불 타는 애니메이션 혹은 파티클 재생
        yield return new WaitForSeconds(3);     //불에 타 없어지는데 걸리는 시간
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            isInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
            isInteract = false;
    }

}
