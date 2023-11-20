using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int[] items = new int[4];

    void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = DataManager.Instance.gameData.items[i];
        }
    }

    void AddItem(GameObject item)   //이 함수를 플레이어 스크립트에서 물체와 상호작용할 때 호출시키도록 함.
    {
        int n = 0;
        while(n < items.Length) 
        {
            /*if (items[n] == 0)
                items[n] = item.GetComponent<ItemScript>().itemNum; //각 획득 가능 아이템마다 식별번호 두고 그 식별번호를 저장
            */                                                      //혹은 그냥 각 아이템의 프리팹을 저장하는 배열을 두고, 그 배열의 인덱스를 저장
        }
    }
}
