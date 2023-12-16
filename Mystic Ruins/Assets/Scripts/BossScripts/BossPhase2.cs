using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class BossPhase2 : MonoBehaviour
{
    public GameObject player;
    public GameObject farTrigger;


    [SerializeField]
    int attackNum;
    [SerializeField]
    float speed;
    [SerializeField]
    bool isWalk = true;
    [SerializeField]
    bool turnHead = false;


    bool isFar = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnHead());
        StartCoroutine(Walk());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFar = false;
            isWalk = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFar = true;
            isWalk = true;
        }
    }

    IEnumerator TurnHead()
    {
        bool ing = false;
        while (true)
        {
            if(turnHead)
                if (!ing) 
                {
                    ing = true;
                    Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z); ;
                    for (float i = 0; i < 10; i++)
                    {
                        Vector3 t_dir = (playerPos - transform.position).normalized;
                        transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.1f);
                        yield return new WaitForEndOfFrame();
                    }
                    turnHead = false;
                }
            ing = false;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Walk()
    {
        while (true)
        {
            if (isWalk)
            {
                turnHead = true;
                transform.position += transform.forward * Time.deltaTime * speed;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ExecuteBossPattern()
    {
        while(true)
        {
            attackNum = Random.Range(0,3);
            switch (attackNum)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
