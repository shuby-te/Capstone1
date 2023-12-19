using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceanManager : MonoBehaviour
{
    Animator anim1, anim2;
    public GameObject mainBoss;
    public GameObject mainCamera;
    public GameObject cutSceneSword;
    public GameObject aiSword;
    public FadeEffect fade;
    void Start()
    {
        anim1 = transform.GetComponent<Animator>();
        anim2 = mainBoss.GetComponent<Animator>();
    }


    public IEnumerator CutSceanStart(int num)
    {
        yield return StartCoroutine(fade.Fade(0));
        StartCoroutine(fade.Fade(1));
        anim1.enabled = true;
        anim1.SetInteger("SceneNum", num);
        StartCoroutine(SetInt());
    }

    public void CutSceanEnd(int num)
    {
        StartCoroutine(CutSceanEnd());

        if(num==1)
        {
        }
        else if(num==2)
        {
        }
        else if (num==3) 
        {
        }
    }

    void SpawnAiSword()
    {
    }

    IEnumerator CutSceanEnd()
    {
        yield return StartCoroutine(fade.Fade(0));
        anim1.enabled = false;
        aiSword.SetActive(true);
        cutSceneSword.SetActive(false);
        yield return StartCoroutine(fade.Fade(1));
        anim2.SetBool("isPass", true);
    }

    IEnumerator CutSceanEnd1()
    {
        yield return StartCoroutine(fade.Fade(0));
        anim1.enabled = false;
        cutSceneSword.SetActive(false);
        yield return StartCoroutine(fade.Fade(1));
    }

    IEnumerator SetInt()
    {
        yield return new WaitForSeconds(0.1f);
        anim1.SetInteger("SceneNum", 0);
    }

}
