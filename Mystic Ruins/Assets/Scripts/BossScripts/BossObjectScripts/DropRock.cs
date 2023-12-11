using System.Collections;
using UnityEngine;

public class DropRock : MonoBehaviour
{
    public ParticleSystem[] particle = new ParticleSystem[3];
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        particle[0].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].transform.localPosition = new Vector3(transform.localPosition.x, -0.175f, transform.localPosition.z);
            StartCoroutine(Drop());
        }
    }
    private void OnDisable()
    {
        for (int i = 1; i < 4; i++)
        {
            particle[i].Play();
        }
    }
    IEnumerator Drop()
    {
        particle[0].gameObject.SetActive(true);
        particle[0].Play();
        yield return new WaitForSeconds(0.5f);
        while (gameObject.transform.localPosition.y > -0.175f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.0001f, gameObject.transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
        particle[0].gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
