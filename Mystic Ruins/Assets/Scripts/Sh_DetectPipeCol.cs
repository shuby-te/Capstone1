using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_DetectPipeCol : MonoBehaviour
{
    public int num;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.transform.parent.parent)
            this.transform.parent.GetComponentInParent<Sh_PushPipe>().num = num;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.transform.parent.parent)
            this.transform.parent.GetComponentInParent<Sh_PushPipe>().num = 0;
    }
}
