using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Sh_HpManager : MonoBehaviour
{
    public GameObject needle;
    public List<GameObject> gears;

    public float maxBossHp = 6000;
    public float maxPlayerHp = 100;

    float playerDmg = 100;
    float bossDmg = 100;

    float bossHp;
    float playerHp;

    private void Start()
    {
        bossHp = maxBossHp;
        playerHp = maxPlayerHp;
    }

    public void AttackToBoss()
    {
        bossHp -= playerDmg;
        Debug.Log(playerDmg + "!!!!!");
        MoveHpNeedle();
    }

    public void AttackToPlayer()
    {
        playerHp -= bossDmg;
        Debug.Log(bossDmg + "?????");
    }

    public void MoveHpNeedle()
    {        
        StartCoroutine(RotateHpNeedle(needle));

        for (int i = 0; i < gears.Count; i++)
        {
            if(i == 1 || i == 4)
                StartCoroutine(RotateHpNeedle(gears[i]));
            else
                StartCoroutine(RotateHpNeedle(gears[i], -1));
        }
    }

    IEnumerator RotateHpNeedle(GameObject gameObj, int reverse = 1)
    {
        float angle = (playerDmg / maxBossHp * 360) * 10;

        int n = 0;
        while (n < angle)
        {
            yield return new WaitForSeconds(0.001f);
            gameObj.transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 0.1f * reverse));
            yield return null;
            n++;
        }
    }
}
