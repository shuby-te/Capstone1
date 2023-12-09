using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        StartCoroutine(temp());
    }
    IEnumerator temp()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
