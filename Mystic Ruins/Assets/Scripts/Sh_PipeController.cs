using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sh_PipeController : MonoBehaviour
{
    public GameObject[] pipes = new GameObject[24];
    public Camera cameraObj;
    public Camera player;

    Animator anim;
    Vector3 cPos = new Vector3(-33.6f, 14.5f, 153f);
    Vector3 cRot = new Vector3(90, -90, -90);

    bool isActive;
    bool isControl;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(isActive && Input.GetKeyDown(KeyCode.E))
        {
            if (!isControl)
            {
                cameraObj.GetComponent<CameraMovement>().enabled = false;
                cameraObj.transform.position = cPos;
                cameraObj.transform.rotation = Quaternion.Euler(cRot);
                player.GetComponent<PlayerAnim>().enabled = false;
                isControl = true;
            }
            else if (isControl)
            {
                cameraObj.GetComponent<CameraMovement>().enabled = true;
                player.GetComponent<PlayerAnim>().enabled = true;
                isControl = false;
            }
        }

       if(isControl)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Pipe")))
            {
                Sh_RotatePipe hitPipe = hit.transform.gameObject.GetComponent<Sh_RotatePipe>();
                if (!hitPipe.isSelect)
                {
                    hitPipe.isSelect = true;
                    hitPipe.time = 0f;
                }    
            }
            else
                hit.transform.gameObject.GetComponent<Sh_RotatePipe>().isSelect = false;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            isActive = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {        
        
        if (collision.gameObject.CompareTag("Player"))
            isActive = false;
    }

    public void ResetPipes()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].GetComponent<Sh_RotatePipe>().ResetPipe();
        }
    }

    public void ClearPipes()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].GetComponent<Sh_RotatePipe>().ClearPipe();
        }
    }
}
