using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlescreen : MonoBehaviour
{

    public GameObject one;
    public GameObject two;
    public float animTime;
    // Start is called before the first frame update
    void Start()
    {
        ImageTwo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Jakob");
        }
    }

    void ImageOne()
    {
        one.SetActive(true);
        two.SetActive(false);
        Invoke("ImageTwo", animTime);
    }

    void ImageTwo()
    {
        two.SetActive(true);
        one.SetActive(false);
        Invoke("ImageOne", animTime);
    }
}
