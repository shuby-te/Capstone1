using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnlockFirebreath : MonoBehaviour
{
    public UIManager ui;
    public GameObject fireTwinkle;

    Animator anim;

    string firebreathTuto = "1���� ���� ������ ���� ȭ�����";
    bool isDetect;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isDetect)
        {
            anim.SetBool("isDetect", true);

            if (DataManager.Instance.gameData.skillStates[0] == 0 && Input.GetKeyDown(KeyCode.E))
            {
                DataManager.Instance.gameData.mapProgress[3] = 1;

                DataManager.Instance.gameData.skillStates[0] = 1;
                ui.SetTutoText(firebreathTuto);
                StartCoroutine(ui.FadeTutoText(4f));
                fireTwinkle.SetActive(false);
            }
        }
        else
            anim.SetBool("isDetect", false);
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
