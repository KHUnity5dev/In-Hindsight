using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{

    public GameObject Dark;
    public void Get_Player_Shooted()
    {
        Debug.Log("444");
        Instantiate(Dark, transform);
        gameObject.layer = 8;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Light);
    }
}
