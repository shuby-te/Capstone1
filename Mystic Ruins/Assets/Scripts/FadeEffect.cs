using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    float fadeTime;

    [SerializeField]
    AnimationCurve fadeCurve;

    public Image image;

    public IEnumerator Fade(int state)
    {
        if (state == 1)
            yield return StartCoroutine(FadeEff(1, 0)); //fade in
        else
            yield return StartCoroutine(FadeEff(0, 1)); //fade out

        yield return null;
    }

    private IEnumerator FadeEff(float start, float end)
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
