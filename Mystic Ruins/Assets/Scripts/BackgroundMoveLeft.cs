using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class BackgroundMoveLeft : MonoBehaviour
{
    public float speed = 20;
    public Vector3 dir;

    void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }
}
