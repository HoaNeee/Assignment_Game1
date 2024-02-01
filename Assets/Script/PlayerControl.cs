using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        Debug.Log("Vao ham Jump");
        
        rb.AddForce(Vector2.up * 6, ForceMode2D.Impulse);


    }
    public void MoveLeft()
    {

        rb.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
    }

    public void MoveRight()
    {
        rb.AddForce(Vector2.right * 5, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
