using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class P_Title : MonoBehaviour
{
    public GameObject teamTitle;
    public float destroyDelay;

    void Start()
    {
        Destroy(teamTitle, destroyDelay);
    }
}
