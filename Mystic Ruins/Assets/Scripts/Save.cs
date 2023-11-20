using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    public Vector3 posWeight;

    //GameData gameData = 
    bool isSave;

    void Update()
    {
        if (isSave && Input.GetKeyDown(KeyCode.E))
        {
            DataManager.Instance.gameData.x = transform.position.x + posWeight.x;
            DataManager.Instance.gameData.y = transform.position.y + posWeight.y;
            DataManager.Instance.gameData.z = transform.position.z + posWeight.z;
        }           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isSave = true;
            Debug.Log("saved~");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isSave = false;
            Debug.Log("leave~");
        }
    }
}
