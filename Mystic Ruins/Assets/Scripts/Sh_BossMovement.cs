using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Sh_BossMovement : MonoBehaviour
{
    public GameObject player;
    public float followDelay;

    Queue<Vector3> headingVecs;
    Vector3 followVec = Vector3.zero;
    Animator anim;    

    void Start()
    {
        headingVecs = new Queue<Vector3>();
        anim = GetComponent<Animator>();
        anim.SetBool("isActive", true);
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        Watch();
        //TurnHead();
    }

    /*private void FixedUpdate()
    {
        
    }*/

    void TurnHead()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        Vector3 t_dir = (playerPos - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.1f);
    }

    void Watch()
    {
        if (!headingVecs.Contains(player.transform.position))
            headingVecs.Enqueue(player.transform.position);

        if (headingVecs.Count > followDelay)
            followVec = headingVecs.Dequeue();

        transform.forward = new Vector3(followVec.x - transform.position.x, 
            transform.forward.y, followVec.z - transform.position.z);
    }
}
