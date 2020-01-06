using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toiletpaper : MonoBehaviour
{
    public float projSpeedX;
    public float projSpeedY;
    private Rigidbody2D theRB;
    private int bounceOnce;
    public float lifetime;

    
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        theRB.AddForce(new Vector2(projSpeedX * transform.localScale.x * 5, projSpeedY));

        Invoke("DestroyRoll", lifetime);

    }



    // Vi sætter kuglens hastighed og sender den afsted i forhold til hvilken vej spilleren kigger
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            bounceOnce++;
            if (bounceOnce >= 2)
            {
                Destroy(gameObject);
            }
        } 


        if (collision.gameObject.tag == "Player")
        {
            //Instantiate(bloodSplat, transform.position, transform.rotation);
            //Instantiate(bulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void DestroyRoll()
    {
        Destroy(gameObject);
    }

}
