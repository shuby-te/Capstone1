using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToCartoonScene()
    {
        SceneManager.LoadScene("CartoonScene");
    }
    public void ToEnd()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
