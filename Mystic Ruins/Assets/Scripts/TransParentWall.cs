using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransParentWall : MonoBehaviour
{
    public bool isDetectOut;
    public bool isDetectIn;

    Material material;
    Color originalColor;

    public float fadeSpeed = 0.5f; // 페이드 속도 조절
    public float delayTime = 2.0f; // 타이머 지연 시간

    bool timerStarted = false;
    public float time;

    public void Start()
    {
        time = 0;

        Renderer renderer = GetComponent<Renderer>();

        material = new Material(renderer.material);
        renderer.material = material;

        originalColor = material.color;

        material.SetFloat("_Mode", 2);

        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (!isDetectIn && time > delayTime)
        {
            StartCoroutine(FadeIn());
        }
    }

    public void BecomeTransparent()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        isDetectOut = true;
        float currentAlpha = material.color.a;

        while (currentAlpha > 0.3f)
        {
            currentAlpha = Mathf.Clamp01(currentAlpha - Time.deltaTime / fadeSpeed);

            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha);

            if (!timerStarted && currentAlpha <= 0.3f)
            {
                timerStarted = true;
                isDetectIn = false;                
            }

            yield return null;
        }

        isDetectOut = false;
    }

    public IEnumerator FadeIn()
    {
        isDetectIn = true;
        float currentAlpha = material.color.a;

        while (currentAlpha < 1f)
        {
            /*if (time < delayTime)
                yield break;*/

            currentAlpha = Mathf.Clamp01(currentAlpha + Time.deltaTime / fadeSpeed);

            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha);

            yield return null;
        }

        timerStarted = false;
    }
}
