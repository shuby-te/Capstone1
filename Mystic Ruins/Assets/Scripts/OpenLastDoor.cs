using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLastDoor : MonoBehaviour
{
    public GameObject ItemManager;
    public GameObject UIManager;
    public GameObject lastDoor;

    public CameraMovement cm;
    public FadeEffect fade;

    bool isActive;

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.F))
        {
            int num = UIManager.GetComponent<UIManager>().itemPointerNum;
            if (ItemManager.GetComponent<ItemManager>().UseItem(num, 4))
            {
                StartCoroutine(OpenDoorAndToLast());
            }
        }
    }

    IEnumerator OpenDoorAndToLast()
    {
        int i = 0;
        while (i < 500)
        {
            lastDoor.transform.Rotate(new Vector3(0, 0, 0.2f));
            i++;
            yield return null;
        }

        yield return StartCoroutine(fade.Fade(0));
        SceneManager.LoadScene("EndingScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = true;
            cm.afterBoss = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isActive = false;
            cm.afterBoss = false ;
        }
    }
}
