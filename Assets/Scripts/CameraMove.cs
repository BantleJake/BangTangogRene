using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    //Vi definere de to spillere
    public GameObject play1;
    public GameObject play2;
    
    //Variable vi bruger til at bestemme kameraets størrelse og placering
    private Vector3 middle;
    public Camera cam;
    private float MinSizeX;
    public float MinSizeY;
    public float buffer;

    //Hastigheden vi bruger til at smoothe cameraet
    private Vector3 velocity = Vector3.zero;

    //Vores variable til at holde styr på runderne
    private int roundCount;
    private bool roundOver;
    private int controlTracking;
    public GameObject zero;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject nOne;
    public GameObject nTwo;
    public GameObject nThree;
    public GameObject nFour;

    //Vores to spillere som prefabs, når vi skal spawne dem igen
    public GameObject play1Prefab;
    public GameObject play2Prefab;

    private void Start()
    {
        roundOver = false;
    }


    // Update is called once per frame
    void Update()
    {
        //I disse if-statements, checker vi om begge spillere er levende, og fokuserer kameraet på vinderen, hvem end det er
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
        
        //Her holder vi styr på runde-antallet, og genstarter scenen når der er spillet x-antal runder
        if (play1 == null && !roundOver)
        {
            roundOver = true;
            RoundCounter();

        } else if (play2 == null && !roundOver)
        {
            roundOver = true;
            RoundCounter();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            controlTracking++;
            ApartmentControl();
            print(controlTracking);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            controlTracking--;
            ApartmentControl();
            print(controlTracking);
        }

        
        
    }


    //En custom funktion der sætter kameraets position i forhold til spillernes afstand til hinanden
    void SetCameraPos()
    {
        Vector3 middle = (play1.transform.position + play2.transform.position) * 0.5f;
        Vector3 newPos = new Vector3(middle.x, middle.y+2f, cam.transform.position.z);
        
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, newPos, ref velocity, 0.3f);
    }


    //Her sætter vi kameraet orthografiske størrelse, også i forhold til spillernes afstand til hinanden.
    void SetCameraSize()
    {
        MinSizeX = MinSizeY * Screen.width / Screen.height;
        float width = (Mathf.Abs(play1.transform.position.x - play2.transform.position.x) * 0.5f) +buffer;
        float height = (Mathf.Abs(play1.transform.position.y - play2.transform.position.y) * 0.5f) +buffer;

        float CamSizeX = Mathf.Max(width, MinSizeX);
        cam.orthographicSize = Mathf.Lerp (cam.orthographicSize,Mathf.Max(height, CamSizeX * Screen.height / Screen.width, MinSizeY),3f);
    }

    //Vi sætter kameraet størrelse til noget mindre, så det fokuserer på en enkelt spiller
    void SetCameraSizeForOne(GameObject Winner)
    {
        cam.orthographicSize = Mathf.Lerp (cam.orthographicSize,3f,Time.deltaTime);
    }

    //Vi sætter kamera positionen på spilleren der har vundet runden.
    void SetCameraPosForOne(GameObject Winner)
    {
        Vector3 newPos = new Vector3(Winner.transform.position.x, Winner.transform.position.y, cam.transform.position.z);
        cam.transform.position = cam.transform.position = Vector3.SmoothDamp(cam.transform.position, newPos, ref velocity, 0.3f);
    }

    //I denne funktion genstarter vi runden, uden at slette de ting vi har ændret. 
    void ResetRound()
    {
        Destroy(play1);
        Destroy(play2);
        play1 = Instantiate(play1Prefab, new Vector3(-18, -4, 0), Quaternion.identity);
        play2 = Instantiate(play2Prefab, new Vector3(16, 2, 0), Quaternion.identity);
        roundOver = false;
        
    }

    //I denne funktion genstarter vi runden med en lille forsinkelse, og tæller op på rundetælleren. Når vi har spillet 3 runder, genstarter scenen.
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

    //Simpel funktion der genstarter scenen.
    void ResetScene()
    {
        SceneManager.LoadScene("Jakob");
    }

    void ApartmentControl()
    {
        if (controlTracking == 0)
        {
            zero.SetActive(true);
            one.SetActive(false);
            nOne.SetActive(false);
        }
        else if (controlTracking == 1)
        {
            one.SetActive(true);
            zero.SetActive(false);
            two.SetActive(false);
        }
        else if (controlTracking == 2)
        {
            two.SetActive(true);
            one.SetActive(false);
            three.SetActive(false);
        }
        else if (controlTracking == 3)
        {
            three.SetActive(true);
            two.SetActive(false);
            four.SetActive(false);
        }
        else if (controlTracking == 4)
        {
            four.SetActive(true);
            three.SetActive(false);
            Invoke("ResetScene", 3f);
        }
        else if (controlTracking == -1)
        {
            nOne.SetActive(true);
            zero.SetActive(false);
            nTwo.SetActive(false);
        } else if (controlTracking == -2)
        {
            nTwo.SetActive(true);
            nOne.SetActive(false);
            nThree.SetActive(false);
        } else if (controlTracking == -3)
        {
            nThree.SetActive(true);
            nTwo.SetActive(false);
            nFour.SetActive(false);
        } else if (controlTracking == -4)
        {
            nFour.SetActive(true);
            nThree.SetActive(false);
            Invoke("ResetScene", 3f);
        }
    }
}
