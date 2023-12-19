using System.Collections;
using UnityEngine;

public class DropGear : BossObject
{
    public ParticleSystem[] particle = new ParticleSystem[3];

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y <= -2.15f)
        {
            gameObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                particle[i].Play();
            }
        }
    }
    new private void OnEnable()
    {
        base.OnEnable();
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(grow());
    }
    new private void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].transform.localPosition = new Vector3(pos.x, particle[i].transform.localPosition.y, pos.z);
        }
    }
    IEnumerator grow()
    {
        while (gameObject.transform.localScale.y < 0.5f)
        {
            gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1);
        while (gameObject.transform.localPosition.y>-2.15f)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.03f, gameObject.transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
    }

}
