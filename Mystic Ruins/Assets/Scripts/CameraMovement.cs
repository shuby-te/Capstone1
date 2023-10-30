using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public bool boss;
    Vector3 t_pos;
    bool isMap;

    private void Start()
    {
        isMap = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (isMap) isMap = false;
            else isMap = true;
        }

        if (isMap)
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
        else
        {
            transform.rotation = Quaternion.Euler(43.067f, 81.555f, -0.081f);
            transform.position = new Vector3(-229.8252f, 150.901f, 144.0875f);
        }
    }

    
}
