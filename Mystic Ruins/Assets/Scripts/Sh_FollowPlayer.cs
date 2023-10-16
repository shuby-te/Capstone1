using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_FollowPlayer : MonoBehaviour
{
    public Vector3 relativeDis;
        
    public Transform partnerNav;
    public Transform player;
    public Queue<Vector3> playerPos;
    public int followDelay;
    public bool isEnable;

    private Vector3 followPos;

    void Start()
    {
        playerPos = new Queue<Vector3>();
        followPos = partnerNav.position + relativeDis;
        isEnable = true;
    }

    void Update()
    {
        Debug.Log(playerPos.Count);
        if (isEnable)
        {
            Watch();
            Follow();
        }
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

    public void resetQue()
    {
        playerPos.Clear();
        Debug.Log("큐 내용물: " + playerPos.Count);
        Debug.Log("큐는 내가 정리했으니 걱정 말라고");
    }
}
