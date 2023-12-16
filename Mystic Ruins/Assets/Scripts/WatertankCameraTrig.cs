using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatertankCameraTrig : MonoBehaviour
{
    public Camera cameraObj;

    Vector3 cPos = new Vector3(44.79f, 33.1f, 118.63f);
    Vector3 cRot = new Vector3(86f, 60.61f, 0);

    bool isTrig;

    void Update()
    {
        if(isTrig)
        {
            cameraObj.GetComponent<CameraMovement>().enabled = false;
            cameraObj.transform.position = cPos;
            cameraObj.transform.rotation = Quaternion.Euler(cRot);
        }
        else
            cameraObj.GetComponent<CameraMovement>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(isTrig) isTrig = false;
            else isTrig = true;
        }
    }
}
