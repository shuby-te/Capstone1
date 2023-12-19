using UnityEngine;

public class ThrowPipe : MonoBehaviour
{
    public float speed;
    public bool isThrow = false;
    public int orbNum = -1;
    public GameObject range;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isThrow)
        {
            range.SetActive(false);
            if (transform.parent != null)
                transform.parent = null;
            transform.GetComponent<MeshCollider>().enabled = true;
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
