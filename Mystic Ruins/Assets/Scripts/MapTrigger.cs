using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public GameObject Camera;
    public GameObject player;
    public GameObject[] tpPoint;
    public FadeEffect fade;

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
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(Move());
        }
    }
    private IEnumerator Move()
    {
        Animator anim = player.GetComponent<Animator>();
        anim.SetInteger("moveNum", 0);
        anim.SetInteger("isAttack", 0);
        anim.SetBool("isRoll", false);

        player.GetComponent<PlayerMovement2>().enabled = false;
        player.GetComponent<PlayerAnim>().enabled = false;

        yield return StartCoroutine(fade.Fade(0));

        switch(triggerTag)
        {
            case 3: case 4: case 5: case 6: case 15: //bridge
                DataManager.Instance.gameData.currentMapValue = 1;
                CM.SetCamera(40, 0, 0, 0, 15, -13); break;

            case 0: //watertank
                DataManager.Instance.gameData.currentMapValue = 4;
                CM.SetCamera(40, 56, 0, -7, 15, -6); break;

            case 1: //stove
                DataManager.Instance.gameData.currentMapValue = 3;
                CM.SetCamera(40, 120, 0, -10, 15, 6); break;

            case 2: //mine
                DataManager.Instance.gameData.currentMapValue = 2;
                CM.SetCamera(40, 240, 0, 10, 15, 3); break;

            case 13: //engine
                DataManager.Instance.gameData.currentMapValue = 7;
                break;

            case 14: //tutorial
                DataManager.Instance.gameData.currentMapValue = 0;
                break;

            case 9: case 10: //pulley
                DataManager.Instance.gameData.currentMapValue = 6;
                break;

            case 7: case 8: case 12: //pipe
                DataManager.Instance.gameData.currentMapValue = 5;
                break;

            case 11: //boss
                DataManager.Instance.gameData.currentMapValue = 8;
                break;
        }

        player.transform.position = tpPoint[triggerTag].transform.position;
        player.transform.rotation = tpPoint[triggerTag].transform.rotation;
        if (triggerTag < 3)
            PM.yAngle = tpPoint[triggerTag].transform.eulerAngles.y;
        else
            PM.yAngle = 0;

        yield return StartCoroutine(fade.Fade(1));

        player.GetComponent<PlayerMovement2>().enabled = true;
        player.GetComponent<PlayerAnim>().enabled = true;
    }
}