using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemManager : MonoBehaviour
{
    

    public void AddItem(GameObject item)
    {         
        int[] items = DataManager.Instance.gameData.items;
        bool isSaved = false;
        int i = 0;
        while(i < items.Length) 
        {
            Debug.Log("i: " + i);
            if (items[i] == 0)
            {
                DataManager.Instance.gameData.items[i] = item.GetComponent<ItemData>().itemValue;
                Debug.Log("save: " + i);
                isSaved = true;
                break;
            }
            i++;
        }

        if (!isSaved)
            Debug.Log("it`s full!");
    }
}
