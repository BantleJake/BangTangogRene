using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetGunAnimation : MonoBehaviour
{
    public Animator gun;

    void Reloading()
    {
        gun.SetBool("Pew", false);
    }
}
