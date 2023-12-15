using UnityEngine;

public class Orb : MonoBehaviour
{
    public float rotationSpeed = 5f; // 회전 속도
    public int orbNum;
    public GameObject barrier;
    public GameObject boss;
    int a, b, c;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    void Update()
    {
        a = Random.Range(0, 1);
        b = Random.Range(0, 1);
        c = Random.Range(0, 1);
        if ((a == 0 && b == 0 && c == 0))
            a = 1;
        float x = transform.eulerAngles.x + a;
        float y = transform.eulerAngles.y + b;
        float z = transform.eulerAngles.z + c;
        transform.Rotate(new Vector3(x, y, z), rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.GetComponent<ThrowPipe>() != null)
        {
            Destroy(gameObject);
            Destroy(other);
            if (--boss.GetComponent<BossMovement>().barrierNum == 0)
            {
                boss.GetComponent<Animator>().SetBool("throw", false);
            }
            if (other.GetComponent<ThrowPipe>().orbNum==orbNum)
            {
                Destroy(barrier);
            }
        }
    }
}
