using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Con : MonoBehaviour 
{

    //Variable der bestemmer hop og bevægelseshastighed
    public float movSpeed;
    public float jumpForce;

    //En bool der checker om spilleren står på jorden
    private bool jumpCheck;

    //Custom controls der kan sættes i editoren
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode shoot;

    public GameObject throwPoint;
    public GameObject bullet;
    private Rigidbody2D rb;
    private bool gun1ShotLimit;
    public bool haveGun1;

    private GameObject opponent;
    private Vector3 originalePos;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        gun1ShotLimit = true;
        originalePos = gameObject.transform.position;
        haveGun1 = false;


       foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player != this)
            {
                opponent = player;
            }
        }

        Physics2D.IgnoreCollision(opponent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        //Basalt movement system hvor vi bruger hastighedsvektorer
        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-movSpeed, rb.velocity.y);
            transform.localScale = new Vector3 (0.1f,0.1f,1f);
        } else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(movSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-0.1f, 0.1f, 1f);
        } else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Hoppe system, der checker om man trykker på hoppe knappen, og om man står på jorden, før man kan hoppe.
        if (Input.GetKeyDown(jump) && jumpCheck == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            print("hop");
            
        }

        if (Input.GetKeyDown(shoot) && gun1ShotLimit == true && haveGun1 == true)
        {
            gun1ShotLimit = false;
            GameObject bulletClone = (GameObject)Instantiate(bullet, throwPoint.transform.position, throwPoint.transform.rotation);
            bulletClone.transform.localScale = -transform.localScale * 2f;
            Invoke("ShootAgain", 1f);
        }
    }

    //Her checker vi bare om vi står på jorden så vi kan hoppe.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCheck = false;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);            
        }
        if (collision.gameObject.tag == "Gun1")
        {
            haveGun1 = true;
        }
       

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCheck = true;
        }
    }

    void ShootAgain()
    {
        gun1ShotLimit = true;
    }

   
}
