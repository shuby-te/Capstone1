using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{  
    public float speed = 10f;
    public float dashSpeed = 2f;
    public float rotateSpeed = 7f;
    public float yAngle;

    public bool isActive;
    public bool isClimb;

    GameObject partner;
    GameObject attackRange;
    GameObject ladder;
    Rigidbody rb;    
    Animator anim;

    Vector3 dir = Vector3.zero;
    bool dashCool;
    bool isDash;
    bool isMove;
    bool isKnokback;
    bool isLadder;

    float xAxis = 1f, zAxis = -1f;
    float partnerSpeed = 4f;

    void Start()
    {
        dashCool = true;
        isActive = true;

        partner = this.transform.GetChild(1).gameObject;
        attackRange = transform.GetChild(2).gameObject;

        rb = GetComponent<Rigidbody>();        
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //check climbing ladder
        if(isLadder)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isClimb = true;
                anim.SetBool("isClimb", true);

                //사다리의 각도와 상관없이 사다리를 타는 방향으로부터 y축을 생각하지 않고 1만큼 떨어진 위치에 플레이어를 고정시킨 뒤,
                //사다리 방향으로 회전시켜야함
                transform.position = new Vector3(ladder.transform.GetChild(0).position.x, 
                    transform.position.y, ladder.transform.GetChild(0).position.z);

                transform.forward = new Vector3(ladder.transform.position.x - transform.position.x, 
                    0, ladder.transform.position.z - transform.position.z);
            }
        }

        /*//climb behavior
        if(isClimb)
        {
            dir.x = dir.z = 0f;
            dir.y = Input.GetAxisRaw("Vertical");

            NormalizeDirection();

            isMove = false;
        }*/

        //common behavior
        if(!isKnokback)
        {
            //key input
            if (!isDash && !isClimb)
            {
                dir.x = Input.GetAxisRaw("Horizontal");
                dir.z = Input.GetAxisRaw("Vertical");

                NormalizeDirection();
            }

            //move only keying
            if (!isClimb && isActive && Input.anyKey) isMove = true;
            else isMove = false;

            //patnerNav movement
            if (Input.GetKey(KeyCode.W))
            {
                zAxis += Time.deltaTime * partnerSpeed;
                if (zAxis > 1f) zAxis = 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                zAxis -= Time.deltaTime * partnerSpeed;
                if (zAxis < -1f) zAxis = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                xAxis += Time.deltaTime * partnerSpeed;
                if (xAxis > 1f) xAxis = 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                xAxis -= Time.deltaTime * partnerSpeed;
                if (xAxis < -1f) xAxis = -1f;
            }

            //dash
            if (!isClimb && Input.GetKeyDown(KeyCode.LeftShift) && dashCool)
            {
                isDash = true;
                dashCool = false;
                StartCoroutine("OffTheDash");
            }
        }      

        //임시
        if(transform.position.y < -15)
        {
            GameData gd = DataManager.Instance.gameData;
            transform.position = new Vector3(gd.x, gd.y, gd.z);
        }
    }

    private void FixedUpdate()
    {
        if(!isKnokback && !isClimb)
        {
            //rotation
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 point = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                Vector3 rotateDir = point - transform.position;
                if (rotateDir != Vector3.zero)
                    transform.forward = rotateDir;
            }

            //move            
            if (isMove && anim.GetInteger("isAttack") == 0 && !isDash)
                rb.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);
        }

        //partner move
        partner.transform.position = new Vector3(transform.position.x - xAxis * 1.6f,
            transform.position.y + 2.5f, transform.position.z - zAxis * 1.6f);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            ladder = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
            ladder = null;
        }
    }

    void NormalizeDirection()
    {
        dir.Normalize();

        Quaternion yRotation = Quaternion.Euler(0, yAngle, 0);
        dir = yRotation * dir;
        dir.Normalize();
    }

    IEnumerator OffTheDash()
    {
        yield return new WaitForSeconds(1.5f);
        isDash = false;
        yield return new WaitForSeconds(1.5f);
        dashCool = true;
    }

    void Attack()
    {
        attackRange.GetComponent<Sh_PlayerAttack>().Attack();
    }

    void EndAttack()
    {
        attackRange.GetComponent<Sh_PlayerAttack>().EndAttack();
    }
}
