using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SetCameraPos();
        SetCameraSize();
        
        
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
}
