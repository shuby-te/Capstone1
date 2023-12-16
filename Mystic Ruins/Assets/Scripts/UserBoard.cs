using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UserBoard : MonoBehaviour
{
    public GameObject board;
    public GameObject ItemManager;
    public GameObject UIManager;

    bool isActive;

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.F))
        {
            int num = UIManager.GetComponent<UIManager>().itemPointerNum;
            if (ItemManager.GetComponent<ItemManager>().UseItem(num, 2))
            {
                board.SetActive(true);
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
