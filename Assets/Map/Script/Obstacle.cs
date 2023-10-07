using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private int hp;

    void Start()
    {
    }

    public void Get_Player_Shooted()
    {

        hp -= 1;
        if (hp <= 0)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Enemydead);
            Destroy(gameObject);
        }
        
    }
}
