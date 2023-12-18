using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEngine : MonoBehaviour
{
    public GameObject player;
    public GameObject cart;
    public GameObject fire;

    GameObject coal;
    PlayerMovement2 pm;

    bool isCart;
    bool isPlayer;
    bool isFire;

    void Start()
    {
        coal = this.transform.GetChild(0).gameObject;
        pm = player.GetComponent<PlayerMovement2>();        
    }

    void Update()
    {
        if(isCart && pm.setCart && Input.GetKeyDown(KeyCode.F) && DataManager.Instance.gameData.mapProgress[7] == 0)
        {
            DataManager.Instance.gameData.mapProgress[7] = 1;
            coal.SetActive(true);
            cart.GetComponent<AssembleCart>().EngineBehaviour();
        }     
        
        if(isFire && DataManager.Instance.gameData.mapProgress[7] == 1)
        {
            DataManager.Instance.gameData.mapProgress[7] = 2;
            fire.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cart"))
            isCart = true;

        if (other.gameObject.CompareTag("Fire"))
            isFire = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cart"))
            isCart = false;
    }
}
