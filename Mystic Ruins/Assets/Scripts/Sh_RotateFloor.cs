using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_RotateFloor : MonoBehaviour
{
    public GameObject hourFloor;
    public GameObject minuteFloor;

    private GameObject player;
    [SerializeField] bool isActivate;
    [SerializeField] bool isCol;

    private void Update()
    {
        if(isCol && Input.GetKey(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !isActivate)
            {
                if (player != null && player.transform.parent == null)
                    player.transform.SetParent(transform, true);

                isActivate = true;
                StartCoroutine(moveFloor(minuteFloor, 1));
                StartCoroutine(moveFloor(hourFloor, 1));
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2) && !isActivate)
            {
                if (player != null && player.transform.parent == null)
                    player.transform.SetParent(transform, true);

                isActivate = true;
                StartCoroutine(moveFloor(minuteFloor, 2));
                StartCoroutine(moveFloor(hourFloor, 1));
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3) && !isActivate)
            {
                if (player != null && player.transform.parent == null)
                    player.transform.SetParent(transform, true);

                isActivate = true;
                StartCoroutine(moveFloor(minuteFloor, 3));
                StartCoroutine(moveFloor(hourFloor, 1));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            isCol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null, true);
            isCol = false;
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
        if (gameObj == minuteFloor)
            isActivate = false;
    }
}
