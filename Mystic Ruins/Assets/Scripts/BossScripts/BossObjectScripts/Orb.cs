using UnityEngine;

public class Orb : MonoBehaviour
{
    public float rotationSpeed = 5f; // 회전 속도
    public int orbNum;
    public GameObject barrier;
    public GameObject boss;
    public ParticleSystem[] ps; 
    int a, b, c;

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
        if(other.GetComponent<ThrowPipe>() != null)
        {
            if (--boss.GetComponent<BossPhase1>().barrierNum == 0)
            {
                boss.GetComponent<Animator>().SetBool("throw", false);
            }
            if (other.GetComponent<ThrowPipe>().orbNum==orbNum)
            {
                Destroy(barrier);
            }
            for(int  i = 0; i < ps.Length; i++)
            {
                ps[i].Play();
            }

            Destroy(other.gameObject);
            Destroy(this.gameObject);
            
        }
    }
}
