using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DetectWaterFlow : MonoBehaviour
{
    public TurnPipeValve valve;

    MeshRenderer mesh;
    GameObject com;

    public bool superPass;
    bool isActivate;
    public int isConnect;
    public bool isDetect;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (DataManager.Instance.gameData.mapProgress[4] == 3)
            isActivate = true;

        if(isActivate)
        {
            if(!valve.isClicked)
            {
                isConnect = 0;
                mesh.enabled = false;
            }

            if (superPass)
                isConnect = 2;
            else
            {
                if(valve.isClicked)
                {
                    if (isDetect && com.GetComponent<DetectWaterFlow>() != null)
                    {
                        int tmp = com.GetComponent<DetectWaterFlow>().isConnect;
                        if(tmp > isConnect)
                            isConnect = tmp;
                    }
                }
               
                SetNeighborConnect(isConnect);
            }
            

            if(isConnect == 2)
            {
                if (isDetect)
                    mesh.enabled = false;
                else
                    mesh.enabled = true;
            }
            else
                mesh.enabled = false;
        }  
    }

    void SetNeighborConnect(int connect)
    {
        //bool allBreak = false;
        GameObject parent = this.transform.parent.gameObject;
        DetectWaterFlow[] neighbors = parent.GetComponentsInChildren<DetectWaterFlow>(true);
        foreach(DetectWaterFlow e in neighbors)
        {
            /*if(connect == -1)
            {
                foreach (DetectWaterFlow e2 in neighbors)
                {
                    e2.isConnect = 0;
                }
                allBreak = true;
            }
            if (allBreak)
                break;

            if (e.isConnect == 2)
            {
                foreach (DetectWaterFlow e2 in neighbors)
                {
                    e2.isConnect = 2;
                }
                allBreak = true;
            }
            if (allBreak)
                break;*/

            e.isConnect = connect;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("FlowedWater"))
        {
            isDetect = true;
            com = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FlowedWater"))
        {
            isDetect = false;

            com = null;
        }
    }
}
