using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ItemManager : MonoBehaviour
{
    public GameObject[] itemImages = new GameObject[4];

    public Sprite[] itemSprites = new Sprite[3];
    //(0)¹ÙÄû / (1)¼®Åº / (2)»ç´Ù¸®

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
}
