using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateShockWave : MonoBehaviour
{
    public TurnWaterValves[] valves = new TurnWaterValves[5];

    bool isStop;
    bool waveOnce;
    bool clear;

    void Update()
    {
        if (valves[1].turnOn || valves[2].turnOn)
        {
            isStop = false;
            if(!waveOnce)
                StartCoroutine(ShockWave());
        }
        else if (valves[0].turnOn && !valves[1].turnOn && !valves[2].turnOn && valves[3].turnOn && valves[4].turnOn)
        {
            isStop = true;
            clear = true;            
        }
        else
        {
            isStop = true;
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
        waveOnce = true;
        while (transform.localPosition.y < 3.67f)
        {
            if(isStop)
            {
                waveOnce = false;
                yield break;
            }

            transform.Translate(Vector3.up * Time.deltaTime);
            DataManager.Instance.gameData.localWaveY = transform.localPosition.y;
            yield return null;
        }

        waveOnce = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(collision.gameObject.GetComponent<PlayerMovement2>().GameOver());
            //transform.localPosition = new Vector3(transform.localPosition.x, - 3.5f, transform.localPosition.z);
            valves[1].turnOn = false;
            valves[2].turnOn = false;
            //isStop = true;
        }
    }
}
