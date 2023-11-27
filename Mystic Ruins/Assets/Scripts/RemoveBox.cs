using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBox : MonoBehaviour
{
    bool isInteract;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)  //왜 트리거가 아닌가? Enter와 Exit는 어떻게 구분하는가?
    {
        if (other.gameObject.CompareTag("Fire"))
            isInteract = true;
    }

    
}
