using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEngine : MonoBehaviour
{
    public GameObject player;
    public GameObject cart;

    GameObject coal;
    GameObject cartCoal;

    bool isCart;
    bool isPlayer;
    bool isFire;

    void Start()
    {
        /*
        coal = this.transform.GetChild(0).gameObject;
        cartCoal = cart.transform.GetChild(0).gameObject;

        if(DataManager.Instance.gameData.mapProgress[7] == 0)
            coal.SetActive(false);
        else if(DataManager.Instance.gameData.mapProgress[7] == 1)
            coal.SetActive(true);
            각종 이펙트 실행
        else
            각종 이펙트 실행
         */
    }

    void Update()
    {
        if(isCart && isPlayer && Input.GetKeyDown(KeyCode.E) && DataManager.Instance.gameData.mapProgress[7] == 0)
        {
            DataManager.Instance.gameData.mapProgress[7] = 1;
            /*coal.SetActive(true);
            cartCoal.SetActive(false);*/
        }     
        
        if(isFire && DataManager.Instance.gameData.mapProgress[7] == 1)
        {
            DataManager.Instance.gameData.mapProgress[7] = 2;
            //석탄이 가열된 이펙트 실행 (또는 석탄 색을 빨갛게 달궈진 모습으로 조정)
            //다른 기타 퍼즐 해결 관련 이펙트 실행
        }

        /*
        if(){}  //진행도가 2이고 파이프룸 및 수조룸이 모두 해결되었을때를 조건으로, 
                //물이 끓거나 증기가 나오는 이펙트 실행하고 해결된 도르래에 한하여 증기 피스톤 애니메이션 재생
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cart"))
            isCart = true;

        if (other.gameObject.CompareTag("Player"))
            isPlayer = true;

        if (other.gameObject.CompareTag("Fire"))
            isFire = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cart"))
            isCart = false;

        if (other.gameObject.CompareTag("Player"))
            isPlayer = false;
    }
}
