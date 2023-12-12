using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Orb : MonoBehaviour
{
    public float rotationSpeed = 5f; // 회전 속도
    int a, b, c;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    void Update()
    {
        a = UnityEngine.Random.Range(0, 1);
        b = UnityEngine.Random.Range(0, 1);
        c = UnityEngine.Random.Range(0, 1);
        if ((a == 0 && b == 0 && c == 0))
            a = 1;
        float x = transform.eulerAngles.x + a;
        float y = transform.eulerAngles.y + b;
        float z = transform.eulerAngles.z + c;
        transform.Rotate(new Vector3(x, y, z), rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
    }
}
