using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;
    [SerializeField]
    private AnimationCurve fadeCurve;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image=GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fade(bool state)//true¸é fade in false¸é fade out
    {
        if (state)
            StartCoroutine(Fade(1, 0)); 
        else
            StartCoroutine(Fade(0, 1));
    }
    private IEnumerator Fade(float start,float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent<1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a=Mathf.Lerp(start,end, fadeCurve.Evaluate(percent));
            image.color=color;

            yield return null;
        }
    }
}
