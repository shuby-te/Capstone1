using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_FollowPlayer : MonoBehaviour
{
    public Vector3 relativeDis;
    
    private Vector3 followPos;
    public int followDelay;
    public Transform partnerNav;
    public Transform player;
    public Queue<Vector3> playerPos;  

    void Start()
    {
        playerPos = new Queue<Vector3>();
        followPos = partnerNav.position + relativeDis;
    }

    void Update()
    {
        Watch();
        Follow();
    }

    void Watch()
    {
        if(!playerPos.Contains(partnerNav.position))
            playerPos.Enqueue(partnerNav.position);

        if (playerPos.Count > followDelay)
            followPos = playerPos.Dequeue() + relativeDis;

        transform.forward = new Vector3(player.position.x - transform.position.x, 
            0, player.position.z - transform.position.z);
    }

    void Follow()
    {
        transform.position = followPos;
    }
}
