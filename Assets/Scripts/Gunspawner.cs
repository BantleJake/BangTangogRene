using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunspawner : MonoBehaviour
{

    public GameObject GunPrefab;
    public GameObject Gun;
    private bool GunTimeDone = true;


    Vector3 Gunpos;
    // Start is called before the first frame update
    void Start()
    {
        Gunpos = Gun.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gun == null && GunTimeDone)
        {
            GunTimeDone = false;
            Timer();
        }
    }

    void Timer()
    {
        Invoke("SpawnGun", 5f);
    }

   void SpawnGun()
    {

        Gun = Instantiate(GunPrefab, Gunpos, Quaternion.identity);
        GunTimeDone = true;
    }
}
