using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float eSpeed;
    public GameObject enemyGraphic;
    bool facingRight = false;
    float facingTime = 4f;
    float nextFlip = 0f;
    bool canFlip = true;

    Animator anim;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFlip)
        {
            nextFlip = Time.time + facingTime;
            Flip();
        }

    }

    void Flip()
    {
        if (!canFlip)
        {
            return;
        }
        facingRight = !facingRight;
        Vector3 vector3 = enemyGraphic.transform.localScale;
        vector3.x *= -1;
        enemyGraphic.transform.localScale = vector3; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(facingRight && collision.transform.position.x < transform.position.x)
            {
                Flip();
            } else if(!facingRight && collision.transform.position.x > transform.position.x)
            {
                Flip();
            }
            canFlip = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!facingRight) {
                rb.AddForce(new Vector2(-1, 1) * eSpeed);
            } else
            {
                rb.AddForce(new Vector2(1, 0) * eSpeed);
            }
            anim.SetBool("isRunning", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canFlip = true;
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("isRunning", false);
        }
    }
}
