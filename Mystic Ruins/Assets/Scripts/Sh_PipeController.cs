using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sh_PipeController : MonoBehaviour
{
    public Sh_RotatePipe[] pipes = new Sh_RotatePipe[24];
    public Camera cameraObj;
    public GameObject player;
    public GameObject plane;

    Animator anim;
    Vector3 cPos = new Vector3(-33.6f, 14.5f, 153f);
    Vector3 cRot = new Vector3(90, -90, -90);

    bool isActive;
    bool isControl;
    public bool isValveStop;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isActive && Input.GetKeyDown(KeyCode.E))
        {
            if (!isControl)
            {
                cameraObj.GetComponent<CameraMovement>().isRotate = true;
                cameraObj.transform.position = cPos;
                cameraObj.transform.rotation = Quaternion.Euler(cRot);
                player.GetComponent<PlayerAnim>().enabled = false;
                player.GetComponent<PlayerMovement2>().isActive = false;
                plane.SetActive(false);
                isControl = true;
            }
            else if (isControl)
            {
                NotControl();
            }
        }

       if(isControl)
       {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Pipe")) && !isValveStop)
            {
                Sh_RotatePipe hitPipe = hit.transform.gameObject.GetComponent<Sh_RotatePipe>();
                if (!hitPipe.isSelect)
                {
                    hitPipe.isSelect = true;
                    hitPipe.time = 0f;
                }    
            }
            else
            {
                Sh_RotatePipe pipe = hit.transform?.gameObject.GetComponent<Sh_RotatePipe>();
                if (pipe != null)
                {
                    pipe.isSelect = false;
                }
            }

       }

        if (CountCorrecting())
        {
            //ÆÛÁñ Å¬¸®¾î

            DataManager.Instance.gameData.mapProgress[5] = 1;

            Debug.Log("Pipe Clear~~");
        }
    }

    bool CountCorrecting()
    {
        int count = 0;
        for (int i = 0; i < pipes.Length; i++)
        {
            if (!(i == 2 || i == 3 || i == 4 || i == 5 || i == 9 || i == 10 || i == 14))
            {
                if (!pipes[i].correct)
                    return false;
            }
        }

        return true;
    }

    public void NotControl()
    {
        cameraObj.GetComponent<CameraMovement>().isRotate = false;
        player.GetComponent<PlayerAnim>().enabled = true;
        player.GetComponent<PlayerMovement2>().isActive = true;
        plane.SetActive(true);
        isControl = false;
    }

    public void ClearAct()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        NotControl();
        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = false;
        }
    }

    public void ResetPipes()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].ResetPipe();
        }
    }

    public void ClearPipes()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].ClearPipe();
        }
    }
}
