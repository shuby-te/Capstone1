using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWatertank : MonoBehaviour
{
    public GameObject waterInTank;
    public GameObject hydroTrig;

    Animator anim;

    bool isOpen;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("isOpen", true);
            StartCoroutine(FillTheWater());
        }
    }

    public void CloseValve()
    {
        anim.SetBool("isOpen", false);
    }

    IEnumerator FillTheWater()
    {
        while(waterInTank.transform.localPosition.y < 4.5f)
        {
            waterInTank.transform.Translate(Vector3.up * Time.deltaTime * 1.5f);            
            yield return null;
        }

        DataManager.Instance.gameData.mapProgress[4] = 2;
        this.GetComponent<OpenWatertank>().enabled = false;
        hydroTrig.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOpen = false;
        }
    }
}
