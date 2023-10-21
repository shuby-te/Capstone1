using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sh_PushPipe : MonoBehaviour
{
    public GameObject player;
    public GameObject pipeCols;
    public int num;

    Rigidbody rb;
    bool isTouch;
    bool isPush;
    int x, z;

    void Start()
    {
        isTouch = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /*if(isTouch)
        {
            if (-2f <= player.transform.position.x && player.transform.position.x <= -1.5f) x = -1;
            else if (1.5f <= player.transform.position.x && player.transform.position.x <= 2f) x = 1;
            else x = 0;

            if (-2f <= player.transform.position.z && player.transform.position.z <= -1.5f) z = -1;
            else if (1.5f <= player.transform.position.z && player.transform.position.z <= 2f) z = 1;
            else z = 0;

            if(!isPush)
            {
                if ((num == 1 && Input.GetKeyDown(KeyCode.D)) ||
                (num == 2 && Input.GetKeyDown(KeyCode.S)) ||
                (num == 3 && Input.GetKeyDown(KeyCode.A)) ||
                (num == 4 && Input.GetKeyDown(KeyCode.W))){
                    isPush = true;
                    StartCoroutine(Push(true));
                }

                else if ((num == 1 && Input.GetKeyDown(KeyCode.S)) ||
                    (num == 2 && Input.GetKeyDown(KeyCode.A)) ||
                    (num == 3 && Input.GetKeyDown(KeyCode.W)) ||
                    (num == 4 && Input.GetKeyDown(KeyCode.D))){
                    isPush = true;
                    StartCoroutine(Push(false));
                }
            }
        }*/

        if(isTouch && Input.GetKeyDown(KeyCode.G))
        {
            Vector3 forceDirection = transform.forward; // 앞으로 밀기
            rb.AddForce(forceDirection * 10);

            Vector3 torque = new Vector3(0, 1, 0); // y축을 중심으로 회전
            rb.AddTorque(torque * 10);
        }
    }

    IEnumerator Push(bool ClockWise)
    {
        float n = 0;
        float dir = (ClockWise ? 0.5f : -0.5f);
        while (n < 90)
        {
            yield return new WaitForSeconds(0.001f);

            transform.Rotate(0, dir, 0);
            yield return null;
            n += dir;
        }
        pipeCols.transform.rotation = Quaternion.Euler(0, 0, 0);
        isPush = false;        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isTouch = true;
            if(player.transform.parent == null){
                player.transform.SetParent(transform, true);
                pipeCols.transform.SetParent(transform, true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouch = false;
            if (player.transform.parent){
                player.transform.SetParent(null, true);
                pipeCols.transform.SetParent(null, true);
            }
        }
    }
}
