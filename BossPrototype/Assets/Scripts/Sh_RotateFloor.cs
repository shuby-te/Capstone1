using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_RotateFloor : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.U))
        {
            if(collision.gameObject.transform.parent == null)
                collision.gameObject.transform.SetParent(transform, true);

            transform.Rotate(0, 0, 1);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null, true);
        }
    }
}
