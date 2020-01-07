using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject one;
    public GameObject two;
    public float animTime;
    // Start is called before the first frame update
    void Start()
    {
        ImageTwo();
        Invoke("ResetGame", 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Invoke("ResetGame", 3.5f);
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

    void ResetGame()
    {
        SceneManager.LoadScene("Title");
    }
}
