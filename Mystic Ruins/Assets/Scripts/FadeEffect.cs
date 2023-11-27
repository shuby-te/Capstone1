using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    float fadeTime;

    [SerializeField]
    AnimationCurve fadeCurve;

    Image image;
    
    void Start()
    {
        image=GetComponent<Image>();
    }

    public void Fade(bool state)    //true¸é fade in false¸é fade out
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

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start,end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }
    }
}
