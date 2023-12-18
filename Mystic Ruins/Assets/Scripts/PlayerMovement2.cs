using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2 : MonoBehaviour
{
    public GameObject cart;
    public FadeEffect fade;
    public Sh_HpManager hm;

    public float speed = 10f;
    public float dashSpeed = 2f;
    public float rotateSpeed = 7f;
    public float yAngle;
    public float climbToTopLen;

    public bool isKnockback = false;
    public bool isActive;
    public int isClimb;
    public bool setCart;

    public bool isInteract;

    GameObject partner;
    GameObject attackRange;
    GameObject ladder;
    AssembleCart cartWheel;
    Rigidbody rb;
    Animator anim;

    Vector3 dir = Vector3.zero;
    bool dashCool;
    bool isDash;
    public bool isMove;
    bool isGround;
    bool isLadder;
    public bool isCart;
    bool isOver;

    float xAxis = 1f, zAxis = -1f;
    float partnerSpeed = 4f;

    void Start()
    {
        dashCool = true;
        isActive = true;

        partner = transform.GetChild(1).gameObject;
        attackRange = transform.GetChild(2).gameObject;

        rb = GetComponent<Rigidbody>();        
        anim = GetComponent<Animator>();
        cartWheel = cart.GetComponent<AssembleCart>();
    }

    void Update()
    {
        //climb ladder
        if(isLadder && isGround)
        {
            if(!isInteract && Input.GetKeyDown(KeyCode.E))
            {
                isInteract = true;
                isClimb = 1;

                this.GetComponent<ConstantForce>().enabled = false;

                transform.position = new Vector3(ladder.transform.position.x - 0.2f,
                    transform.position.y, ladder.transform.position.z);

                transform.forward = ladder.transform.forward;

                anim.SetInteger("isClimb", 1);

                isGround = false;
            }
        }
        else if(isClimb == 1 && (Input.GetKeyDown(KeyCode.E) || isGround))
        {
            isInteract = false;
            this.GetComponent<ConstantForce>().enabled = true;
            anim.SetInteger("isClimb", -1);
        }

        //move cart
        if(!isInteract && !setCart && isCart && Input.GetKeyDown(KeyCode.E) && cartWheel.wheelNum == 2)
        {
            isInteract = true;
            setCart = true;
            cart.transform.SetParent(transform, false);
            cart.transform.localPosition = new Vector3(0, 0, 2);
            cart.transform.localRotation = Quaternion.Euler(0, 0, 0);
            speed = 8f;
        }
        else if (setCart && Input.GetKeyDown(KeyCode.E))
        {
            isInteract = false;
            setCart = false;
            isCart = false;
            cart.transform.parent = null;
            speed = 10f;
        }

        //left from ladder
        if (isGround)
            isClimb = 0;

        if(isClimb == 2)
        {
            anim.SetInteger("isClimb", 2);
        }    

        if(!isKnockback)
        {
            //key input
            if (!isDash && isClimb == 0)
            {
                dir.x = Input.GetAxisRaw("Horizontal");
                dir.z = Input.GetAxisRaw("Vertical");

                NormalizeDirection();
            }

            //move only keying
            if (isClimb == 0 && isActive && Input.anyKey) isMove = true;
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
        }
        //dash
        if (isClimb == 0 && Input.GetKeyDown(KeyCode.LeftShift) && dashCool)
        {
            hm.ChangeDamageImmune(true);
            hm.ChangeFireDamageImmune(true);
            isDash = true;
            dashCool = false;
            StartCoroutine("OffTheDash");
        }

        //game over
        if (!isOver && transform.position.y < -15)
        {
            isOver = true;
            StartCoroutine(GameOver());
        }
    }

    private void FixedUpdate()
    {
        if(!isKnockback && isClimb == 0)
        {
            //rotation
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 projectedPoint = Vector3.ProjectOnPlane(hit.point - transform.position, Vector3.up);

                Vector3 ignoreHeightPoint = new Vector3(projectedPoint.x, 0, projectedPoint.z);

                if (ignoreHeightPoint != Vector3.zero)
                    transform.forward = ignoreHeightPoint.normalized;
            }

            //move            
            if (isMove && anim.GetInteger("isAttack") == 0 && !isDash)
                rb.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);
        }

        //partner move
        partner.transform.position = new Vector3(transform.position.x - xAxis * 1.6f,
            transform.position.y + 2.5f, transform.position.z - zAxis * 1.6f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGround = true;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = true;
            ladder = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("Cart"))
        {
            isCart = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isLadder = false;
        }

        if (collision.gameObject.CompareTag("Cart"))
        {
            isCart = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isClimb = 2;
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

    public IEnumerator GameOver()
    {
        isActive = false;
        anim.SetBool("isOver", true);
        yield return new WaitForSeconds(1.5f); //time of playing die anim
        
        yield return StartCoroutine(fade.GetComponent<FadeEffect>().Fade(0));

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.UnloadSceneAsync(currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
        isOver = false;
    }

    public void Over()
    {
        anim.SetBool("isOver", false);
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
