using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;

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

        if (triggerTag == 1)//가운데>수조룸
        {
            CM.SetCamera(40, 56, 0, -7, 15, -6);
            player.transform.position = new Vector3(-13.25f, 0.48f, 78.43f);
            player.transform.rotation = Quaternion.Euler(0, 59, 0);
            PM.yAngle = 59;
        }
        else if (triggerTag == 2)//가운데>화로방
        {
            CM.SetCamera(40, 120, 0, -10, 15, 6);
            player.transform.position = new Vector3(-2.4f, -0.13f, 31.4f);
            player.transform.rotation = Quaternion.Euler(0, 120, 0);
            PM.yAngle = 120;
        }
        else if (triggerTag == 3)//가운데>광산
        {
            CM.SetCamera(40, 240, 0, 10, 15, 3);
            player.transform.position = new Vector3(-86.51f, 0.47f, 36.4f);
            player.transform.rotation = Quaternion.Euler(0, -120, 0);
            PM.yAngle = 120;
        }
        else if (triggerTag == 4)//수조룸>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
            player.transform.position = new Vector3(-17.25f, 0.48f, 76.03f);
            player.transform.rotation = Quaternion.Euler(0, 240, 0);
            PM.yAngle = 240;
        }
        else if (triggerTag == 5)//화로방>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
            player.transform.position = new Vector3(-10.44f, -0.13f, 36.03f);
            player.transform.rotation = Quaternion.Euler(0, 300, 0);
            PM.yAngle = 300;
        }
        else if (triggerTag == 6)//광산>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
            player.transform.position = new Vector3(-81.09f, 0.48f, 39.53f);
            player.transform.rotation = Quaternion.Euler(0, 60, 0);
            PM.yAngle = 60;
        }

        yield return StartCoroutine(CM.Fade(true));

        player.GetComponent<PlayerMovement2>().enabled = true;
        player.GetComponent<PlayerAnim>().enabled = true;
    }
}
