using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    public Vector3 posWeight;

    bool isSave;

    void Update()
    {
        if (isSave && Input.GetKeyDown(KeyCode.E))
        {
            DataManager.Instance.gameData.x = transform.position.x + posWeight.x;
            DataManager.Instance.gameData.y = transform.position.y + posWeight.y;
            DataManager.Instance.gameData.z = transform.position.z + posWeight.z;

            DataManager.Instance.SaveGameData();
        }           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isSave = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isSave = false;
        }
    }
}
