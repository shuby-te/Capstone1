using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isMoving) {
            StopCoroutine(DoorClose());
            StartCoroutine(DoorOpen());
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(DoorOpen());
        StartCoroutine(DoorClose());
    }

    IEnumerator DoorClose()
    {
        isMoving = false;
        while (transform.localPosition.y > 14f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.05f, transform.localPosition.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator DoorOpen()
    {
        isMoving = true;
        while (transform.localPosition.y < 19.2f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.05f, transform.localPosition.z);
            yield return new WaitForSeconds(0.01f);
        }
        isMoving = false;
    }
}
