using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UserBoard : MonoBehaviour
{
    public GameObject board;
    public GameObject boardWall;
    public GameObject ItemManager;
    public GameObject UIManager;
    
    public StartTutorial tutoSystem;

    bool isActive;

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.F))
        {
            int num = UIManager.GetComponent<UIManager>().itemPointerNum;
            if (ItemManager.GetComponent<ItemManager>().UseItem(num, 3))
            {
                board.SetActive(true);
                tutoSystem.isUse = true;
                boardWall.SetActive(false);
                this.gameObject.SetActive(false);                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = false;
        }
    }
}
