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

    //Variable til at skyde med ketchup pistolen
    public GameObject throwPoint;
    public GameObject bullet;
    public GameObject toiletPaper;
    private Rigidbody2D rb;
    private bool gun1ShotLimit;
    public bool haveGun1;
    public bool haveGun2;

    //Modstanderens gameobjekt vi bruger til at forhindre dem i at kollidere
    public GameObject opponent;

    //Den originale position til vores spillere, når vi skal genstarte runden
    private Vector3 originalePos;

    //Animationssystem
    public Animator thisBodyAnimator;
    //public Animator thisArmAnimator;
    public Animator thisGunAnimator;
    public GameObject gunArm;
    public GameObject penArm;
    

    void Start()
    {

        //Når scenen starter definerer vi disse variable så vi har gemt deres værdi til senere.
        rb = GetComponent<Rigidbody2D>();
        gun1ShotLimit = true;
        originalePos = gameObject.transform.position;
        haveGun1 = false;
        haveGun2 = false;

        //Dette er et "for each" loop, hvor vi leder efter alle gameobjekter med tagget "player". Derefter vælger vi det objekt
        //der ikke er denne spiller, og sætter det til at være modstanderen. Så er vi altid sikre på vi kan finde en modstander, så længe
        //der er en i scenen.
       foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player != this.gameObject)
            {
                opponent = player;
            }
        }
        //Nu hvor vi har en modstander kan vi fortælle unity at den skal ignorerer collisionen mellem disse.
        Physics2D.IgnoreCollision(opponent.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        
    }

    // Update is called once per frame
    void Update()
    {
        //Basalt movement system hvor vi bruger hastighedsvektorer. Hvis vi holder en knap nede, vil vi bevæge i den retning, ellers vil vi stoppe.
        //Systemet sørger for at al vertical momentum er bibeholdt.
        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-movSpeed, rb.velocity.y);

            //Her vender vi gameobjektet baseret på retningen, så animation er rigtig.
            transform.localScale = new Vector3 (0.1f,0.1f,1f);
        } else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(movSpeed, rb.velocity.y);

            //Her vender vi gameobjektet baseret på retningen, så animation er rigtig.
            transform.localScale = new Vector3(-0.1f, 0.1f, 1f);
        } else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Hoppe system, der checker om man trykker på hoppe knappen, og om man står på jorden, før man kan hoppe.
        //Systemet fungerer ved at en lille kasse under spilleren konstant checker om man står på jorden. Hvis kassen rør jorden, så kan man hoppe.
        if (Input.GetKeyDown(jump) && jumpCheck == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            thisBodyAnimator.SetBool("Jumping", true);
            
        }
        

        if (rb.velocity.x != 0 && jumpCheck == false)
        {
            thisBodyAnimator.SetBool("Moving", true);
        } else
        {
            thisBodyAnimator.SetBool("Moving", false);
        }

        //Det er her vi skyder med Ketchup gunnen. Vi spawner en klon af vores bullet, udfra et gameobjekt vi har placeret på vores spiller.
        //Systemet checker om vi har en ketchupgun, om vi trykker på skyde knappen, og om vi lige har skudt. 
        if (Input.GetKeyDown(shoot) && gun1ShotLimit == true && haveGun1 == true)
        {
            //Vi sætter en bool til falsk, så vi ikke kan skyde.
            thisGunAnimator.SetBool("Pew", true);
            gun1ShotLimit = false;
            GameObject bulletClone = (GameObject)Instantiate(bullet, throwPoint.transform.position, throwPoint.transform.rotation);
            bulletClone.transform.localScale = -transform.localScale * 2f;
            //Vi får en funktion til at sætte samme bool til sand efter et sekund, så vi kan skyde igen.
            Invoke("ShootAgain", 1f);
        }

        if (Input.GetKeyDown(shoot) && gun1ShotLimit == true && haveGun2 == true)
        {
            //Vi sætter en bool til falsk, så vi ikke kan skyde.
            thisGunAnimator.SetBool("Pew", true);
            gun1ShotLimit = false;
            GameObject toiletPaperClone = (GameObject)Instantiate(toiletPaper, throwPoint.transform.position, throwPoint.transform.rotation);
            toiletPaperClone.transform.localScale = -transform.localScale;
            //Vi får en funktion til at sætte samme bool til sand efter et sekund, så vi kan skyde igen.
            Invoke("ShootAgain", 1f);
        }

        
    }

    //Her checker vi de forskellige ting der kollider og hvad de gør.
    //Hvis vores lille bobble rammer jorden, så kan man hoppe.
    //Hvis vi bliver ramt af en bullet, så bliver vores spiller destrueret.
    //Hvis vi kolliderer med ketchupgun, så har vi den
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCheck = false;
            thisBodyAnimator.SetBool("Jumping", false);
        }

        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "ToiletRoll")
        {
            Destroy(gameObject);            
        }
        if (collision.gameObject.tag == "Gun1")
        {
            haveGun1 = true;
            gunArm.GetComponent<SpriteRenderer>().enabled = true;
            penArm.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (collision.gameObject.tag == "Gun2")
        {
            haveGun2 = true;
            gunArm.GetComponent<SpriteRenderer>().enabled = true;
            penArm.GetComponent<SpriteRenderer>().enabled = false;
        }


    }

    //Hvis vores lille hoppe-bobbel forlader jorden, så kan vi ikke hoppe igen.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCheck = true;
        }
    }

    //Funktion vi bruger til at forsinke vores skud-frekvens.
    void ShootAgain()
    {
        gun1ShotLimit = true;
    }

   
}
