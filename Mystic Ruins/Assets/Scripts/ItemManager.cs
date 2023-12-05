using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ItemManager : MonoBehaviour
{
    public GameObject[] itemImages = new GameObject[4];

    public Sprite[] itemSprites = new Sprite[3];
    //(0)바퀴 / (1)석탄 / (2)사다리

    public void AddItem(GameObject item, int itemValue)
    {         
        int[] items = DataManager.Instance.gameData.items;
        bool isSaved = false;
        int i = 0;
        while(i < items.Length) 
        {
            if (items[i] == 0)
            {
                DataManager.Instance.gameData.items[i] = item.GetComponent<ItemData>().itemValue;
                itemImages[i].GetComponent<Image>().sprite = itemSprites[itemValue - 1];
                itemImages[i].GetComponent<Image>().enabled = true;
                isSaved = true;
                break;
            }
            i++;
        }

        if (!isSaved)
            Debug.Log("it`s full!");
    }

    //아이템 사용시의 데이터 수정 기능 및 UI 수정 기능 구현 필요
}
