using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Con : MonoBehaviour
{

    public float movSpeed;
    public float jumpForce;

    private bool jumpCheck;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode shoot;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-movSpeed, rb.velocity.y);
        } else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(movSpeed, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKey(jump) && jumpCheck == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        jumpCheck = false;
        //hasJumped = false;
        print("Landet");

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        jumpCheck = true;
        print("Hop");

    }
}
