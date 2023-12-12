using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] uniqueItems = new GameObject[3];
    public GameObject[] pulleys = new GameObject[4];

    public GameObject player;    
    public GameObject cart;    
    public GameObject pipeController;
    public GameObject itemManager;

    AssembleCart cartScript;

    private void Start()
    {
        cartScript = cart.GetComponent<AssembleCart>();

        DataManager.Instance.LoadGameData();
        player.transform.position = new Vector3(
            DataManager.Instance.gameData.x,
            DataManager.Instance.gameData.y,
            DataManager.Instance.gameData.z);

        cartScript.coalNum = DataManager.Instance.gameData.coalNum;
        cartScript.wheelNum = DataManager.Instance.gameData.wheelNum;

        for (int i = 0; i < cartScript.coalNum; i++)
            cartScript.coals[i].SetActive(true);

        for (int i = 0; i < cartScript.wheelNum; i++)
        {
            cartScript.wheels[i].SetActive(true);
            uniqueItems[i].SetActive(false);
        }

        if (DataManager.Instance.gameData.mapProgress[2] == 1)
            cart.transform.position = new Vector3(
                DataManager.Instance.gameData.cartPos[0],
                DataManager.Instance.gameData.cartPos[1],
                DataManager.Instance.gameData.cartPos[2]);

        if (DataManager.Instance.gameData.mapProgress[5] == 0)
            pipeController.GetComponent<Sh_PipeController>().ResetPipes();
        else
            pipeController.GetComponent<Sh_PipeController>().ClearPipes();

        if (DataManager.Instance.gameData.mapProgress[6] == 1)
        {
            for (int i = 0; i < 1; i++)
            {
                if ((DataManager.Instance.gameData.pulleyState[i] == 2))
                    pulleys[i].GetComponent<MovePulley>().ResetClearedState();
            }
        }

        itemManager.GetComponent<ItemManager>().SetItems();
    }

    private void OnApplicationQuit()
    {
        //실행 종료한다고 저장이 되면 안되긴 해
        //DataManager.Instance.SaveGameData();
    }

    void Update()
    {
        if (DataManager.Instance.gameData.mapProgress[2] == 1)
        {
            DataManager.Instance.gameData.cartPos[0] = cart.transform.position.x;
            DataManager.Instance.gameData.cartPos[1] = cart.transform.position.y;
            DataManager.Instance.gameData.cartPos[2] = cart.transform.position.z;
        }        

        //임시로 넣음
        if (Input.GetKeyDown(KeyCode.R))
            player.transform.position = new Vector3(0, 0, 0);

        //임시로 넣음
        if (Input.GetKeyDown(KeyCode.I))
            pipeController.GetComponent<Sh_PipeController>().ResetPipes();
        if (Input.GetKeyDown(KeyCode.O))
            pipeController.GetComponent<Sh_PipeController>().ClearPipes();
    }
}
