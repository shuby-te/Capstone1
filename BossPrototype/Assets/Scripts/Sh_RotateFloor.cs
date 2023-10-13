using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_RotateFloor : MonoBehaviour
{
    public GameObject hourFloor;
    [SerializeField] bool isActivate;

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("colli");

        if (collision.gameObject.CompareTag("Player"))
        { 
            Debug.Log("taggi");
            if (Input.GetKeyDown(KeyCode.U) && !isActivate)
            {
                if (collision.gameObject.transform.parent == null)
                    collision.gameObject.transform.SetParent(transform, true);

                isActivate = true;
                StartCoroutine(moveFloor(this.gameObject, 1));
                StartCoroutine(moveFloor(hourFloor, 1));            
            }

            else if(Input.GetKeyDown(KeyCode.I) && !isActivate) 
            {
                if (collision.gameObject.transform.parent == null)
                    collision.gameObject.transform.SetParent(transform, true);

                isActivate = true;
                StartCoroutine(moveFloor(this.gameObject, 2));
                StartCoroutine(moveFloor(hourFloor, 1));
            }

            else if (Input.GetKeyDown(KeyCode.O) && !isActivate)
            {
                if (collision.gameObject.transform.parent == null)
                {
                    Debug.Log("parri");
                    collision.gameObject.transform.SetParent(transform, true);
                }

                isActivate = true;
                StartCoroutine(moveFloor(this.gameObject, 3));
                StartCoroutine(moveFloor(hourFloor, 1));
            }     
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null, true);
        }
    }

    IEnumerator moveFloor(GameObject gameObj, int num)
    {
        num *= 60;
        int n = 0;
        while(n < num)
        {
            yield return new WaitForSeconds(0.001f);
            gameObj.transform.Rotate(0, 0, 1);
            yield return null;
            n++;
        }

        Debug.Log(num);
        if (gameObj == this.gameObject)
            isActivate = false;
    }
}
