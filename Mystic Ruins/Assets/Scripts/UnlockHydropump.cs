using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHydropump : MonoBehaviour
{
    public UIManager ui;

    string hydropumpTuto = "2번을 눌러 정면을 향해 하이드로펌프";
    bool isDetect;

    void Update()
    {
        if(isDetect)
        {
            if(DataManager.Instance.gameData.skillStates[1] == 0 && Input.GetKeyDown(KeyCode.E))
            {
                DataManager.Instance.gameData.skillStates[1] = 1;
                ui.SetTutoText(hydropumpTuto);
                StartCoroutine(ui.FadeTutoText(4f));
                this.transform.GetChild(0).gameObject.SetActive(false);
                this.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDetect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDetect = false;
        }
    }
}
