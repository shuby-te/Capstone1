using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePulley : MonoBehaviour
{
    public GameObject player;
    public GameObject cart;

    public bool isDetect;
    public int value;

    Animator anim;
    PlayerMovement2 pm;

    int state;

    void Start()
    {
        state = DataManager.Instance.gameData.pulleyState[value - 1];
        anim = GetComponent<Animator>();
        pm = player.GetComponent<PlayerMovement2>();
    }

    void Update()
    {
        if (isDetect)
        {
            isDetect = false;

            pm.isInteract = false;
            pm.setCart = false;
            pm.cart.transform.parent = null;
            pm.speed = 10f;

            cart.transform.SetParent(this.transform, true);

            if (state == 0)
            {
                state = 1;
                DataManager.Instance.gameData.pulleyState[value - 1] = 1;

                anim.SetInteger("downState", 1);
            }
            else
            {
                anim.SetInteger("downState", 2);
            }
        }
    }

    void ResetUpState()
    {
        anim.SetBool("upState", false);
        //여기에 restrict 블록이나 다른 것들을 리셋시키는 내용 들어가야 할지도
    }
    //도르래룸 분기 변경
    //도르래 상태 데이터 변경
    //도르래 시야 가리는 거 없애기
    //벽 고민


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("www");
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("upState", true);
            anim.SetFloat("upSpeed", 1);
            anim.SetInteger("downState", 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("weee");
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetFloat("upSpeed", 0);
        }
    }
}
