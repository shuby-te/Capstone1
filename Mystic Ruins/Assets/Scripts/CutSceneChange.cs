using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    Animator anim1, anim2;
    public GameObject boss;


    // Start is called before the first frame update
    void Start()
    {
        anim1 = gameObject.GetComponent<Animator>();
        anim2 = boss.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim1.SetInteger("SceneNum", 1);
            anim2.SetInteger("SceneNum", 1);
            StartCoroutine(SetInt());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            anim1.SetInteger("SceneNum", 2);
            anim2.SetInteger("SceneNum", 2);
            StartCoroutine(SetInt());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            anim1.SetInteger("SceneNum", 3);
            anim2.SetInteger("SceneNum", 2);
            StartCoroutine(SetInt());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            anim1.SetInteger("SceneNum", 4);
            anim2.SetInteger("SceneNum", 4);
            StartCoroutine(SetInt());
        }
    }

    IEnumerator SetInt()
    {
        yield return new WaitForSeconds(0.1f);
        anim1.SetInteger("SceneNum", 0);
        anim2.SetInteger("SceneNum", 0);



    }
 
}

