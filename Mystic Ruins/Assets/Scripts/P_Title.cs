using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class P_Title : MonoBehaviour
{
    public GameObject Title;
    public GameObject TeamTitle;
    public float destroyDelay = 3.0f;

    void Start()
    {
        Destroy(Title, destroyDelay);
        Destroy(TeamTitle, destroyDelay+2);
    }

    public void OnClickChageScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}
