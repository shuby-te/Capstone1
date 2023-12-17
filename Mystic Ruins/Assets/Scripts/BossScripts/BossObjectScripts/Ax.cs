using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour
{
    public float rotationSpeed = 45f; // 회전 속도 (도/초)
    public float moveDistance = 3f;   // 이동 거리
    public float moveSpeed = 2f;      // 이동 속도

    private Vector3 originalPosition;
    private float currentRotation = 0f;
    private bool isMovingForward = true;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        // Y 축 주위로 회전
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // 이동 로직
        if (isMovingForward)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(originalPosition, transform.position) >= moveDistance)
            {
                isMovingForward = false;
            }
        }
        else
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(originalPosition, transform.position) <= 0.1f)
            {
                isMovingForward = true;
            }
        }
    }
}
