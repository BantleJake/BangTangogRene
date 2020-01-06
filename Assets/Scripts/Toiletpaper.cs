﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toiletpaper : MonoBehaviour
{
    public float projSpeedX;
    public float projSpeedY;
    private Rigidbody2D theRB;
    private int bounceOnce;

    
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        theRB.AddForce(new Vector2(projSpeedX * transform.localScale.x * 5, projSpeedY));
    }



    // Vi sætter kuglens hastighed og sender den afsted i forhold til hvilken vej spilleren kigger
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bounceOnce++;

        if (bounceOnce >= 2)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            //Instantiate(bloodSplat, transform.position, transform.rotation);
            //Instantiate(bulletEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}