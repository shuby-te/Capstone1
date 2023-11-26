using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public GameObject fade;
    public GameObject player;
    public bool boss;
    public bool isFade=false;
    Vector3 t_pos;
    bool isMap;
    public float a = 40, b = 0, c = 0, x = 0, y = 15, z = -13;
    private void Start()
    {
        isMap = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (isMap) isMap = false;
            else isMap = true;
        }

        if (isMap)
        {
            if (boss)
            {
                t_pos = new Vector3(player.transform.position.x + x, player.transform.position.y + y, player.transform.position.z + z);
            }
            else
            {
                t_pos = new Vector3(player.transform.position.x, player.transform.position.y + 8.5f, player.transform.position.z - 8);
            }
            transform.rotation = Quaternion.Euler(a, b, c);
            transform.position = Vector3.Lerp(transform.position, t_pos, Time.deltaTime * 2);
        }
        else
        {
            transform.rotation = Quaternion.Euler(43.067f, 81.555f, -0.081f);
            transform.position = new Vector3(-229.8252f, 150.901f, 144.0875f);
        }
    }
    public void SetCamera(int a,int b,int c,int x, int y,int z)
    {
        this.a = a; this.b = b;this.c = c; this.x = x;this.y = y; this.z = z;
    }
    public IEnumerator Fade(bool state)
    {
        fade.GetComponent<FadeEffect>().Fade(state);
        if (state)
            yield return new WaitForSeconds(1.5f);
        else
            yield return new WaitForSeconds(3);

    }

}
