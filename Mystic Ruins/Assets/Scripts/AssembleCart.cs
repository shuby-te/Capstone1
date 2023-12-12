using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleCart : MonoBehaviour
{
    public GameObject[] coals = new GameObject[3];
    public GameObject[] wheels = new GameObject[2];
    
    public GameObject ItemManager;
    public GameObject UIManager;

    bool isActive;

    public int coalNum;
    public int wheelNum;

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.F))
        {
            int num = UIManager.GetComponent<UIManager>().itemPointerNum;
            if (ItemManager.GetComponent<ItemManager>().UseItem(num, 1))
            {
                AssembleCoal();
            }
            else if(ItemManager.GetComponent<ItemManager>().UseItem(num, 0))
            {
                AssembleWheel();
            }
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = false;
        }
    }

    public void AssembleCoal()
    {
        if(coalNum < 3)
        {
            coals[coalNum].SetActive(true);
            coalNum++;
        }

        DataManager.Instance.gameData.coalNum = coalNum;
        DataManager.Instance.gameData.wheelNum = wheelNum;
    }

    public void AssembleWheel()
    {
        if (wheelNum < 2)
        {
            wheels[wheelNum].SetActive(true);
            wheelNum++;
        }

        DataManager.Instance.gameData.coalNum = coalNum;
        DataManager.Instance.gameData.wheelNum = wheelNum;

        if (wheelNum == 2)
        {
            DataManager.Instance.gameData.mapProgress[2] = 1;
        }
    }

    public void EngineBehaviour()
    {
        coalNum = 0;

        for (int i = 0; i < 3; i++)
            coals[i].SetActive(false);
    }
}
