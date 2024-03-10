using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject Smoke;
    public void Get_Player_Shooted()
    {
        Debug.Log("666");
        Instantiate(Smoke, transform);
        Destroy(this.gameObject, 5f);
        gameObject.layer = 8;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Gas);
    }
}
