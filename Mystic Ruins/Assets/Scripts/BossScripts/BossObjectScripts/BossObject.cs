using UnityEngine;

public class BossObject : MonoBehaviour
{
    protected Vector3 pos;
    protected Vector3 rot;
    protected Transform parent;
    protected bool setParent = false;
    // Start is called before the first frame update
    protected void Start()
    {
        if (setParent)
            parent = transform.parent;
        pos = transform.localPosition;
        rot = transform.eulerAngles;
        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        if (setParent)
            transform.parent = null;
    }
    public void Disable()
    {
        if (setParent)
            transform.parent = parent;
        gameObject.SetActive(false);
    }
    protected void OnDisable()
    {
        transform.localPosition = pos;
        transform.eulerAngles = rot;
    }

}
