using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        DataManager.Instance.LoadGameData();
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
        Debug.Log(Application.persistentDataPath);
    }

    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.P))
            Application.Quit();*/

        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("TitleScene");
    }
}
