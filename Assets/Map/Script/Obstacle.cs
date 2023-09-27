using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int hp;

    void Start()
    {
        hp = 3;
    }

    public void Get_Player_Shooted()
    {

        hp -= 1;
        if (hp == 0)
        {
            Destroy(gameObject);
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Enemydead);
    }
}
