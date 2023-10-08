using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_FollowPlayer : MonoBehaviour
{
    public Vector3 relativeDis;
    
    private Vector3 followPos;
    public int followDelay;
    public Transform player;
    public Queue<Vector3> playerPos;  

    void Start()
    {
        playerPos = new Queue<Vector3>();
        followPos = player.position + relativeDis;
    }

    void Update()
    {
        Watch();
        Follow();
    }

    void Watch()
    {
        if(!playerPos.Contains(player.position))
            playerPos.Enqueue(player.position);

        if (playerPos.Count > followDelay)
            followPos = playerPos.Dequeue() + relativeDis;
    }

    void Follow()
    {
        transform.position = followPos;
    }
}
