using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUI : MonoBehaviour
{
    public GameObject itemsUI;
    public GameObject battleUI;

    public float duration;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            StartCoroutine(UISwitching());
        }
    }

    IEnumerator UISwitching()
    {
        Transform openUI = itemsUI.transform;
        Transform closeUI = battleUI.transform;

        if (itemsUI.transform.position.y > battleUI.transform.position.y)
        {
            Transform tmp = openUI;
            openUI = closeUI; closeUI = tmp;
        }

        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            closeUI.position = Vector3.Lerp(closeUI.position, new Vector3(closeUI.position.x, -150f, closeUI.position.z), t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            openUI.position = Vector3.Lerp(openUI.position, new Vector3(openUI.position.x, 0f, openUI.position.z), t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}