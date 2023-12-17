using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockFirebreath : MonoBehaviour
{
    Animator anim;
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
                //½ºÅ³ È¹µæ ½ÃÀÇ ÀÌÆåÆ®³ª ÄÆ½Å, Æ©Åä¸®¾ó µîÀÇ ½ÇÇà
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
