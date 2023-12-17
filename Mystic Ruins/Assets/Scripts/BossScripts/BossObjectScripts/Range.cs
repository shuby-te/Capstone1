using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Range : MonoBehaviour
{
    bool scaleUp;
    // Start is called before the first frame update
    public SwordTypeA sword;
    public IEnumerator RangeScaleUp()
    {
        scaleUp = true;
        while (scaleUp)
        {
            transform.localScale += new Vector3(1f, 0, 0) * Time.deltaTime * sword.speed * 1.5f;
            transform.localPosition -= transform.right * 5 * Time.deltaTime * sword.speed * 1.5f;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    public IEnumerator RangeScaleDown()
    {
        while (transform.localScale.x>=0)
        {
            transform.localScale -= new Vector3(1, 0, 0) * Time.deltaTime * sword.speed;
            transform.localPosition -= transform.right * Time.deltaTime * sword.speed * 5f;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.CompareTag("wall"))
        {
            sword.wait = false;
            scaleUp = false;
            StartCoroutine(RangeScaleDown());
        }
    }
}
