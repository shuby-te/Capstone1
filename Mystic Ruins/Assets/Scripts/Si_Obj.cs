using System.Collections;
using UnityEngine;

public class Si_Obj : MonoBehaviour
{
    private GameObject player;
    Rigidbody rb;
    public int speed;
    public bool isActive = false;
    public bool isDrop = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (gameObject.CompareTag("BigGear"))
        {
            Destroy(gameObject, 3.5f);
        }
 
        if (gameObject.CompareTag("Bomb"))
        {

            Destroy(gameObject, 3f);
        }

        if (gameObject.CompareTag("Rock"))
        {
            rb.useGravity = true;
            rb.AddForce(Vector3.down * 70, ForceMode.Impulse);
            Destroy(gameObject, 2f);
        }
        if (gameObject.CompareTag("Fire"))
        {
            rb.useGravity = true;
            rb.AddForce(Vector3.down * 100, ForceMode.Impulse);
            Destroy(gameObject, 2.5f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.CompareTag("DropGear"))
        {
            StartCoroutine(DropGear());
            if (isDrop)
            {
                rb.useGravity = true;
                rb.AddForce(Vector3.down * 30,ForceMode.Impulse);
                Destroy(gameObject, 2);
                isDrop= false;
            }
        }
        if (gameObject.CompareTag("MiniGear"))
        {
            transform.position += transform.right * Time.deltaTime * speed;
            Destroy(gameObject, 3f);
        }
    }

    IEnumerator DropGear()
    {
        while (gameObject.transform.localScale.y < 3)
        { 
            gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Debug.Log("����");
    }
}
