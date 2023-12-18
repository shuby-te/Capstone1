using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockPulleyWall : MonoBehaviour
{
    public GameObject[] locks = new GameObject[8];

    int state;

    void Start()
    {
        state = 0;
    }

    void Update()
    {
        if (DataManager.Instance.gameData.mapProgress[7] == 2)
        {
            if (state == 0 && DataManager.Instance.gameData.pulleyState[0] >= 1)
            {
                StartCoroutine(Unlock(locks[0], -1));
                StartCoroutine(Unlock(locks[4], 1));
                state++;
            }

            if (state == 1 && DataManager.Instance.gameData.pulleyState[1] >= 1)
            {
                StartCoroutine(Unlock(locks[1], -1));
                StartCoroutine(Unlock(locks[5], 1));
                state++;
            }

            if (state == 2 && DataManager.Instance.gameData.pulleyState[2] >= 1)
            {
                StartCoroutine(Unlock(locks[2], -1));
                StartCoroutine(Unlock(locks[6], 1));
                state++;
            }

            if (state == 3 && DataManager.Instance.gameData.pulleyState[3] >= 1)
            {
                StartCoroutine(Unlock(locks[3], -1));
                StartCoroutine(Unlock(locks[7], 1));
                state++;
            }

            if(state >= 4)
            {
                transform.Translate(-Vector3.forward * 0.2f);
                if(transform.position.y < -40f)
                    Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Unlock(GameObject _lock, int dir)
    {
        int i = 0;
        while (i < 80)
        {
            _lock.transform.Translate(Vector3.up * dir * 0.05f);
            i++;
            yield return null;
        }
    }
}
