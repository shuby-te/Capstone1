using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartTutorial : MonoBehaviour
{
    public Image tutoMsg;
    public GameObject player;
    public GameObject boardWall;
    public TextMeshProUGUI msgText;

    public GameObject boardTwinkle;
    public GameObject saveTwinkle;

    public float msgPrintTime;

    public bool isUse;
    public bool isSave;
    
    public int state;

    bool[] alphas = new bool[4];
    float yAngle;

    

    void Start()
    {        
        yAngle = player.transform.rotation.eulerAngles.y;
        SetTutoText(0);
        StartCoroutine(FadeTutoText());
    }

    void Update()
    {
        switch(state)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.W)) alphas[0] = true;
                if (Input.GetKeyDown(KeyCode.S)) alphas[1] = true;
                if (Input.GetKeyDown(KeyCode.A)) alphas[2] = true;
                if (Input.GetKeyDown(KeyCode.D)) alphas[3] = true;

                if (alphas[0] && alphas[1] && alphas[2] && alphas[3])
                {                    
                    state = 2;
                    SetTutoText(state - 1);
                    StartCoroutine(FadeTutoText());
                }
                break;

            case 2:
                if (player.transform.rotation.eulerAngles.y != yAngle)
                {
                    state = 3;
                    SetTutoText(state - 1);
                    StartCoroutine(FadeTutoText());                    
                }
                break;

            case 3:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    state = 4;
                    SetTutoText(state - 1);
                    StartCoroutine(FadeTutoText());                    
                }
                break;

            case 4:
                if (Input.GetMouseButtonDown(0))
                {
                    state = 5;
                    SetTutoText(state - 1);
                    StartCoroutine(FadeTutoText());
                    boardTwinkle.SetActive(true);
                }
                break;

            case 5:
                if(isUse)
                {
                    state = 6;
                    boardWall.SetActive(false);
                    SetTutoText(state - 1);
                    StartCoroutine(FadeTutoText());
                    saveTwinkle.SetActive(true);
                }
                break;

            case 6:
                if (isSave)
                {
                    boardWall.SetActive(false);
                    saveTwinkle.SetActive(false);
                    tutoMsg.gameObject.SetActive(false);
                    msgText.gameObject.SetActive(false);
                    state = 7;
                    this.gameObject.SetActive(false);
                }
                break;
        }
    }

    string[] messages = new string[6] {
        "W,A,S,D를 눌러 이동",
        "마우스 회전으로 방향 조절",
        "LeftShift를 눌러 구르기",
        "마우스 좌클릭으로 공격하기",
        "F를 눌러 아이템 획득/사용",
        "E를 눌러 진행 사항 저장"
    };

    void SetTutoText(int i)
    {
        msgText.text = messages[i];
    }

    IEnumerator FadeTutoText()
    {
        Color color = msgText.color;
        Color color2 = tutoMsg.color;

        /*msgText.color = new Color(color.r, color.g, color.b, 0f);
        tutoMsg.color = new Color(color2.r, color2.g, color2.b, 0f);

        yield return new WaitForSeconds(3f);*/

        float time = 0f;

        Color targetColor = new Color(color.r, color.g, color.b, 1f);
        Color targetColor2 = new Color(color2.r, color2.g, color2.b, 1f);

        while (time < 1f)
        {
            msgText.color = Color.Lerp(color, targetColor, time);
            tutoMsg.color = Color.Lerp(color2, targetColor2, time);
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(msgPrintTime);

        time = 0f;
        color = msgText.color;
        color2 = tutoMsg.color;
        targetColor = new Color(color.r, color.g, color.b, 0f);
        targetColor2 = new Color(color2.r, color2.g, color2.b, 0f);

        while (time < 1f)
        {
            msgText.color = Color.Lerp(color, targetColor, time);
            tutoMsg.color = Color.Lerp(color2, targetColor2, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            state = 1;
        }
    }
}
