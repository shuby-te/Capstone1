using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Save : MonoBehaviour
{
    public Vector3 posWeight;
    public StartTutorial tutoSystem;

    public int tutoPoint;

    bool isSave;

    private void Start()
    {
        if(tutoPoint == 0)
            this.transform.GetChild(1).gameObject.SetActive(true);
    }

    void Update()
    {
        if (isSave && Input.GetKeyDown(KeyCode.E))
        {
            DataManager.Instance.gameData.x = transform.position.x + posWeight.x;
            DataManager.Instance.gameData.y = transform.position.y + posWeight.y;
            DataManager.Instance.gameData.z = transform.position.z + posWeight.z;

            if (DataManager.Instance.gameData.mapProgress[0] == 0)
                DataManager.Instance.gameData.mapProgress[0] = 1;

            DataManager.Instance.SaveGameData();
            StartCoroutine(PlayEffect());
            if (tutoPoint == 1)
                tutoSystem.isSave = true;
        }           
    }

    IEnumerator PlayEffect()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(7f);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isSave = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isSave = false;
        }
    }
}
