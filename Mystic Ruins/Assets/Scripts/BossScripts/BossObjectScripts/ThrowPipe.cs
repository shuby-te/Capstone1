using UnityEngine;

public class ThrowPipe : MonoBehaviour
{
    public float speed;
    public bool isThrow = false;
    public int orbNum = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isThrow)
        {
            if (transform.parent != null)
                transform.parent = null;
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);
        }
    }


}
