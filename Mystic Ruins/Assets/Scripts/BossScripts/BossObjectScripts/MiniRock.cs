using System.Collections;
using UnityEngine;

public class MiniRock : BossObject
{
    public float speed;
    public float rotationSpeed;
    public GameObject range;
    bool run=false;

    // Start is called before the first frame update
    new void Start()
    {
        setParent = true;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(run)
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
        float randomRotationX = Random.Range(0f, 360f);
        transform.Rotate(Vector3.right * randomRotationX * rotationSpeed * Time.deltaTime, Space.Self);
    }
    new private void OnEnable()
    {
        base.OnEnable();
        range.gameObject.SetActive(true);
        StartCoroutine(setRun());
    }
   new private void OnDisable()
    {
        base.OnDisable();
        range.gameObject.SetActive(false);
        run = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
            Disable();
    }
    IEnumerator setRun()
    {
        yield return new WaitForSeconds(1);
        run = true;
    }
}
