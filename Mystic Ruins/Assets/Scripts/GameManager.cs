using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject titleB;
    public GameObject quitB;
    public GameObject pipeController;
    public GameObject script;

    public float hintPrintTime;

    Dictionary<int, string> hintData = new Dictionary<int, string>();

    private void Start()
    {
        DataManager.Instance.LoadGameData();
        player.transform.position = new Vector3(
            DataManager.Instance.gameData.x,
            DataManager.Instance.gameData.y,
            DataManager.Instance.gameData.z);

        if (DataManager.Instance.gameData.mapProgress[5] == 0)
            pipeController.GetComponent<Sh_PipeController>().ResetPipes();
        else
            pipeController.GetComponent<Sh_PipeController>().ClearPipes();

        if (DataManager.Instance.gameData.currentMapValue == 8)
            FadeHintText();

        titleB.SetActive(false);
        quitB.SetActive(false);

        setHint();
    }

    private void OnApplicationQuit()
    {
        //실행 종료한다고 저장이 되면 안되긴 해
        //DataManager.Instance.SaveGameData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            player.transform.position = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
            PrintMenu();

        if (Input.GetKeyDown(KeyCode.I))
            pipeController.GetComponent<Sh_PipeController>().ResetPipes();
        if (Input.GetKeyDown(KeyCode.O))
            pipeController.GetComponent<Sh_PipeController>().ClearPipes();
    }

    public void PrintHint()
    {
        int key = DataManager.Instance.gameData.currentMapValue * 10 + 
            DataManager.Instance.gameData.mapProgress[DataManager.Instance.gameData.currentMapValue];

        script.GetComponent<TextMeshProUGUI>().text = hintData[key];

        StartCoroutine(FadeHintText());
    }

    IEnumerator FadeHintText()
    {
        TextMeshProUGUI hintText = script.GetComponent<TextMeshProUGUI>();
         
        float time = 0f;
        Color color = hintText.color;
        Color targetColor = new Color(color.r, color.g, color.b, 1f);

        while (time < 1f)
        {
            hintText.color = Color.Lerp(color, targetColor, time);
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(hintPrintTime);

        time = 0f;
        color = hintText.color;
        targetColor = new Color(color.r, color.g, color.b, 0f);

        while (time < 1f)
        {
            hintText.color = Color.Lerp(color, targetColor, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    void setHint()
    {
        hintData.Add(0, "q");
        hintData.Add(10, "q");
        hintData.Add(20, "q");
        hintData.Add(21, "q");
        hintData.Add(30, "q");
        hintData.Add(31, "q");
        hintData.Add(40, "q");
        hintData.Add(41, "q");
        hintData.Add(50, "q");
        hintData.Add(51, "q");
        hintData.Add(60, "q");
        hintData.Add(61, "q");
        hintData.Add(70, "q");
        hintData.Add(71, "q");
        hintData.Add(72, "q");
        hintData.Add(80, "q");
        hintData.Add(81, "q");
        hintData.Add(82, "q");
    }

    public void PrintMenu()
    {
        if (titleB.activeSelf && quitB.activeSelf)
        {
            titleB.SetActive(false);
            quitB.SetActive(false);
        }
        else
        {
            titleB.SetActive(true);
            quitB.SetActive(true);
        }
    }
}
