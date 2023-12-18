using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_RotateFloor : MonoBehaviour
{
    public GameObject hourFloor;
    public GameObject minuteFloor;
    public GameObject mainCamera;
    public GameObject player;

    [SerializeField] bool isActivate;
    [SerializeField] bool isCol;
    [SerializeField] bool isWork;

    private void Update()
    {
        if (isCol && Input.GetKeyDown(KeyCode.E))
        {
            if(isWork)
            {
                isWork = false;
                player.GetComponent<PlayerMovement2>().isActive = true;
                player.GetComponent<PlayerAnim>().enabled = true;
                mainCamera.GetComponent<CameraMovement>().isRotate = false;
            }
            else
            {
                isWork = true;
                player.GetComponent<PlayerMovement2>().isActive = false;
                player.GetComponent<PlayerAnim>().enabled = false;
                mainCamera.GetComponent<CameraMovement>().isRotate = true;
                mainCamera.transform.position = new Vector3(-0.2f, 27.6f, 30);
                mainCamera.transform.rotation = Quaternion.Euler(49, 0, 0);
            }
        }

        if(isWork)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !isActivate)
            {
                isActivate = true;
                StartCoroutine(moveFloor(minuteFloor, 1));
                StartCoroutine(moveFloor(hourFloor, 1));
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2) && !isActivate)
            {
                isActivate = true;
                StartCoroutine(moveFloor(minuteFloor, 2));
                StartCoroutine(moveFloor(hourFloor, 1));
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3) && !isActivate)
            {
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
            isCol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCol = false;
        }
    }

    IEnumerator moveFloor(GameObject gameObj, int num)
    {
        num *= 60;
        int n = 0;
        while(n < num)
        {
            yield return new WaitForSeconds(Time.deltaTime / 10f);
            gameObj.transform.Rotate(0, 0, 1);
            yield return null;
            n++;
        }

        if (gameObj == minuteFloor)
            isActivate = false;
    }
}
