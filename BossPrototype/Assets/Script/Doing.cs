using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Doing : MonoBehaviour
{
    public Animator anim;

    private void Awake()

    {
        anim = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetInteger("DoingTest", 1);
            
        }
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("DoingTest", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("DoingTest", 0);
        }
    }
}
