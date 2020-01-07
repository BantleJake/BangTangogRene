using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletTrash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }

        foreach (GameObject trash in GameObject.FindGameObjectsWithTag("Trash"))
        {
            Physics2D.IgnoreCollision(trash.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }

        foreach (GameObject roll in GameObject.FindGameObjectsWithTag("ToiletRoll"))
        {
            Physics2D.IgnoreCollision(roll.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());
        }
        foreach (GameObject ket in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Physics2D.IgnoreCollision(ket.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }
    }
}
