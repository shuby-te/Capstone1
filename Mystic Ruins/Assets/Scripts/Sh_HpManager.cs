using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sh_HpManager : MonoBehaviour
{
    public List<GameObject> gears;
    public GameObject needle;
    public GameObject player;
    public GameObject hpBar;

    public AudioSource attackedS;
    PlayerMovement2 pm;

    public float maxBossHp = 6000;
    public float maxPlayerHp = 100;

    float overHeat = 800;
    float playerDmg = 100;
    float bombDmg = 8;
    float bossDmg = 5;
    float objectDmg = 3;
    float fireDmg = 1;
    public float bossHp;
    public float playerHp;

    bool damageImmune = false;
    bool fireDamageImmune = false;
    public BossPhase1 bm;
    public Slider hpSlider;
    private void Start()
    {
        pm = player.GetComponent<PlayerMovement2>();
        hpSlider = hpBar.GetComponent<Slider>();
        bossHp = maxBossHp;
        playerHp = maxPlayerHp;
    }

    private void Update()
    {
        if(playerHp < 0)
            StartCoroutine(pm.GameOver());
    }

    public void AttackToBoss()
    {
        if (bm.isActive)
        {
            bossHp -= playerDmg;
            Debug.Log("player attacked: " + playerDmg + "!!!!!");
            MoveHpNeedle();
            attackedS.Play();
        }
    }

    public void BossOverHeat()
    {
        bossHp -= overHeat;
        MoveHpNeedle();
        MoveHpNeedle();
        MoveHpNeedle();
        MoveHpNeedle();
        MoveHpNeedle();
        MoveHpNeedle();
        MoveHpNeedle();
        MoveHpNeedle();
    }

    public void AttackToPlayer(int type)
    {
        if (!damageImmune)
        {
            if (type == 0)
            {
                playerHp -= bossDmg;
                Debug.Log("boss attacked: " + bossDmg + "?????");
                Debug.Log("hp = " + playerHp);
                player.GetComponent<PlayerMovement2>().isKnockback = true;
                player.GetComponent<Animator>().SetBool("isKnockback", true);
            }
            else if (type == 1)
            {
                playerHp -= objectDmg;
                Debug.Log("obj attacked: " + objectDmg + "?????");
                Debug.Log("hp=" + playerHp);
            }
            else if (type == 2)
            {
                playerHp -= fireDmg;
                Debug.Log("fire attacked: " + fireDmg + "?????");
                Debug.Log("hp=" + playerHp);
            }
            else if(type==4)
            {
                playerHp -= bombDmg;
                Debug.Log("obj attacked: " + objectDmg + "?????");
                Debug.Log("hp=" + playerHp);
            }
        }
        if (!fireDamageImmune)
            if (type == 3)
            {
                playerHp -= bombDmg;
                Debug.Log("boss attacked: " + bombDmg + "?????");
                Debug.Log("hp=" + playerHp);
            }
        hpSlider.value = playerHp;
    }

    public void ChangeDamageImmune(bool state)
    {
        damageImmune = state;
    }

    public void ChangeFireDamageImmune(bool state)
    {
        fireDamageImmune = state;
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
            gameObj.transform.Rotate(new Vector3(transform.rotation.x, 0.1f * reverse, transform.rotation.z));
            yield return null;
            n++;
        }
    }

    public void MoveHpNeedleWater()
    {
        StartCoroutine(RotateHpNeedleWater(needle));

        for (int i = 0; i < gears.Count; i++)
        {
            if (i == 1 || i == 4)
                StartCoroutine(RotateHpNeedleWater(gears[i]));
            else
                StartCoroutine(RotateHpNeedleWater(gears[i], -1));
        }
    }


    IEnumerator RotateHpNeedleWater(GameObject gameObj, int reverse = 1)
    {
        float angle = (10 / maxBossHp * 360) * 10;

        int n = 0;
        while (n < angle)
        {
            yield return new WaitForSeconds(0.001f);
            gameObj.transform.Rotate(new Vector3(transform.rotation.x, 0.1f * reverse, transform.rotation.z));
            yield return null;
            n++;
        }
    }
}
