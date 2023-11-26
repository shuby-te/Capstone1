using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public GameObject itemManager;

    public int itemValue;
    public bool isUnique;

    bool isInteract;

    private void Update()
    {
        if (isInteract && Input.GetKeyDown(KeyCode.E))
        {
            itemManager.GetComponent<ItemManager>().AddItem(this.gameObject);
            if(isUnique)
                Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInteract = false;
        }
    }
}
