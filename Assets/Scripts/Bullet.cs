using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float projSpeed;
    private Rigidbody2D theRB;

    public GameObject bulletEffect;
    public GameObject bloodSplat;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = new Vector2(projSpeed * transform.localScale.x * 5, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(bloodSplat, transform.position, transform.rotation);
        Instantiate(bulletEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
