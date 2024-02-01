using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public float wSpeed = 3f;
    public float rSpeed = 12f;
    public float move ;
    public int do_cao = 13;
    public bool allowJump;
    public bool allowDoubleJump;
    
    
    public bool isGround = true;

    public static CharMove charMove;

    public bool facingRight = true;

    private Rigidbody2D rb;
    private Animator anim;

    SpriteRenderer spriteRenderer;

    // attack
    public Transform muiSung;
    public GameObject fireBall;
    public float fireRate = 1f;
    public float nextFire = 0;



    // Start is called before the first frame update
    void Start()
    {
        charMove = this;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }
    
    public void CharMoveRight()
    {
        //rb.velocity = new Vector2(wSpeed * 2, rb.velocity.y);


        //rb.AddForce(Vector2.right * wSpeed, ForceMode2D.Impulse);
        move = 1f;

    }
    public void CharMoveLeft()
    {
        //rb.velocity = new Vector2(-wSpeed * 2, rb.velocity.y);
        //rb.AddForce(Vector2.left * wSpeed, ForceMode2D.Impulse);
        move = -1f;

    }
    
    public void StopMove()
    {
        move = 0f;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void CharFlipLeft()
    {
        spriteRenderer.flipX = true;
        facingRight = false;
        
    }
    public void CharFlipRight()
    {
        spriteRenderer.flipX = false;
        facingRight = true;
    }
    // Update is called once per frame
    void Update()
    {
        speedControll(wSpeed, rSpeed);
        CharMovement(move);

        CharFlip();
        //transform.Translate(Vector2.right * move * wSpeed * Time.deltaTime);
        
        CharJump();

        checkAni();

        if(Input.GetAxisRaw("Fire1") > 0)
        {
            BanDan();
        }
    }

    private void FixedUpdate()
    {
        
        

    }

        public void BanDan()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (facingRight)
            {
                spriteRenderer.flipX = false;
                Instantiate(fireBall, muiSung.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (!facingRight)
            {
                spriteRenderer.flipX = true;
                Instantiate(fireBall, muiSung.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
        }
    }
    void CharMovement(float speed)
    {
        move = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(move* speed, rb.velocity.y);
    }
    void speedControll(float walkSpeed, float runSpeed)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move = runSpeed;
        }
        else
        {
            move = walkSpeed;
        }
    }
    public void CharJump()
    {
        
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGround)
        {
            rb.AddForce(Vector2.up * do_cao, ForceMode2D.Impulse);
            allowDoubleJump = false;

        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            if (allowDoubleJump || isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, do_cao);
                allowDoubleJump = !allowDoubleJump;
            }
        }
    }
    void checkAni()
    {

        anim.SetFloat("Move", Mathf.Abs(move));
        if (!isGround)
        {
            anim.SetBool("isGround", false);
        }
        else
        {
            anim.SetBool("isGround", true);
        }
        
    }
    void CharFlip()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
            facingRight = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
            facingRight = true;
        }
    }
    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    public void CharJumpControl()
    {
        Debug.Log("ham jump ne");
        if (isGround)
        {
            rb.AddForce(Vector2.up * do_cao, ForceMode2D.Impulse);
            allowDoubleJump = false;

        }
        
            if (allowDoubleJump || isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, do_cao);
                allowDoubleJump = !allowDoubleJump;
            }
        
    }


}
