using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("1111111111111");
        }
    }
}
