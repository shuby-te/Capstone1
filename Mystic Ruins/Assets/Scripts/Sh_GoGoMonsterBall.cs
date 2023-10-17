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
    bool isPump = false;
    Vector3 movedBlinkPos;
    Vector3 stoppedBlinkPos; //어디 쓸려고 만들었지?

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
                
                StartCoroutine(Blink(1));
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!isSkill)
            {
                isSkill = true;
                partner.GetComponent<Sh_FollowPlayer>().isEnable = false;

                StartCoroutine(Blink(2));
            }
        }
    }

    IEnumerator Blink(int num)
    {
        blink.transform.position = partner.position;
        blink.Play();
        partner.gameObject.SetActive(false);
        partner.position = movedBlinkPos;
        yield return new WaitForSeconds(0.5f);
       
        partner.rotation = player.rotation;
        partner.gameObject.SetActive(true);
        yield return null;

        blink.transform.position = movedBlinkPos;
        blink.Play();

        switch(num)
        {
            case 1:
                fireBreath.transform.rotation = player.rotation;
                fireBreath.transform.position = movedBlinkPos;
                StartCoroutine("FireBreath");                
                break;
            case 2:
                
                StartCoroutine("Hydropump");
                break;
            case 3:
                
                break;
        }
        
        yield break;
    }

    IEnumerator FireBreath()
    {
        
        yield return new WaitForSeconds(0.4f);
        
        fireBreath.Play();
        yield return new WaitForSeconds(2f);    //실제 동작 시간은 1.7f

        blink.Play();
        partner.gameObject.SetActive(false);       
        partner.GetComponent<Sh_FollowPlayer>().isEnable = true;    
        isSkill = false;
        yield return new WaitForSeconds(0.5f);

        partner.GetComponent<Sh_FollowPlayer>().resetPos();
        partner.gameObject.SetActive(true);
        yield return null;
        blink.transform.position = partner.position;
        blink.Play();
    }

    IEnumerator Hydropump()
    {
        yield return new WaitForSeconds(0.4f);
    }
}
