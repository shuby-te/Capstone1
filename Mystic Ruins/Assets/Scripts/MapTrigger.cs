using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    public GameObject[] tpPoint;

    public int triggerTag;

    CameraMovement CM;
    PlayerMovement2 PM;

    void Start()
    {
        CM = camera.GetComponent<CameraMovement>();
        PM = player.GetComponent<PlayerMovement2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        Animator anim = player.GetComponent<Animator>();
        anim.SetInteger("moveNum", 0);
        anim.SetInteger("isAttack", 0);
        anim.SetBool("isRoll", false);

        player.GetComponent<PlayerMovement2>().enabled = false;
        player.GetComponent<PlayerAnim>().enabled = false;

        yield return StartCoroutine(CM.Fade(false));

        if (triggerTag == 0)//가운데>수조룸
        {
            CM.SetCamera(40, 56, 0, -7, 15, -6);
        }
        else if (triggerTag == 1)//가운데>화로방
        {
            CM.SetCamera(40, 120, 0, -10, 15, 6);
        }
        else if (triggerTag == 2)//가운데>광산
        {
            CM.SetCamera(40, 240, 0, 10, 15, 3);
        }
        else if (triggerTag == 3)//수조룸>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
        }
        else if (triggerTag == 4)//화로방>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
        }
        else if (triggerTag == 5)//광산>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
        }

        player.transform.position = tpPoint[triggerTag].transform.position;
        player.transform.rotation = tpPoint[triggerTag].transform.rotation;
        if (triggerTag < 3)
            PM.yAngle = tpPoint[triggerTag].transform.eulerAngles.y;
        else
            PM.yAngle = 0;

        yield return StartCoroutine(CM.Fade(true));

        player.GetComponent<PlayerMovement2>().enabled = true;
        player.GetComponent<PlayerAnim>().enabled = true;
    }
}
