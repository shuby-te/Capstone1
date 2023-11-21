using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    public int triggerTag;
    public GameObject camera;
    public GameObject player;
    CameraMovement CM;
    // Start is called before the first frame update
    void Start()
    {
        CM = camera.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerTag == 0)
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
        }
        if (triggerTag == 1)
        {
            CM.SetCamera(40, 56, 0, -7, 15, -6);
            player.transform.position = new Vector3(-13.25f, 0.48f, 78.43f);
            player.transform.rotation = Quaternion.Euler(0, 59, 0);
        }
        if (triggerTag == 2)
        {
            CM.SetCamera(40, 120, 0, -10, 15, 6);
            player.transform.position = new Vector3(-2.4f, -0.13f, 31.4f);
            player.transform.rotation= Quaternion.Euler(0, 120, 0);
        }
        if (triggerTag == 3)
        {
            CM.SetCamera(40, 240, 0, 10, 15, 3);
            player.transform.position = new Vector3(-86.51f, 0.47f, 36.4f);
            player.transform.rotation = Quaternion.Euler(0, -120, 0);
        }
    }
}
