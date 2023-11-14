using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public string savePoint;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(savePoint);
        }
        
    }
}
