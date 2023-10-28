using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sh_PipeController : MonoBehaviour
{
    public Camera cameraObj;

    bool isActive;
    Vector3 pos = new Vector3(-75, 13, 146);
    Vector3 rot = new Vector3(90, 45, 45);   

    private void FixedUpdate()
    {
       if(isActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Pipe")))
            {
                if(!hit.transform.gameObject.GetComponent<Sh_RotatePipe>().isSelect)
                {
                    hit.transform.gameObject.GetComponent<Sh_RotatePipe>().isSelect = true;
                    hit.transform.gameObject.GetComponent<Sh_RotatePipe>().time = 0f;
                }    
                //hit.transform.gameObject.SetActive(false);
            }
            else
                hit.transform.gameObject.GetComponent<Sh_RotatePipe>().isSelect = false;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cameraObj.GetComponent<CameraMovement>().enabled = false;
            cameraObj.transform.position = pos;
            cameraObj.transform.rotation = Quaternion.Euler(rot);
            collision.gameObject.GetComponent<PlayerAnim>().enabled = false;

            isActive = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        cameraObj.GetComponent<CameraMovement>().enabled = true;
        collision.gameObject.GetComponent<PlayerAnim>().enabled = true;
        if (collision.gameObject.CompareTag("Player"))
            isActive = false;
    }
}
