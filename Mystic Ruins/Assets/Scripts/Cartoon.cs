using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cartoon : MonoBehaviour
{
    public Image[] images; // 이미지 배열, Inspector 창에서 이미지들을 할당
    public float fadeInDuration = 1f; // 서서히 밝아지는 시간
    public GameObject SceneManager;
    private int currentIndex = 0;

    void Start()
    {
        if (images == null || images.Length == 0)
        {
            Debug.LogError("Images not set!");
            return;
        }

        // 초기 이미지 설정 (첫 번째 이미지는 밝게 시작)
        SetImageAlpha(0);

        // 나머지 이미지는 처음에는 투명하게 설정
        for (int i = 1; i < images.Length; i++)
        {
            SetImageAlpha(i, 0);
        }
        ShowNextImage();
    }

    void Update()
    {
        // 예시: 마우스 클릭할 때마다 다음 이미지로 서서히 밝아지게 함
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextImage();
        }
    }

    void ShowNextImage()
    {
        // 현재 이미지는 서서히 투명하게
        //tartCoroutine(FadeImage(currentIndex, 0));

        // 다음 이미지는 서서히 밝아지게
        currentIndex = (currentIndex + 1) % images.Length;
        if (currentIndex == 0)
        {
            SceneManager.GetComponent<ResetGameData>().ResetData();
            SceneManager.GetComponent<ChangeScene>().ToMainScene();
        }
        StartCoroutine(FadeImage(currentIndex, 1));
    }

    IEnumerator FadeImage(int index, float targetAlpha)
    {
        Image targetImage = images[index];
        Color startColor = targetImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            targetImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetImage.color = targetColor;
    }

    void SetImageAlpha(int index, float alpha = 1)
    {
        Color color = images[index].color;
        color.a = alpha;
        images[index].color = color;
    }
}