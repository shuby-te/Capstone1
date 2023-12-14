using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChainge : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetInteger("SceneNum", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            anim.SetInteger("SceneNum", 2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            anim.SetInteger("SceneNum", 3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            anim.SetInteger("SceneNum", 4);
        }
    }
}
