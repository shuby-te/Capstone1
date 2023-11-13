using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_HpManager : MonoBehaviour
{
    public float playerHp = 100;
    public float bossHp = 10000;

    float playerDmg = 100;
    float bossDmg = 100;

    public void AttackToBoss()
    {
        bossHp -= playerDmg;
        Debug.Log(playerDmg + "!!!!!");
    }
}
