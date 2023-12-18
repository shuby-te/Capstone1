using System.Collections;
using UnityEngine;

public class MiniRock : BossObject
{
    public float speed;
    public float rotationSpeed;

    // Start is called before the first frame update
    new void Start()
    {
        setParent = true;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
        float randomRotationX = Random.Range(0f, 360f);
        transform.Rotate(Vector3.right * randomRotationX * rotationSpeed * Time.deltaTime, Space.Self);
    }
    new private void OnEnable()
    {
        base.OnEnable();
    }
   new private void OnDisable()
    {
        base.OnDisable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
            Disable();
    }

}
