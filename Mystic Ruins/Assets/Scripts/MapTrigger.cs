using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public CameraMovement CM;
    public PlayerMovement2 PM;
    public GameObject[] tpPoint;
    public FadeEffect fade;

    public int triggerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(Move());
        }
    }
    public IEnumerator Move(bool spe = false)
    {
        Animator anim = PM.gameObject.GetComponent<Animator>();
        anim.SetInteger("moveNum", 0);
        anim.SetInteger("isAttack", 0);
        anim.SetBool("isRoll", false);

        PM.gameObject.GetComponent<PlayerMovement2>().isMove = false;
        PM.gameObject.GetComponent<PlayerMovement2>().enabled = false;
        PM.gameObject.GetComponent<PlayerAnim>().enabled = false;

        if(!spe)
        {
            yield return StartCoroutine(fade.Fade(0));

            switch (triggerTag)
            {
                case 3:case 4:case 5:case 6:case 15: //bridge
                    DataManager.Instance.gameData.currentMapValue = 1;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 0, 0, 0, 15, -13 };
                    CM.SetCamera(40, 0, 0, 0, 15, -13); break;

                case 0: //watertank
                    DataManager.Instance.gameData.currentMapValue = 4;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 56, 0, -7, 15, -6 };
                    CM.SetCamera(40, 56, 0, -7, 15, -6); break;

                case 1: //stove
                    DataManager.Instance.gameData.currentMapValue = 3;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 120, 0, -10, 15, 6 };
                    CM.SetCamera(40, 120, 0, -10, 15, 6); break;

                case 2: //mine
                    DataManager.Instance.gameData.currentMapValue = 2;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 240, 0, 10, 15, 3 };
                    CM.SetCamera(40, 240, 0, 10, 15, 3); break;

                case 13: //engine
                    DataManager.Instance.gameData.currentMapValue = 7;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 0, 0, 0, 15, -13 };
                    break;

                case 14: //tutorial
                    DataManager.Instance.gameData.currentMapValue = 0;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 0, 0, 0, 15, -13 };
                    break;

                case 9:case 10: //pulley
                    DataManager.Instance.gameData.currentMapValue = 6;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 0, 0, 0, 15, -13 };
                    break;

                case 7:case 8:case 12: //pipe
                    DataManager.Instance.gameData.currentMapValue = 5;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 0, 0, 0, 15, -13 };
                    break;

                case 11: //boss
                    DataManager.Instance.gameData.currentMapValue = 8;
                    DataManager.Instance.gameData.cameraTrans = new int[] { 40, 0, 0, 0, 15, -13 };
                    break;
            }

            PM.gameObject.transform.position = tpPoint[triggerTag].transform.position;
            PM.gameObject.transform.rotation = tpPoint[triggerTag].transform.rotation;

            if (triggerTag < 3)
                PM.yAngle = tpPoint[triggerTag].transform.eulerAngles.y;
            else
                PM.yAngle = 0;

            DataManager.Instance.gameData.yAngle = PM.yAngle;

            yield return StartCoroutine(fade.Fade(1));
        }
        else
        {
            int[] t = DataManager.Instance.gameData.cameraTrans;
            CM.SetCamera(t[0], t[1], t[2], t[3], t[4], t[5]);

            PM.yAngle = DataManager.Instance.gameData.yAngle;
        }

        PM.gameObject.GetComponent<PlayerMovement2>().enabled = true;
        PM.gameObject.GetComponent<PlayerMovement2>().isMove = true;
        PM.gameObject.GetComponent<PlayerAnim>().enabled = true;
    }
}