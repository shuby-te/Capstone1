using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject titleB;
    public GameObject quitB;
    public GameObject pipeController;
    public GameObject script;

    public int mapValue;

    private void Start()
    {
        DataManager.Instance.LoadGameData();
        player.transform.position = new Vector3(
            DataManager.Instance.gameData.x, 
            DataManager.Instance.gameData.y, 
            DataManager.Instance.gameData.z);

        titleB.SetActive(false);
        quitB.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            player.transform.position = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
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

        if (Input.GetKeyDown(KeyCode.I))
            pipeController.GetComponent<Sh_PipeController>().ResetPipes();

        if (Input.GetKeyDown(KeyCode.O))
            pipeController.GetComponent<Sh_PipeController>().ClearPipes();
    }
}
