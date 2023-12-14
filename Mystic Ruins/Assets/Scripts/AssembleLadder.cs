using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleLadder : MonoBehaviour
{
    public GameObject ladder;
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
                ladder.SetActive(true);
                DataManager.Instance.gameData.mapProgress[4] = 1;
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
