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
            StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        player.GetComponent<PlayerMovement2>().enabled = false;
        //player.GetComponent<PlayerAnim>().enabled = false;
        yield return StartCoroutine(camera.GetComponent<CameraMovement>().Fade(false));
        if (triggerTag == 1)//가운데>수조룸
        {
            CM.SetCamera(40, 56, 0, -7, 15, -6);
            player.transform.position = new Vector3(-13.25f, 0.48f, 78.43f);
            player.transform.rotation = Quaternion.Euler(0, 59, 0);
        }
        else if (triggerTag == 2)//가운데>화로방
        {
            CM.SetCamera(40, 120, 0, -10, 15, 6);
            player.transform.position = new Vector3(-2.4f, -0.13f, 31.4f);
            player.transform.rotation = Quaternion.Euler(0, 120, 0);
        }
        else if (triggerTag == 3)//가운데>광산
        {
            CM.SetCamera(40, 240, 0, 10, 15, 3);
            player.transform.position = new Vector3(-86.51f, 0.47f, 36.4f);
            player.transform.rotation = Quaternion.Euler(0, -120, 0);
        }
        else if (triggerTag == 4)//수조룸>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
            player.transform.position = new Vector3(-17.25f, 0.48f, 76.03f);
            player.transform.rotation = Quaternion.Euler(0, 240, 0);
        }
        else if (triggerTag == 5)//화로방>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
            player.transform.position = new Vector3(-10.44f, -0.13f, 36.03f);
            player.transform.rotation = Quaternion.Euler(0, 300, 0);
        }
        else if (triggerTag == 6)//광산>가운데
        {
            CM.SetCamera(40, 0, 0, 0, 15, -13);
            player.transform.position = new Vector3(-81.09f, 0.48f, 39.53f);
            player.transform.rotation = Quaternion.Euler(0, 60, 0);
        }
        yield return StartCoroutine(camera.GetComponent<CameraMovement>().Fade(true));
        player.GetComponent<PlayerMovement2>().enabled = true;
        player.GetComponent<PlayerAnim>().enabled = true;

    }
}
