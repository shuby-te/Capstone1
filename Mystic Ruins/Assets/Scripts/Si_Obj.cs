using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Si_Obj : MonoBehaviour
{
    private GameObject player;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("BigGear"))
        {
            Destroy(gameObject,3f);
        }
        if (gameObject.CompareTag("MiniGear"))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            Destroy(gameObject, 3f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("¾ÆÇÄ");
    }
}
