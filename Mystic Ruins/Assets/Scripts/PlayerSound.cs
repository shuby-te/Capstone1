using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public PlayerMovement2 pm;
    public AudioSource stepMetal;
    public AudioSource stepSand;

    public AudioSource stepS;
   
    void Start()
    {

    }

    void Update()
    {
        if (DataManager.Instance.gameData.currentMapValue == 2)
        {
            stepMetal.gameObject.SetActive(false);
            stepSand.gameObject.SetActive(true);
            stepS = stepSand;
        }
        else
        {
            stepMetal.gameObject.SetActive(true);
            stepSand.gameObject.SetActive(false);
            stepS = stepMetal;
        }

        if (pm.isMove)
            stepS.gameObject.SetActive(true);
        else
            stepS.gameObject.SetActive(false);
    }


}
