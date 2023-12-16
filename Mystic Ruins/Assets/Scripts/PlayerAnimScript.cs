using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    public GameObject hpManager;
    Sh_HpManager hm;
    PlayerMovement2 pm;

    private void Start()
    {
        hm = hpManager.GetComponent<Sh_HpManager>();
        pm = transform.GetComponent<PlayerMovement2>();
        if (hm == null)
            Debug.Log("?????????????");
    }

    void EndRoll()
    {
        Debug.Log("endroll");
        hm.ChangeDamageImmune(false);
        hm.ChangeFireDamageImmune(false);
        pm.isKnockback = false;
    }

    void EndEmmune()
    {
        hm.ChangeDamageImmune(false);
    }

    void StandUp()
    {
        hm.ChangeDamageImmune(false);
        hm.ChangeFireDamageImmune(false);
        pm.isKnockback = false;
    }
}
