using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_PartnerSkill : MonoBehaviour
{
    public Transform player;
    public Transform partner;
    public float gapToPlayer;
    public float takeDownForce;

    public GameObject blink;
    public ParticleSystem fireBreath;
    public GameObject hydropump;
    public GameObject shield;
    public GameObject spark;

    bool isSkill = false;
    bool isPump = false;
    int waterLen = 1;

    Vector3 blinkPos;

    void Update()
    {
        if(!isSkill)
            blinkPos = player.position + (player.forward * gapToPlayer);

        if (isPump)
        {
            blinkPos = player.position + (player.forward * gapToPlayer);

            hydropump.transform.rotation = player.rotation;
            hydropump.transform.position = blinkPos + player.forward;

            partner.rotation = player.rotation;
            partner.position = blinkPos;
        }

        if(!Input.GetKey(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!isSkill)
                {
                    isSkill = true;
                    partner.GetComponent<Sh_FollowPlayer>().isEnable = false;

                    StartCoroutine(Blink(1));
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!isSkill)
                {
                    isSkill = true;
                    partner.GetComponent<Sh_FollowPlayer>().isEnable = false;

                    StartCoroutine(Blink(2));
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (!isSkill)
                {
                    isSkill = true;
                    partner.GetComponent<Sh_FollowPlayer>().isEnable = false;

                    StartCoroutine(Blink(3));
                }
            }
        }
    }

    IEnumerator Blink(int num)
    {
        blink.transform.position = partner.position;
        PlayBlink();
        partner.gameObject.SetActive(false);
        partner.position = blinkPos;
        yield return new WaitForSeconds(0.5f);
       
        partner.rotation = player.rotation;
        partner.gameObject.SetActive(true);
        if(num == 2) isPump = true;
        yield return null;

        blink.transform.position = blinkPos;
        PlayBlink();

        switch (num)
        {
            case 1:
                fireBreath.transform.rotation = player.rotation;
                fireBreath.transform.position = blinkPos;
                StartCoroutine("FireBreath");                
                break;
            case 2:
                StartCoroutine("Hydropump");
                break;
            case 3:
                StartCoroutine("Shield");
                break;
        }        
        yield break;
    }

    IEnumerator FireBreath()
    {        
        yield return new WaitForSeconds(0.4f);
        
        fireBreath.Play();
        yield return new WaitForSeconds(2f);    //���� ���� �ð��� 1.7f

        PlayBlink();
        partner.gameObject.SetActive(false);       
        partner.GetComponent<Sh_FollowPlayer>().isEnable = true;    
        isSkill = false;
        yield return new WaitForSeconds(0.5f);

        partner.GetComponent<Sh_FollowPlayer>().resetPos();
        partner.gameObject.SetActive(true);
        yield return null;

        blink.transform.position = partner.position;
        PlayBlink();
    }

    IEnumerator Hydropump()
    {
        yield return new WaitForSeconds(0.4f);

        hydropump.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetFloat("_MainTime", waterLen);
        hydropump.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetFloat("_MainTime", waterLen);
        PlayHydropump();
        yield return new WaitForSeconds(6f);    //���� ���� �ð��� 7f (���� 6f + �Ҹ� 1f)

        StartCoroutine("DeleteWater");
        yield return new WaitForSeconds(1.3f);
        blink.transform.position = blinkPos;
        PlayBlink();
        isPump = false;

        
        partner.gameObject.SetActive(false);        
        partner.GetComponent<Sh_FollowPlayer>().isEnable = true;
        isSkill = false;        
        yield return new WaitForSeconds(0.5f);

        partner.GetComponent<Sh_FollowPlayer>().resetPos();
        partner.gameObject.SetActive(true);        
        yield return null;

        blink.transform.position = partner.position;
        PlayBlink();
    }

    IEnumerator Shield()
    {
        yield return new WaitForSeconds(0.4f);

        
        Vector3 spawnPos = blinkPos + player.forward * 2;
        spawnPos = new Vector3(spawnPos.x, spawnPos.y + 3f, spawnPos.z);
        GameObject spawnShield = Instantiate(shield, spawnPos, player.rotation);
        spawnShield.GetComponent<Rigidbody>().AddForce(Vector3.down * takeDownForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.14f);
        spark.transform.position = new Vector3(spawnShield.transform.position.x,
            spawnShield.transform.position.y - 1.2f, spawnShield.transform.position.z);
        PlaySpark();
        
        yield return new WaitForSeconds(1f);

        PlayBlink();
        partner.gameObject.SetActive(false);
        partner.GetComponent<Sh_FollowPlayer>().isEnable = true;
        isSkill = false;
        yield return new WaitForSeconds(0.5f);

        partner.GetComponent<Sh_FollowPlayer>().resetPos();
        partner.gameObject.SetActive(true);
        yield return null;

        blink.transform.position = partner.position;
        PlayBlink();
        yield return new WaitForSeconds(5f);

        Destroy(spawnShield);
    }

    IEnumerator DeleteWater()
    {
        float len = waterLen;
        while(len > 0)
        {
            hydropump.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetFloat("_MainTime", len);
            hydropump.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetFloat("_MainTime", len);
            len -= 0.01f;

            yield return new WaitForSeconds(0.01f);
        }        
    }

    void PlayBlink()
    {
        ParticleSystem p1 = blink.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        ParticleSystem p2 = blink.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        p1.Play();
        p2.Play();
    }

    void PlayHydropump()
    {
        ParticleSystem outW = hydropump.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        ParticleSystem inW = hydropump.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();        
        ParticleSystem hitW = hydropump.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();

        outW.Play();
        inW.Play();
        hitW.Play();
    }

    void PlaySpark()
    {
        ParticleSystem p1 = spark.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        ParticleSystem p2 = spark.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        ParticleSystem p3 = spark.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();

        p1.Play();
        p2.Play();
        p3.Play();
    }
}
