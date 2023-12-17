using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShield : MonoBehaviour
{
    public UIManager ui;

    string shieldTuto = "3번을 눌러 정면에 방패를 생성";
    bool isDetect;

    void Update()
    {
        if(isDetect)
        {
            if(DataManager.Instance.gameData.skillStates[2] == 0 && Input.GetKeyDown(KeyCode.E))
            {
                DataManager.Instance.gameData.skillStates[2] = 1;
                ui.SetTutoText(shieldTuto);
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
