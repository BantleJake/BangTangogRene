﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", 1);
    }

   void DestroyThis()
    {
        Destroy(gameObject);
    }
}