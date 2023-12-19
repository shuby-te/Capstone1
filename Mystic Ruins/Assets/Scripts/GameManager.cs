using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public FadeEffect fade;

    public GameObject tpPoint;
    public GameObject[] uniqueItems = new GameObject[6];
    public GameObject[] pulleys = new GameObject[4];
    public GameObject[] skillTrigs = new GameObject[3];

    public GameObject player;    
    public GameObject cart;    
    public GameObject pipeController;
    public GameObject itemManager;
    public GameObject engineFire;
    public GameObject engineCoal;

    public GameObject bossS;
    public GameObject mapS;

    public GameObject water;
    public GameObject wave;

    public PlayerSound ps;
    public StartTutorial tuto;
    public MapTrigger mt;

    AssembleCart cartScript;

    private void Start()
    {
        Image image = fade.gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);        

        cartScript = cart.GetComponent<AssembleCart>();        

        DataManager.Instance.LoadGameData();

        if (DataManager.Instance.gameData.currentMapValue == 8)
            DataManager.Instance.gameData.bossSceneLoaded = 1;

        if (DataManager.Instance.gameData.bossSceneLoaded == 1)
        {
            player.transform.position = tpPoint.transform.position;
            bossS.SetActive(true);
            mapS.SetActive(false);
        }
        else
        {
            bossS.SetActive(false);
            mapS.SetActive(true);
            player.transform.position = new Vector3(
                DataManager.Instance.gameData.x,
                DataManager.Instance.gameData.y,
                DataManager.Instance.gameData.z);
        }

        DataManager.Instance.gameData.bossSceneLoaded = 0;

        StartCoroutine(mt.Move(2));

        for (int i = 0; i < skillTrigs.Length; i++)
        {
            if (DataManager.Instance.gameData.skillStates[i] == 1)
            {
                if(i == 0)
                {
                    skillTrigs[i].GetComponent<UnlockFirebreath>().fireTwinkle.SetActive(false);
                    skillTrigs[i].GetComponent<UnlockFirebreath>().enabled = false;
                }
                else
                    skillTrigs[i].SetActive(false);
            }
        }

        if (DataManager.Instance.gameData.currentMapValue == 2)
            ps.stepS = ps.stepSand;
        else
            ps.stepS = ps.stepMetal;

        if (DataManager.Instance.gameData.mapProgress[7] == 0)
            engineCoal.SetActive(false);
        else if (DataManager.Instance.gameData.mapProgress[7] == 1)
            engineCoal.SetActive(true);
        else if (DataManager.Instance.gameData.mapProgress[7] == 2)
            engineFire.SetActive(true);

        cartScript.coalNum = DataManager.Instance.gameData.coalNum;
        cartScript.wheelNum = DataManager.Instance.gameData.wheelNum;

        for (int i = 0; i < cartScript.coalNum; i++)
            cartScript.coals[i].SetActive(true);

        for (int i = 0; i < cartScript.wheelNum; i++)
        {
            cartScript.wheels[i].SetActive(true);
            uniqueItems[i].SetActive(false);
        }

        if(DataManager.Instance.gameData.mapProgress[4] >= 1)
        {
            uniqueItems[2].SetActive(true);
            uniqueItems[3].SetActive(false);
        }

        if (DataManager.Instance.gameData.mapProgress[0] == 1)
        {
            uniqueItems[4].SetActive(true);
            uniqueItems[5].SetActive(false);
        }

        if (DataManager.Instance.gameData.mapProgress[0] == 1)
        {
            tuto.state = 6;
            tuto.isSave = true;
        }

        if (DataManager.Instance.gameData.mapProgress[4] >= 2)
            water.transform.localPosition = new Vector3(
                water.transform.localPosition.x, 4.5f, water.transform.localPosition.z);

        wave.transform.localPosition = new Vector3(wave.transform.localPosition.x, 
            DataManager.Instance.gameData.localWaveY, wave.transform.localPosition.z);

        if (DataManager.Instance.gameData.mapProgress[2] == 1)
            cart.transform.position = new Vector3(
                DataManager.Instance.gameData.cartPos[0],
                DataManager.Instance.gameData.cartPos[1],
                DataManager.Instance.gameData.cartPos[2]);

        if (DataManager.Instance.gameData.mapProgress[5] == 0)
            pipeController.GetComponent<Sh_PipeController>().ResetPipes();
        else
        {
            pipeController.GetComponent<Sh_PipeController>().ClearPipes();
            pipeController.GetComponent<Sh_PipeController>().ClearAct();
        }

        if (DataManager.Instance.gameData.mapProgress[6] == 1)
        {
            for (int i = 0; i < 1; i++)
            {
                if ((DataManager.Instance.gameData.pulleyState[i] == 2))
                    pulleys[i].GetComponent<MovePulley>().ResetClearedState();
            }
        }

        itemManager.GetComponent<ItemManager>().SetItems();

        StartCoroutine(fade.Fade(1));
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
            player.transform.position = new Vector3(-1.13f, 0, -40f);
    }
}
