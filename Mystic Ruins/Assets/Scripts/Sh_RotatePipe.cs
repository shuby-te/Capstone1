using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sh_RotatePipe : MonoBehaviour
{
    public int resetAngle;
    public int clearAngle;

    public bool isSelect;
    public float time;

    public bool minusType;
    public bool correct;

    int currentAngle;
    int answerAngle;

    private void Start()
    {
        answerAngle = (clearAngle - resetAngle + 360) % 360;
    }

    void Update()
    {   
        if(isSelect)
        {
            time += Time.deltaTime;
            if(time < 0.1f && Input.GetMouseButtonDown(0))
                StartCoroutine(Push());
            else if(time >= 0.1f)
                isSelect = false;
        }

        currentAngle %= 360;
        if (minusType && (currentAngle == answerAngle || currentAngle == answerAngle + 180))
            correct = true;
        else if (!minusType && currentAngle == answerAngle)
            correct = true;
        else
            correct = false;
    }

    IEnumerator Push()
    {
        float n = 0;

        while (n < 90)
        {
            yield return new WaitForSeconds(Time.deltaTime / 10);

            transform.Rotate(0, 0, 1);
            yield return null;
            n += 1;
        }
        currentAngle += 90;
        isSelect = false;
    }

    public void ResetPipe()
    {
        Quaternion newRot = Quaternion.Euler(-90, 0, resetAngle);

        transform.rotation = newRot;
    }

    public void ClearPipe()
    {
        Quaternion newRot = Quaternion.Euler(-90, 0, clearAngle);

        transform.rotation = newRot;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

}
