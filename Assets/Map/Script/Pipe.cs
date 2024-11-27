using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour, Object__Shooted_Interface
{
    public GameObject Smoke;
    public void Get_Player_Shooted()
    {
        Instantiate(Smoke, transform);

        gameObject.layer = 8;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Gas);
    }

    public void Get_Enemy_Shooted()
    {
        //Do nothing
    }
}
