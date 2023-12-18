using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public float FCool;
    public float HCool;
    public float SCool;

    public GameObject fireUI;
    public GameObject hydroUI;
    public GameObject shieldUI;

    bool isSkill = false;
    bool isPump = false;
    int waterLen = 1;

    float fTime;
    float hTime;
    float sTime;

    Vector3 blinkPos;
    PlayerMovement2 pm;
    TextMeshProUGUI fireCoolText;
    TextMeshProUGUI hydroCoolText;
    TextMeshProUGUI shieldCoolText;

    public AudioSource fS;
    public AudioSource hS;
    public AudioSource sS;

    private void Start()
    {
        fTime = FCool;
        hTime = HCool;
        sTime = SCool;

        pm = player.parent.gameObject.GetComponent<PlayerMovement2>();

        fireCoolText = fireUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        hydroCoolText = hydroUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        shieldCoolText = shieldUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        ActivateUIWithCool(fireUI, fireCoolText, fTime, FCool);
        ActivateUIWithCool(hydroUI, hydroCoolText, hTime, HCool);
        ActivateUIWithCool(shieldUI, shieldCoolText, sTime, SCool);

        if (!isSkill)
            blinkPos = player.position + (player.forward * gapToPlayer);

        if (isPump)
        {
            blinkPos = player.position + (player.forward * gapToPlayer);

            hydropump.transform.rotation = player.rotation;
            hydropump.transform.position = blinkPos + player.forward;

            partner.rotation = player.rotation;
            partner.position = blinkPos;
        }

        //skillState 활용해서 스킬 사용 상태 수정
        if(pm.isActive)
        {
            if (DataManager.Instance.gameData.skillStates[0] == 1 && Input.GetKeyDown(KeyCode.Alpha1) && fTime >= FCool)
            {
                if (!isSkill)
                {
                    isSkill = true;
                    partner.GetComponent<Sh_FollowPlayer>().isEnable = false;

                    StartCoroutine(Blink(1));
                }
            }
            else if (DataManager.Instance.gameData.skillStates[1] == 1 && Input.GetKeyDown(KeyCode.Alpha2) && hTime >= HCool)
            {
                if (!isSkill)
                {
                    isSkill = true;
                    partner.GetComponent<Sh_FollowPlayer>().isEnable = false;

                    StartCoroutine(Blink(2));
                }
            }
            else if (DataManager.Instance.gameData.skillStates[2] == 1 && Input.GetKeyDown(KeyCode.Alpha3) && sTime >= SCool)
            {
                if (!isSkill)
                {
                    isSkill = true;
                    partner.GetComponent<Sh_FollowPlayer>().isEnable = false;

                    StartCoroutine(Blink(3));
                }
            }
        }

        fTime += Time.deltaTime;
        hTime += Time.deltaTime;
        sTime += Time.deltaTime;

        if (fTime > 100) fTime = FCool;
        if (hTime > 100) hTime = HCool;
        if (sTime > 100) sTime = SCool;        
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
        fTime = 0;
        yield return new WaitForSeconds(0.4f);
        
        fireBreath.Play();
        fS.Play();
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
        fS.Stop();
        PlayBlink();
        //원위치      
    }

    IEnumerator Hydropump()
    {
        hTime = 0;
        yield return new WaitForSeconds(0.4f);

        hydropump.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetFloat("_MainTime", waterLen);
        hydropump.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.SetFloat("_MainTime", waterLen);
        PlayHydropump();
        hS.Play();
        yield return new WaitForSeconds(3f);    //���� ���� �ð��� 7f (���� 6f + �Ҹ� 1f)

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
        hS.Stop();
        PlayBlink();
        //원위치     
    }

    IEnumerator Shield()
    {
        sTime = 0;
        yield return new WaitForSeconds(0.4f);
        
        Vector3 spawnPos = blinkPos + player.forward * 2;
        spawnPos = new Vector3(spawnPos.x, spawnPos.y + 3f, spawnPos.z);
        GameObject spawnShield = Instantiate(shield, spawnPos, player.rotation); 
        spawnShield.GetComponent<Rigidbody>().AddForce(Vector3.down * takeDownForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.14f);
        spark.transform.position = new Vector3(spawnShield.transform.position.x,
            spawnShield.transform.position.y - 1.2f, spawnShield.transform.position.z);
        PlaySpark();
        sS.Play();
        
        yield return new WaitForSeconds(1f);

        PlayBlink();
        partner.gameObject.SetActive(false);
        partner.GetComponent<Sh_FollowPlayer>().isEnable = true;
        isSkill = false;
        yield return new WaitForSeconds(0.5f);

        partner.GetComponent<Sh_FollowPlayer>().resetPos();
        partner.gameObject.SetActive(true);
        yield return null;

        //원위치        
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

    void ActivateUIWithCool(GameObject ui, TextMeshProUGUI textMesh, float time, float cool)
    {
        if(time < cool)
        {
            ui.SetActive(true);
            textMesh.text = (cool - time).ToString("F2");
        }
        else
            ui.SetActive(false);
    }
}
