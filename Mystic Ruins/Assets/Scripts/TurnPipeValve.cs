using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnPipeValve : MonoBehaviour
{
    public GameObject[] pipes = new GameObject[24];
    public Sh_PipeController controller;
    Animator anim;

    public bool isClicked;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.GetMask("Ground")) && Input.GetMouseButtonDown(0))
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                anim.SetBool("isOpen", true);
                if (CountCorrecting())
                {
                    //ÆÛÁñ Å¬¸®¾î

                    DataManager.Instance.gameData.mapProgress[5] = 1;
                    controller.ClearAct();
                    Debug.Log("Pipe Clear~~");
                }
                else
                {
                    StartCoroutine(Clicked());
                }
            }
        }
    }

    IEnumerator Clicked()
    {
        isClicked = true;
        controller.isValveStop = true;
        yield return new WaitForSeconds(4);
        isClicked = false;
        controller.isValveStop = false;
    }

    bool CountCorrecting()
    {
        int count = 0;
        for (int i = 0; i < pipes.Length; i++)
        {
            if (!(i == 2 || i == 3 || i == 4 || i == 5 || i == 9 || i == 10 || i == 14))
            {
                if (!pipes[i].GetComponent<Sh_RotatePipe>().correct)
                    return false;
            }
        }

        return true;
    }

    

    void CloseValve()
    {
        anim.SetBool("isOpen", false);
        //isClicked = false;
    }
}
