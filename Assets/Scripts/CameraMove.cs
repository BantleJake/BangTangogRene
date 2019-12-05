using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public GameObject play1;
    public GameObject play2;
    
    private Vector3 middle;
    public Camera cam;
    private float MinSizeX;
    public float MinSizeY;
    public float buffer;

    private Vector3 velocity = Vector3.zero;
    private int roundCount;
    private bool roundOver;

    public GameObject play1Prefab;
    public GameObject play2Prefab;

    private void Start()
    {
        roundOver = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (play1 != null && play2 != null)
        {
            SetCameraPos();
            SetCameraSize();
        } else if (play1 != null && play2 == null)
        {
            SetCameraPosForOne(play1);
            SetCameraSizeForOne(play1);
        } else if (play1 == null && play2 != null)
        {
            SetCameraPosForOne(play2);
            SetCameraSizeForOne(play2);
        }
        
        if (play1 == null && !roundOver)
        {
            roundOver = true;
            RoundCounter();

        } else if (play2 == null && !roundOver)
        {
            roundOver = true;
            RoundCounter();
        }
        
    }

    void SetCameraPos()
    {
        Vector3 middle = (play1.transform.position + play2.transform.position) * 0.5f;
        Vector3 newPos = new Vector3(middle.x, middle.y, cam.transform.position.z);
        
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, newPos, ref velocity, 0.3f);
    }

    void SetCameraSize()
    {
        MinSizeX = MinSizeY * Screen.width / Screen.height;
        float width = (Mathf.Abs(play1.transform.position.x - play2.transform.position.x) * 0.5f) +buffer;
        float height = (Mathf.Abs(play1.transform.position.y - play2.transform.position.y) * 0.5f) +buffer;

        float CamSizeX = Mathf.Max(width, MinSizeX);
        cam.orthographicSize = Mathf.Lerp (cam.orthographicSize,Mathf.Max(height, CamSizeX * Screen.height / Screen.width, MinSizeY),3f);
    }

    void SetCameraSizeForOne(GameObject Winner)
    {
        cam.orthographicSize = Mathf.Lerp (cam.orthographicSize,3f,Time.deltaTime);
    }

    void SetCameraPosForOne(GameObject Winner)
    {
        Vector3 newPos = new Vector3(Winner.transform.position.x, Winner.transform.position.y, cam.transform.position.z);
        cam.transform.position = cam.transform.position = Vector3.SmoothDamp(cam.transform.position, newPos, ref velocity, 0.3f);
    }

    void ResetRound()
    {
        Destroy(play1);
        Destroy(play2);
        play1 = Instantiate(play1Prefab, new Vector3(-18, -4, 0), Quaternion.identity);
        play2 = Instantiate(play2Prefab, new Vector3(16, 2, 0), Quaternion.identity);
        roundOver = false;
        
    }

    void RoundCounter()
    {
        if(roundCount == 0)
        {
            Invoke("ResetRound", 3f);
            roundCount++;
        } else if(roundCount == 1)
        {
            Invoke("ResetRound", 3f);
            roundCount++;
        }
        else if (roundCount == 2)
        {
            Invoke("ResetScene", 3f);
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene("Jakob");
    }
}
