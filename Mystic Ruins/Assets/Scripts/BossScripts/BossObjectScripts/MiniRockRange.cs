using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class MiniRockRange : MonoBehaviour
{
    public GameObject miniRock;
    bool scaleUp;
    Transform parent;
    private void OnEnable()
    {
        transform.parent = miniRock.transform;
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(0, 0, 0.41f);
        transform.position=new Vector3 (transform.position.x, 0.5f, transform.position.z);
        transform.parent = null;
        StartCoroutine(RangeScaleUp());
    }
    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }

    public IEnumerator RangeScaleUp()
    {
        scaleUp = true;
        while (scaleUp)
        {
            transform.localScale += new Vector3(1f, 0, 0) * 6 * Time.deltaTime;
            transform.localPosition += transform.right * 30 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    public IEnumerator RangeScaleDown()
    {
        yield return new WaitForSeconds(0.5f);
        while (transform.localScale.x >= 0)
        {
            transform.localScale -= new Vector3(1, 0, 0) * 2 * Time.deltaTime;
            transform.localPosition += transform.right * Time.deltaTime * 10f;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.CompareTag("wall"))
        {
            scaleUp = false;
            StartCoroutine(RangeScaleDown());
        }
    }
}
