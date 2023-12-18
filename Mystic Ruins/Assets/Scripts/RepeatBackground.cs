using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RepeatBackground : MonoBehaviour
{
    public GameObject titleB;
    public FadeEffect fade;


    public float spawnTime;
    public float endX;
    public float startX;
    public bool isTitle;

    TextMeshProUGUI titleT;

    private void Start()
    {
        if(isTitle)
        {
            StartCoroutine(fade.Fade(1));
            
            titleT = titleB.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            StartCoroutine(toTitle());                      
        }        
    }

    void Update()
    {
        if (transform.position.x < endX)
        {
            transform.position = new Vector3(startX - 0.1f, 0, 0);
        }
    }

    IEnumerator toTitle()
    {
        yield return new WaitForSeconds(spawnTime);
        Color color = titleT.color;

        float time = 0f;

        Color targetColor = new Color(color.r, color.g, color.b, 1f);

        while (time < 1f)
        {
            titleT.color = Color.Lerp(color, targetColor, time);
            time += Time.deltaTime;
            yield return null;
        }

        titleB.GetComponent<Button>().enabled = true;
    }
}
