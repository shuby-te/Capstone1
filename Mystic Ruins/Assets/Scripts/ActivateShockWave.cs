using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShockWave : MonoBehaviour
{
    public TurnWaterValves[] valves = new TurnWaterValves[5];

    bool isStop;
    bool clear;

    void Update()
    {
        if (valves[1].turnOn || valves[2].turnOn)
        {
            isStop = true;
            StartCoroutine(ShockWave());
        }
        else if (valves[0].turnOn && !valves[1].turnOn && !valves[2].turnOn && valves[3].turnOn && valves[4].turnOn)
        {
            isStop = false;
            clear = true;            
        }
        else
        {
            isStop = false;
        }

        if(clear && DataManager.Instance.gameData.mapProgress[4] == 2)
        {
            DataManager.Instance.gameData.mapProgress[4] = 3;
            for (int i = 0; i < valves.Length; i++)
            {
                valves[i].enabled = false;
            }
        }
    }

    IEnumerator ShockWave()
    {
        while (transform.localPosition.y < 3.67f)
        {
            if(isStop)
                yield break;

            transform.Translate(Vector3.up * Time.deltaTime * 1.5f);
            yield return null;
        }
    }
}
