using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    
    Vector3 boxSize = new Vector3(4, 0.1f, 1);
    Vector3 t_pos;

    public bool afterBoss;    
    public float a = 40, b = 0, c = 0, x = 0, y = 15, z = -13;
    
    public bool isFade = false;
    public bool isRotate;
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

        if(isRotate)
        {

        }
        else if (isMap)
        {
            if (afterBoss)
            {
                t_pos = new Vector3(player.transform.position.x + x, player.transform.position.y + y + 17, player.transform.position.z + z - 12);
            }
            else
            {
                t_pos = new Vector3(player.transform.position.x + x, player.transform.position.y + y, player.transform.position.z + z);
            }
            transform.rotation = Quaternion.Euler(a, b, c);
            transform.position = Vector3.Lerp(transform.position, t_pos, Time.deltaTime * 2);
        }
        else 
        {
            transform.rotation = Quaternion.Euler(43.067f, 81.555f, -0.081f);
            transform.position = new Vector3(-229.8252f, 150.901f, 144.0875f);
        }
    }

    void LateUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, boxSize / 2, 
            direction, Quaternion.identity, Mathf.Infinity, 1 << LayerMask.NameToLayer("Wall"));        

        for (int i = 0; i < hits.Length; i++)
        {
            TransParentWall[] obj = hits[i].transform.GetComponentsInChildren<TransParentWall>();            

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j].time = 0;
                if(!obj[j].isDetectOut)
                    obj[j]?.BecomeTransparent();                
            }
        }
    }

    public void SetCamera(int a,int b,int c,int x, int y, int z)
    {
        this.a = a; this.b = b;this.c = c; this.x = x;this.y = y; this.z = z;
    }
}
