using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, Object__Shooted_Interface
{
    [SerializeField]
    private int hp = 5;

    public void Get_Player_Shooted()
    {
        hp -= 1;
        if (hp <= 0)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Enemydead);
            Destroy(gameObject);
        }
    }
    public void Get_Enemy_Shooted()
    {

        hp -= 1;
        if (hp <= 0)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Enemydead);
            Destroy(gameObject);
        }

    }
}
