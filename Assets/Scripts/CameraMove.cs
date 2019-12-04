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
            Invoke("ResetScene", 3f);
        } else if (play1 == null && play2 != null)
        {
            SetCameraPosForOne(play2);
            SetCameraSizeForOne(play2);
            Invoke("ResetScene", 3f);
        }
        
        
        
    }

    void SetCameraPos()
    {
        Vector3 middle = (play1.transform.position + play2.transform.position) * 0.5f;
        cam.transform.position = new Vector3(middle.x, middle.y, cam.transform.position.z);
    }

    void SetCameraSize()
    {
        MinSizeX = MinSizeY * Screen.width / Screen.height;
        float width = (Mathf.Abs(play1.transform.position.x - play2.transform.position.x) * 0.5f) +buffer;
        float height = (Mathf.Abs(play1.transform.position.y - play2.transform.position.y) * 0.5f)+buffer;

        float CamSizeX = Mathf.Max(width, MinSizeX);
        cam.orthographicSize = Mathf.Max(height, CamSizeX * Screen.height / Screen.width, MinSizeY);
    }

    void SetCameraSizeForOne(GameObject Winner)
    {
        cam.orthographicSize = 3f;
    }

    void SetCameraPosForOne(GameObject Winner)
    {
        cam.transform.position = new Vector3(Winner.transform.position.x, Winner.transform.position.y, cam.transform.position.z);
    }

    void ResetScene()
    {
        SceneManager.LoadScene("Jakob");
    }
}
