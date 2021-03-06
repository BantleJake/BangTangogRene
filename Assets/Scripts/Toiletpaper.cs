﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toiletpaper : MonoBehaviour
{
    public float projSpeedX;
    public float projSpeedY;
    private Rigidbody2D theRB;
    private int bounceOnce;
    public float lifetime;
    public GameObject rollEffect;
    public GameObject toiletTrash;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        theRB.AddForce(new Vector2(projSpeedX * transform.localScale.x * 5, projSpeedY));

        Invoke("DestroyRoll", lifetime);

    }



    // Vi sætter kuglens hastighed og sender den afsted i forhold til hvilken vej spilleren kigger
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            sound.Play(0);
            print("hep");
        }

        

        if (collision.gameObject.tag == "Player")
        {
            Instantiate(toiletTrash, transform.position, transform.rotation);
            Instantiate(rollEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(toiletTrash, transform.position, transform.rotation);
            Instantiate(rollEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void DestroyRoll()
    {
        Instantiate(toiletTrash, transform.position, transform.rotation);
        Instantiate(rollEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
