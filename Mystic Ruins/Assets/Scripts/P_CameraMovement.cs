using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float y, z;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z - z);

    }
}
