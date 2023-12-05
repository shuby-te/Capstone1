using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFiretrap : MonoBehaviour
{
    GameObject fire1;
    GameObject fire2;

    bool isInteract;
    float time;

    void Start()
    {
        fire1 = this.transform.GetChild(0).gameObject;
        fire2 = this.transform.GetChild(1).gameObject;

        time = 0;
    }

    void Update()
    {
        if (isInteract)
            time += Time.deltaTime;

        if(time > 2f)
        {
            endParticleLooping(fire1);
            endParticleLooping(fire2);
        }
    }

    void endParticleLooping(GameObject particleObj)
    {
        ParticleSystem particle = particleObj.GetComponent<ParticleSystem>();
        var mainModule = particle.main;
        this.GetComponent<BoxCollider>().enabled = false;
        mainModule.loop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            isInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
            isInteract = false;
    }
}
