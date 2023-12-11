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
        if (isInteract && Input.GetKeyDown(KeyCode.F))
        {
            bool isSaved = itemManager.GetComponent<ItemManager>().AddItem(itemValue);

            if (isSaved && isUnique)
                Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteract = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteract = false;
        }
    }
}
