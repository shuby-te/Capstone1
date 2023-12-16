using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    static DataManager instance;
    public static DataManager Instance {
        get {
            if(!instance) {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    string GameDataFileName = "GameData.json";
    public GameData gameData = new GameData();

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        if(File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            Debug.Log("불러오기!!!");
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장???");
    }

    public void ResetGameData()
    {
        gameData.x = -1.3f;
        gameData.y = 0;
        gameData.z = -40f;

        gameData.coalNum = 0;
        gameData.wheelNum = 0;

        gameData.localWaveY = -3.5f;

        Array.Fill(gameData.items, 0);
        Array.Fill(gameData.pulleyState, 0);

        gameData.currentMapValue = 0;
        Array.Fill(gameData.mapProgress, 0);        

        SaveGameData();
    }
}
