using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class BackgroundMoveLeft : MonoBehaviour
{
    public float speed = 20;

    void Start()
    {
       
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
