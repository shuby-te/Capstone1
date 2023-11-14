using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    public GameData gameData;

    bool isSave;

    void Start()
    {
        
    }

    void Update()
    {
        if (isSave && Input.GetKeyDown(KeyCode.E))
        {
            gameData.savePoint = "fuck you";
            Debug.Log("eeee");
        }           
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            isSave = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isSave = false;
    }
}
