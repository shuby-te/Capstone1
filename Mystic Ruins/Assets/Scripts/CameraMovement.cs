using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public bool boss;
    Vector3 t_pos;

    void Update()
    {
        if (boss)
        {
            t_pos = new Vector3(player.transform.position.x, player.transform.position.y + 15f, player.transform.position.z - 13);
        }
        else
        {
            t_pos = new Vector3(player.transform.position.x, player.transform.position.y + 8.5f, player.transform.position.z - 8);
        }
        transform.rotation = Quaternion.Euler(40, 0, 0);
        transform.position = Vector3.Lerp(transform.position, t_pos, Time.deltaTime * 2);
    }
}
