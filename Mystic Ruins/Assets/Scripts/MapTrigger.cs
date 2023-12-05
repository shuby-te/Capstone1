using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public GameObject Camera;
    public GameObject player;
    public GameObject[] tpPoint;

    public int triggerTag;

    CameraMovement CM;
    PlayerMovement2 PM;

    void Start()
    {
        CM = Camera.GetComponent<CameraMovement>();
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

        if (triggerTag == 0)//���>������
        {
            CM.SetCamera(40, 56, 0, -7, 15, -6);
        }
        else if (triggerTag == 1)//���>ȭ�ι�
        {
            CM.SetCamera(40, 120, 0, -10, 15, 6);
        }
        else if (triggerTag == 2)//���>����
        {
            CM.SetCamera(40, 240, 0, 10, 15, 3);
        }
        else if (triggerTag == 3)//������>���
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
        }
        else if (triggerTag == 4)//ȭ�ι�>���
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
        }
        else if (triggerTag == 5)//����>���
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
