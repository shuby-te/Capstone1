using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh_GoGoMonsterBall : MonoBehaviour
{
    public Transform player;
    public Transform partner;
    public float blinkLen;

    public ParticleSystem blink;
    public ParticleSystem fireBreath;
    public GameObject hydropump;

    bool isSkill = false;
    Vector3 movedBlinkPos;
    Vector3 stoppedBlinkPos;

    void Start()
    {
        
    }

    void Update()
    {
        if(!isSkill)
            movedBlinkPos = player.position + (player.forward * blinkLen);

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {            
            if(!isSkill)
            {
                isSkill = true;
                partner.GetComponent<Sh_FollowPlayer>().isEnable = false;
                
                StartCoroutine("Blink");
            }
        }
    }

    IEnumerator Blink()
    {
        blink.transform.position = partner.position;
        blink.Play();
        partner.gameObject.SetActive(false);
        partner.position = movedBlinkPos;
        yield return new WaitForSeconds(0.3f);
       
        partner.rotation = player.rotation;
        fireBreath.transform.rotation = player.rotation;
        partner.gameObject.SetActive(true);
        yield return null;
        fireBreath.transform.position = movedBlinkPos;
        blink.transform.position = movedBlinkPos;
        blink.Play();

        StartCoroutine("FireBreath");
        yield break;
    }

    IEnumerator FireBreath()
    {
        partner.GetComponent<Sh_FollowPlayer>().resetQue();
        yield return new WaitForSeconds(0.4f);
        
        fireBreath.Play();
        yield return new WaitForSeconds(2f);    //실제 동작 시간은 1.7f

        //partner.gameObject.SetActive(false);        
        partner.GetComponent<Sh_FollowPlayer>().isEnable = true;    
        isSkill = false;    //거지같은 처음 위치 훑고가기 문제 해결 필요
        //partner.position = new Vector3(player.position.x + 1.6f, player.position.y + 2.5f, player.position.z - 1.6f);
        yield return new WaitForSeconds(0.1f);

        //partner.gameObject.SetActive(true);
    }
}
