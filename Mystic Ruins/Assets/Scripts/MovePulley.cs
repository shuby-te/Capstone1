using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovePulley : MonoBehaviour
{
    public GameObject player;
    public GameObject cart;
    public GameObject wall;
    public Animator anim;

    public bool isDetect;
    public int value;

    
    PlayerMovement2 pm;

    int state;

    void Start()
    {
        state = DataManager.Instance.gameData.pulleyState[value - 1];
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

                if (value == 1)
                    DataManager.Instance.gameData.mapProgress[6] = 1;
            }
        }
    }

    public void ResetClearedState()
    {
        anim.SetFloat("upSpeed", 20);
        anim.SetInteger("downState", 2);

        wall.SetActive(false);
    }

    public void ResetUpState()
    {
        anim.SetBool("upState", false);
        cart.transform.parent = null;

        wall.SetActive(false);
        state = 2;
        DataManager.Instance.gameData.pulleyState[value - 1] = 2;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("upState", true);
            anim.SetFloat("upSpeed", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetFloat("upSpeed", 0);
        }
    }
}
